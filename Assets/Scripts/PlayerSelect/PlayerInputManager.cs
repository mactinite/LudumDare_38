using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    public int numberOfPlayers = 1;
    public int maxPlayers = 8;

    public static string HorizontalAxisBase = "Horizontal";
    public static string JumpButtonBase = "Jump";
    public static string AttackButtonBase = "Attack";

    public List<PlayerInputProfile> playerInfo = new List<PlayerInputProfile>();

    public enum GameState
    {
        MainMenu,
        JoinScreen,
        InGame
    }
    public GameState gameState = GameState.MainMenu;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (gameState)
        {
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.JoinScreen:
                JoinScreen();
                break;
            case GameState.InGame:
                InGame();
                break;
        }
    }

    void MainMenu()
    {
        
    }

    void InGame()
    {
        //Spawn players with the right inputs
    }

    void JoinScreen()
    {
        //Listen For Input and add players
        for(int i = 0; i < maxPlayers; i ++)
        {
            if(Input.GetButtonDown(JumpButtonBase + "_" + i))
            {
                //Add player, assign input of i
                AddPlayer(i);
            }
        }


    }


    public PlayerInputProfile AddPlayer(int joystickNumber)
    {
        string horizontal = HorizontalAxisBase + '_' + joystickNumber;
        string jump = JumpButtonBase + '_' + joystickNumber;
        string attack = AttackButtonBase + '_' + joystickNumber;
        PlayerInputProfile player = new PlayerInputProfile(joystickNumber, horizontal, jump, attack);

        playerInfo.Add(player);

        return player;
    }

    public void RemovePlayer(PlayerInputProfile profile)
    {
        playerInfo.Remove(profile);
    }


    public PlayerInputProfile GetPlayerInformation(int playerNumber)
    {
        if(playerInfo.Count < playerNumber)
        {
            return null;
        }
        else
        {
            return playerInfo[playerNumber];
        }
    }

}
