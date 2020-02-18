using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputDict {
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;
    public bool createWall = false;
    public bool createMine = false;
    public bool shootRocket = false;
    public bool jump = false;
    public bool grapplingHook = false;
    public bool dash = false;
}

public class PlayerInput : NetworkBehaviour {
    private PlayerMovement playerMovement;
    private PlayerAction playerAction;

    // Start is called before the first frame update
    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        playerAction = GetComponent<PlayerAction>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (hasAuthority == false) {
            return;
        }

        InputDict inputDict = new InputDict();

        if (Input.GetKey("w")) {
            inputDict.up = true;
        }
        if (Input.GetKey("a")) {
            inputDict.left = true;
        }
        if (Input.GetKey("s")) {
            inputDict.down = true;
        }
        if (Input.GetKey("d")) {
            inputDict.right = true;
        }
        if (Input.GetKey("j")) {
            inputDict.dash = true;
        }
        if (Input.GetKey("k")) {
            inputDict.grapplingHook = true;
        }
        if (Input.GetKey("l")) {
            inputDict.shootRocket = true;
        }
        if (Input.GetKey(KeyCode.Space)) {
            inputDict.jump = true;
        }

        GetComponent<PlayerController>().OnInput(inputDict);
    }
}