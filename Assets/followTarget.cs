using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTarget : MonoBehaviour
{
 
     public Transform target;
     public float smoothTime = 0.3f;
 
     private Vector3 velocity = Vector3.zero;
 
     void Update () {
         Vector3 goalPos = target.position;
         goalPos.y = target.position.y + 10;
         goalPos.z = goalPos.z - 5;
         transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
         transform.LookAt(target.position);
     }
}