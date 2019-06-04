using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Vector3 position;

    public GameObject wallPrefab;
    public GameObject minePrefab;
    public GameObject rocketPrefab;
    

     void Start()
     {
     }
 
    void FixedUpdate()
    {

    }

    public void onInput(InputDict dict) {
        if (dict.createMine) {
            createMine();
        }
        if (dict.createWall) {
            createWall();
        }
        if (dict.shootRocket) {
            shootRocket();
        }
    }

    void createWall() {
        position = GetComponent<Transform>().position;

        position = position - GetComponent<Transform>().forward;

        Instantiate(wallPrefab, position, GetComponent<Transform>().rotation);
    }

    void createMine() {
        position = GetComponent<Transform>().position;

        position = position - GetComponent<Transform>().forward;

        Instantiate(minePrefab, new Vector3(position.x, 0, position.z), GetComponent<Transform>().rotation);
    }

    void shootRocket() {
        position = GetComponent<Transform>().position;

        position = position + GetComponent<Transform>().forward;

        Vector3 rot = GetComponent<Transform>().rotation.eulerAngles;
        rot = new Vector3(rot.x,rot.y+90,rot.z+90);

        GameObject rocket = Instantiate(rocketPrefab, new Vector3(position.x, 0.6f, position.z), Quaternion.Euler(rot));
        rocket.GetComponent<RocketController>().forwardDir = GetComponent<Transform>().forward;
    }
}