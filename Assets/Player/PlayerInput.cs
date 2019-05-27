using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDict {
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;
    public bool createWall = false;
}

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAction playerAction;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAction = GetComponent<PlayerAction>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        InputDict inputDict = new InputDict();

        if(Input.GetKey("w")) {
            inputDict.up = true;
        }
        if(Input.GetKey("a")) {
            inputDict.left = true;
        }
        if(Input.GetKey("s")) {
            inputDict.down = true;
        }
        if(Input.GetKey("d")) {
            inputDict.right = true;
        }
        if(Input.GetKey("q")) {
            inputDict.createWall = true;
        }

        playerMovement.move(inputDict);
        playerAction.createWall(inputDict);
    }
}
