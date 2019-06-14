using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Material normalMaterial;
    public Material stunnedMaterial;

    private IState state = null;

    public float grappleForce = 40.0f;
    public float dashForce = 30.0f;

    void Start() {
        this.ChangeState(new IdleState(this));
    }

    void Update() {
        if (state != null)state.Execute();
    }

    // STATE

    public void ChangeState(IState newState) {
        if (state != null)
            state.Exit();

        state = newState;
        state.Enter();
    }

    // CONTROLLER

    public void AlertDeath() {
        //alert player to their immenint death
    }

    public void OnDeath() {
        // show that we dead
        GetComponent<MeshRenderer>().material = stunnedMaterial;
    }

    public void OnInput(InputDict inputDict) {
        if (state != null) {
            state.OnInput(inputDict);
        }
    }

    void OnCollisionEnter(Collision collision) {
        state.OnCollisionEnter(collision);
    }
}

public class IState {
    public virtual void Enter() {
        Debug.Log("enter: " + this.GetType().Name);
    }
    public virtual void Execute() {}
    public virtual void Exit() {
        Debug.Log("exit: " + this.GetType().Name);
    }
    public virtual void OnInput(InputDict inputDict) {}
    public virtual void OnCollisionEnter(Collision collision) {}
}

public class IdleState : IState {
    PlayerController owner;

    public IdleState(PlayerController owner) { this.owner = owner; }

    public override void OnInput(InputDict inputDict) {
        if (inputDict.jump) {
            owner.ChangeState(new JumpingState(owner));
        } else {
            owner.GetComponent<PlayerMovement>().move(inputDict);
            owner.GetComponent<PlayerAction>().onInput(inputDict);
        }
    }
}

public class StunnedState : IState {
    PlayerController owner;
    float time = 0.0f;
    float minTime = 0.2f;

    public StunnedState(PlayerController owner) { this.owner = owner; }

    public override void Enter() {
        owner.GetComponent<MeshRenderer>().material = owner.stunnedMaterial;
    }

    public override void Execute() {
        time += Time.deltaTime;
        if (owner.GetComponent<Rigidbody>().velocity.magnitude <= owner.GetComponent<PlayerMovement>().GetMaxSpeed() && time > minTime) {
            owner.ChangeState(new IdleState(owner));
        }
    }

    public override void Exit() {
        owner.GetComponent<MeshRenderer>().material = owner.normalMaterial;
    }

    public override void OnInput(InputDict inputDict) {
        owner.GetComponent<PlayerMovement>().move(inputDict, 0.5f);
    }
}

public class GrappleState : IState {
    PlayerController owner;
    Vector3 desiredPosition;

    public GrappleState(PlayerController owner, Vector3 desiredPosition) { this.owner = owner; this.desiredPosition = desiredPosition; }

    public override void Execute() {
        Vector3 pos = owner.GetComponent<Transform>().position;
        owner.GetComponent<PlayerAction>().UpdateGrappleStartPos(pos);
        owner.GetComponent<Rigidbody>().AddForce((desiredPosition - pos).normalized * owner.grappleForce);
    }

    public override void Exit() {
        owner.GetComponent<Rigidbody>().velocity = Vector3.zero;
        owner.GetComponent<PlayerAction>().HideGrapple();
    }

    public override void OnInput(InputDict inputDict) {
        owner.GetComponent<PlayerMovement>().move(inputDict, 0.5f);
    }

    public override void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Walls")) {
            owner.ChangeState(new IdleState(owner));
        }
    }
}

public class DashState : IState {
    PlayerController owner;
    Vector3 desiredPosition;

    public DashState(PlayerController owner, Vector3 desiredPosition) { this.owner = owner; this.desiredPosition = desiredPosition; }

    public override void Execute() {
        Vector3 pos = owner.GetComponent<Transform>().position;
        owner.GetComponent<Rigidbody>().velocity = (desiredPosition - pos).normalized * owner.dashForce;

        if (Vector3.Distance(owner.GetComponent<Transform>().position, desiredPosition) < 0.5f) {
            owner.ChangeState(new IdleState(owner));
        }
    }

    public override void Exit() {
        owner.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public override void OnInput(InputDict inputDict) {
        owner.GetComponent<PlayerMovement>().move(inputDict, 0.5f);
    }

    public override void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Walls")) {
            owner.ChangeState(new IdleState(owner));
        }
    }
}

public class JumpingState : IState {
    PlayerController owner;
    Vector3 startingPosition;

    float time = 0.0f;

    public JumpingState(PlayerController owner) { this.owner = owner; }

    public override void Enter() {
        Debug.Log("entering jump state");
        owner.GetComponent<CapsuleCollider>().enabled = false;
        startingPosition = owner.GetComponent<Transform>().position;
        owner.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public override void Execute() {

        time += Time.deltaTime;
        if (time * 2 < 1)
            owner.GetComponent<Transform>().position = MathParabola.Parabola(
                startingPosition,
                startingPosition + (owner.GetComponent<Transform>().forward * 2),
                1f,
                time * 2
            );
        if (time * 2 >= 1.4f) {
            owner.ChangeState(new IdleState(owner));
        }
    }

    public override void Exit() {
        owner.GetComponent<CapsuleCollider>().enabled = true;
        Debug.Log("exiting jump state");
        Transform transform = owner.GetComponent<Transform>();
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }

    public override void OnInput(InputDict inputDict) {

    }
}

public class MathParabola {

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t) {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

}