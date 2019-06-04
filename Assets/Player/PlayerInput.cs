using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDict {
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;
    public bool createWall = false;
    public bool createMine = false;
    public bool shootRocket = false;
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
        if(Input.GetKey("j")) {
            inputDict.createWall = true;
        }
        if(Input.GetKey("k")) {
            inputDict.createMine = true;
        }
        if(Input.GetKey("l")) {
            inputDict.shootRocket = true;
        }

        playerMovement.move(inputDict);
        playerAction.onInput(inputDict);
    }
}
