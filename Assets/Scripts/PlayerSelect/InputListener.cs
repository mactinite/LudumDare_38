using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListener : MonoBehaviour {


    public PlayerInputManager inputManager;

    public int playerNumber = 0;
    public bool playerJoined = false;

    public Transform playerJoinedUI;
    public Transform inactiveUI;

    public PlayerInputProfile inputProfile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown(PlayerInputManager.JumpButtonBase + "_" + playerNumber) && !playerJoined)
        {
            playerJoined = true;
            playerJoinedUI.gameObject.SetActive(playerJoined);
            inactiveUI.gameObject.SetActive(!playerJoined);
            inputProfile = inputManager.AddPlayer(playerNumber);
        }

        if(Input.GetButtonDown(PlayerInputManager.AttackButtonBase + "_" + playerNumber) && playerJoined)
        {
            playerJoined = false;
            playerJoinedUI.gameObject.SetActive(playerJoined);
            inactiveUI.gameObject.SetActive(!playerJoined);
            inputManager.RemovePlayer(inputProfile);
        }   

	}
}
