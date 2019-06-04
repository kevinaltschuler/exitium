using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class CutHoleInFloor : MonoBehaviour
{
    private int holeAngle = 45;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // void DeleteCircle(int index, Vector3 point) {
    //     Destroy(this.gameObject.GetComponent<MeshCollider>());
    //     Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
    //     int[] oldTriangles = mesh.triangles;
    //     int[] newTriangles = new int[mesh.triangles.Length - 3];

    //     float originX = point.x;
    //     float originZ = point.z;
    //     float originY = point.y;
    //     float radius = 2;

    //     int i = 0;
    //     int j = 0;
    //     while(j < mesh.triangles.Length) {
    //         if(j != index*3) {
    //             newTriangles[i++] = oldTriangles[j++];
    //             newTriangles[i++] = oldTriangles[j++];
    //             newTriangles[i++] = oldTriangles[j++];
    //         } else {
    //             int angle = 0;
    //             int[] circleTriangles = new int[360/holeAngle];
    //             int k = 0;
    //             while(angle <= 360) {
    //                 float newX = radius / Mathf.Cos(holeAngle);
    //                 float newZ = radius / Mathf.Sin(holeAngle);
    //                 circleTriangles[k++] = newX;
    //                 circleTriangles[k++] = originY;
    //                 circleTriangles[k++] = newZ;
    //                 angle += holeAngle;
    //             }
    //             int n = 0;
    //             while(n < circleTriangles.Length) {
    //                 newTriangles[i++] = circleTriangles[n++];
    //                 newTriangles[i++] = circleTriangles[n++];
    //                 newTriangles[i++] = circleTriangles[n++];
    //                 n += 3;
    //             }
    //         }
    //     }
    //     transform.GetComponent<MeshFilter>().mesh.triangles = newTriangles;
    //     this.gameObject.AddComponent<MeshCollider>();
    // }

    void DeleteTri(int index) {
        Destroy(this.gameObject.GetComponent<MeshCollider>());
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        int[] oldTriangles = mesh.triangles;
        int[] newTriangles = new int[mesh.triangles.Length - 3];

        int i = 0;
        int j = 0;
        while(j < mesh.triangles.Length) {
            if(j != index*3) {
                newTriangles[i++] = oldTriangles[j++];
                newTriangles[i++] = oldTriangles[j++];
                newTriangles[i++] = oldTriangles[j++];
            } else {
                j += 3;
            }
        }
        transform.GetComponent<MeshFilter>().mesh.triangles = newTriangles;
        this.gameObject.AddComponent<MeshCollider>();
    }

    void DeleteTriInPlace(int index) {
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        int[] triangles = mesh.triangles;

        triangles = triangles.RemoveThreeAt(index*3);

        //Destroy(this.gameObject.GetComponent<MeshCollider>());
        transform.GetComponent<MeshFilter>().mesh.triangles = triangles;
        //this.gameObject.AddComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f)) {
                DeleteTriInPlace(hit.triangleIndex);
            }
        }
    }
}


public static class Utils {
    public static T[] RemoveThreeAt<T>(this T[] source, int index)
    {
        T[] dest = new T[source.Length - 3];
        if( index > 0 )
            Array.Copy(source, 0, dest, 0, index);

        if( index < source.Length - 1 )
            Array.Copy(source, index + 3, dest, index, source.Length - index - 3);

        return dest;
    }
}


