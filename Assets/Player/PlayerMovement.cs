using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
 // Normal Movements Variables
     private float walkSpeed;
     private float curSpeed;
     private float maxSpeed;
 
     //private CharacterStat plStat;
 
     void Start()
     {
         //plStat = GetComponent<CharacterStat>();
 
         //walkSpeed = (float)(plStat.Speed + (plStat.Agility/5));

         walkSpeed = 5;
         //sprintSpeed = walkSpeed + (walkSpeed / 2);
 
     }
 
    void FixedUpdate()
    {

    }

    public void move(InputDict dict) {
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;

        int h = 0;
        int v = 0;

        if (dict.left)
            h -= 1; 
        if (dict.right)
            h += 1;
        if (dict.up)
            v -= 1;
        if (dict.down)
            v += 1;
 
        // Move senteces
        GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal")* curSpeed, 0.8f), GetComponent<Rigidbody>().velocity.y,
                                            Mathf.Lerp(0, Input.GetAxis("Vertical")* curSpeed, 0.8f));
    }
}