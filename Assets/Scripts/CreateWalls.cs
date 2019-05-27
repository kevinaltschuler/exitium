using UnityEngine;
public class CreateWalls : MonoBehaviour 
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject wallPrefab;

    // This script will simply instantiate the Prefab when the game starts.
    public void Create(int x, int y, int z)
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        Instantiate(wallPrefab, new Vector3(x, y, z), Quaternion.identity);
    }
}