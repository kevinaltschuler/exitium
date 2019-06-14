using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Normal Movements Variables
    private float maxSpeed;
    public float rotateSpeed = 100.0f;
    public float xSpeed = 0.0f;
    public float zSpeed = 0.0f;
    private Rigidbody rb;

    //private CharacterStat plStat;

    void Start() {
        rb = GetComponent<Rigidbody>();
        //plStat = GetComponent<CharacterStat>();

        //walkSpeed = (float)(plStat.Speed + (plStat.Agility/5));

        maxSpeed = 3.0f;
        //sprintSpeed = walkSpeed + (walkSpeed / 2);

    }

    void FixedUpdate() {
        xSpeed = rb.velocity.x;
        zSpeed = rb.velocity.z;
    }

    public void move(InputDict dict, float factor = 1.0f) {
        //maxSpeed = max*factor;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float xToAdd = 0;
        float zToAdd = 0;
        float curX = rb.velocity.x;
        float curZ = rb.velocity.z;

        //TODO
        /* diagonal movement is too fast because i need to normalize
            the speed of the character and not the velocity being added to the speed
         */

        /*
            scale the addition to the velocity based on the speed
            so if the current speed is > maxspeed then hitting the key should add 0 to the velocity
            and if velocity.x == 0 then we add full speed to the velocity
         */

        // interpolate -x<maxspeed<x to -1<maxSpeed<1
        // multiply speed to add by (-1*abs(maxSpeed)) + 1

        // inverselerp returns 0-1 so then shift it over to -0.5 to 0.5 then * 2
        // this block returns curSpeed in -1 to 1
        float interpolatedX = (Mathf.InverseLerp(-1 * maxSpeed, maxSpeed, Mathf.Clamp(curX, -1 * maxSpeed, maxSpeed)) - 0.5f) * 2;
        float interpolatedZ = (Mathf.InverseLerp(-1 * maxSpeed, maxSpeed, Mathf.Clamp(curZ, -1 * maxSpeed, maxSpeed)) - 0.5f) * 2;

        // then caluculate how much to add based on how fast the character is already moving in that direction
        Vector3 vectorToAdd = new Vector3(x - interpolatedX, 0, z - interpolatedZ);

        if (vectorToAdd.magnitude > 1) {
            vectorToAdd.Normalize();
        }

        // limits diagonal speed but does not allow DI
        // if (rb.velocity.magnitude < maxSpeed) {
        //     rb.velocity
        //         = new Vector3(curX + vectorToAdd.x, rb.velocity.y, curZ + vectorToAdd.z);
        // }

        // allows DI but does not limit diagonal speed
        if (Mathf.Abs(rb.velocity.x) < maxSpeed) {
            rb.velocity = new Vector3(curX + vectorToAdd.x, rb.velocity.y, curZ);
        }

        if (Mathf.Abs(rb.velocity.z) < maxSpeed) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, curZ + vectorToAdd.z);
        }

        if (x != 0 || z != 0) {
            GetComponent<Transform>().rotation =
                Quaternion.RotateTowards(
                    GetComponent<Transform>().rotation,
                    Quaternion.LookRotation(
                        new Vector3(
                            rb.velocity.x,
                            0,
                            rb.velocity.z
                        )
                    ),
                    Time.deltaTime * rotateSpeed
                );
        }
    }

    public float GetMaxSpeed() {
        return this.maxSpeed;
    }
}