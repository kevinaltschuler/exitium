using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockYPosition : MonoBehaviour
{

    Transform transform;
    float startY;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,startY,transform.position.z);
    }
}
