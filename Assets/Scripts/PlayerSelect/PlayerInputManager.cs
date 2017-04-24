using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour {

    public int numberOfPlayers = 1;
    public int maxPlayers = 4;

    public static string HorizontalAxisBase = "Horizontal";
    public static string JumpButtonBase = "Jump";
    public static string AttackButtonBase = "Attack";

    public List<PlayerInputProfile> playerInfo = new List<PlayerInputProfile>();

    public Transform playerPrefab;
    public Transform playerTrackerPrefab;
    public Color[] colorSet;

    public enum GameState
    {
        MainMenu,
        JoinScreen,
        InGame
    }
    public GameState gameState = GameState.MainMenu;
    bool playersSpawned = false;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
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
        if(SceneManager.GetActiveScene().name == "PlayerSelect")
        {
            gameState = GameState.JoinScreen;
        }
    }

    void InGame()
    {
        //Spawn players with the right inputs
        if (!playersSpawned)
        {
            for(int i = 0; i < playerInfo.Count; i ++)
            {
                SpawnPlayer(i);
            }
            playersSpawned = true;
        }

    }

    void JoinScreen()
    {
        if(playerInfo.Count > 0)
        {
            if (Input.GetButtonDown("Start"))
            {
                SceneManager.LoadScene("GameScene");
                gameState = GameState.InGame;
            }
        }
    }

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
        player.GetComponent<Grabber>().throwButton = profile.AttackButton;
        //TODO: add other inputs here
        sr.color = colorSet[profile.JoyNumber];

        pt.player = player;
        pt.playerNumber = profile.JoyNumber + 1;
        pt.trackerColor = sr.color;
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
