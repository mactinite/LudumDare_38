using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{

    public int numberOfPlayers = 1;
    public int maxPlayers = 4;

    public static string HorizontalAxisBase = "Horizontal";
    public static string JumpButtonBase = "Jump";
    public static string AttackButtonBase = "Attack";

    public List<PlayerInputProfile> playerInfo = new List<PlayerInputProfile>();


    public Transform playerPrefab;
    public Transform playerTrackerPrefab;
    public Color[] colorSet;

    public int playerCount;

    public Text winText;

    bool winnerSelected = false;

    public enum GameState
    {
        JoinScreen,
        InGame,
        WinScreen
    }
    public GameState gameState = GameState.JoinScreen;
    bool playersSpawned = false;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.JoinScreen:
                JoinScreen();
                break;
            case GameState.InGame:
                InGame();
                break;
            case GameState.WinScreen:
                WinScreen();
                break;
        }
    }

    void MainMenu()
    {
        if (SceneManager.GetActiveScene().name == "PlayerSelect")
        {
            gameState = GameState.JoinScreen;
        }
    }

    void WinScreen()
    {
        if(!winnerSelected)
            StartCoroutine(Winner());
    }

    IEnumerator Winner()
    {
        winnerSelected = true;
        //Pass player number to UI element
        GameObject winningPlayer = GameObject.FindGameObjectWithTag("Player");
        if (winningPlayer != null)
        {
            winText.text = "Player " + (playerInfo[winningPlayer.GetComponent<CharacterMotor>().playerNum].JoyNumber + 1) + "wins!";
        }
        else
        {
            winText.text = "It was a tie!";
        }
        //Show UI Element
        winText.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
        Destroy(this.gameObject);
    }

    void InGame()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;

        //Spawn players with the right inputs
        if (!playersSpawned)
        {
            for (int i = 0; i < playerInfo.Count; i++)
            {
                SpawnPlayer(i);
            }
            playersSpawned = true;
        }

        if(playersSpawned && playerCount < 2)
        {
            gameState = GameState.WinScreen;
        }

    }

    void JoinScreen()
    {
        if (playerInfo.Count > 0)
        {
            if (Input.GetButtonDown("Start"))
            {
                SceneManager.LoadScene("GameScene");
                gameState = GameState.InGame;
            }
        }
    }

    //Set up and spawn a player
    public void SpawnPlayer(int playerIndex)
    {

        PlayerInputProfile profile = playerInfo[playerIndex];
        Vector2 playerPosition = GetPointUnitOnCircle(Random.Range(0, 360), 5);
        Transform player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        Transform tracker = Instantiate(playerTrackerPrefab, playerPosition, Quaternion.identity);
        PlayerTracker pt = tracker.GetComponent<PlayerTracker>();
        CharacterMotor cm = player.GetComponent<CharacterMotor>();
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        cm.HorizontalAxis = profile.HorizontalAxis;
        cm.JumpButtonName = profile.JumpButton;
        cm.gravityPoint = GameObject.FindGameObjectWithTag("World").transform;
        cm.playerNum = playerIndex;
        player.GetComponent<Grabber>().throwButton = profile.AttackButton;
        sr.color = colorSet[profile.JoyNumber];
        pt.player = player;
        pt.playerNumber = profile.JoyNumber + 1;
        pt.trackerColor = sr.color;
        playerCount++;
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
        if (playerInfo.Count < playerNumber)
        {
            return null;
        }
        else
        {
            return playerInfo[playerNumber];
        }
    }

    public Vector2 GetPointUnitOnCircle(float angleDegrees, float radius)
    {
        float x = 0.0f;
        float y = 0.0f;
        float angleRadians = 0.0f;
        Vector2 result;

        angleRadians = angleDegrees * Mathf.PI / 180.0f;
        x = radius * Mathf.Cos(angleRadians);
        y = radius * Mathf.Sin(angleRadians);

        result = new Vector2(x, y);

        return result;
    }

}
