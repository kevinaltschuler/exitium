using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

    public GameObject piece;
    private List<GameObject> pieces = new List<GameObject>();
    [Range(0, 10.0f)]
    public float size;
    [Range(0, 360.0f)]
    public float rotation;

    void Start() {
        // check length of wall and build wall with many wall pieces
        float numPieces = size / 0.5f;

        Vector3 forwardVector = Quaternion.Euler(new Vector3(0, rotation, 0)) * Vector3.forward * 0.5f * size;

        for (int i = 0; i < numPieces; i++) {
            Vector3 piecePos = GetComponent<Transform>().position - forwardVector * (size * 0.2f);
            piecePos = piecePos + forwardVector * i * 0.2f;
            GameObject newPiece = Instantiate(piece, piecePos, Quaternion.Euler(0, rotation, 0))as GameObject;
            newPiece.GetComponent<Transform>().parent = gameObject.transform;
            pieces.Add(newPiece);
        }

    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector3 forwardVector = Quaternion.Euler(new Vector3(0, rotation, 0)) * Vector3.forward * 0.5f * size;
        Vector3 startPos = GetComponent<Transform>().position + forwardVector;
        Vector3 endPos = GetComponent<Transform>().position - forwardVector;
        Gizmos.DrawLine(startPos, endPos);
    }

    void Update() {

    }

    public void deletePieces(int startIndex, int endIndex) {

    }

    public void deletePiece(int index) {

    }
}