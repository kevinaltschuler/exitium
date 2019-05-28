using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    CreateWalls createWalls;
    Vector3 position;
    

     void Start()
     {
        createWalls = GetComponent<CreateWalls>();
     }
 
    void FixedUpdate()
    {

    }

    public void createWall(InputDict dict) {
        position = GetComponent<Transform>().position;

        if(dict.createWall){
            createWalls.Create(position.x,position.y,position.z+1);
        }
    }
}