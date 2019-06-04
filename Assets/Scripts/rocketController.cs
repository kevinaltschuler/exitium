using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{

    public Vector3 forwardDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity += forwardDir;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") {
            Explode();
        }
    }

    void Explode() {
        Object.Destroy(this.gameObject);
    }
}
