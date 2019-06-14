using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    private float stayCount = 0.0f;
    private bool armed = false;
    public Material Material1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && armed == true) {
            other.gameObject.GetComponent<PlayerController>().AlertDeath();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player" && armed == true)
        {
            if (stayCount > 0.25f)
            {
                //Debug.Log("staying");
                stayCount = 0;
                other.gameObject.GetComponent<PlayerController>().OnDeath();
                Object.Destroy(this.gameObject);
            }
            else
            {
                stayCount = stayCount + Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            armed = true;
            GetComponent<MeshRenderer>().material = Material1;
        }
    }

}
