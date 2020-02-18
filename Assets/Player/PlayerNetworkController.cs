using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkController : NetworkBehaviour {
    public GameObject playerPrefab;

    private GameObject localPlayer;

    // Start is called before the first frame update
    void Start() {
        if (isLocalPlayer == false) {
            return;
        }
        CmdSpawnPlayer();
    }

    // Update is called once per frame
    void Update() {

    }

    [Command]
    void CmdSpawnPlayer() {
        localPlayer = Instantiate(playerPrefab);
        NetworkServer.SpawnWithClientAuthority(localPlayer, connectionToClient);
    }
}