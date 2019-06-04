using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     public Material Material1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlertDeath() {
        //alert player to their immenint death
    }

    public void OnDeath() {
        // show that we dead
        GetComponent<MeshRenderer>().material = Material1;
    }
}
