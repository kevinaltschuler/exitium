using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    CreateWalls createWalls;

     void Start()
     {
        createWalls = GetComponent<CreateWalls>();
     }
 
    void FixedUpdate()
    {

    }

    public void createWall(InputDict dict) {
        if(dict.createWall){
            createWalls.Create(0,0,0);
        }
    }
}