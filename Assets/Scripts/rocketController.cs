using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {
    public GameObject explosion;
    public Vector3 forwardDir;
    private float radius = 2.5f;
    private float power = 1000.0f;
    private float time = 0.0f;
    private float killTimer = 0.0f;
    private bool exploded = false;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        time += Time.deltaTime;
        if (time >= 0.7f) {
            Explode();
        }
        if (exploded) {
            killTimer += Time.deltaTime;

            if (killTimer >= 0.5f) {
                Object.Destroy(this.gameObject);
            }
        } else {
            GetComponent<Rigidbody>().velocity += forwardDir;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") {
            Explode();
        }
    }

    void Explode() {
        if (exploded == false) {
            GetComponent<Renderer>().enabled = false;
            Vector3 explosionPos = GetComponent<Transform>().position - (forwardDir * 0.1f);
            GameObject expl = Instantiate(explosion, explosionPos, Quaternion.identity)as GameObject;
            expl.GetComponent<Transform>().localScale = new Vector3(radius * 1.2f, radius * 1.2f, radius * 1.2f);
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders) {
                PlayerController player = hit.GetComponent<PlayerController>();
                if (player) {
                    hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 0.0F);
                    player.ChangeState(new StunnedState(player));
                }
            }
            Collider[] closeColliders = Physics.OverlapSphere(explosionPos, radius * 0.5f);
            foreach (Collider hit in closeColliders) {
                if (hit.gameObject.tag == "Wall") {
                    Destroy(hit.gameObject);
                }
            }
            exploded = true;
        }
    }
}