using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerInputProfile{
    public int JoyNumber;
    public string HorizontalAxis;
    public string JumpButton;
    public string AttackButton;

    public PlayerInputProfile(int joyNum)
    {
        this.JoyNumber = joyNum;
    }

    public PlayerInputProfile(int joyNum, string horizontal, string jump, string attack)
    {
        this.JoyNumber = joyNum;
        this.HorizontalAxis = horizontal;
        this.JumpButton = jump;
        this.AttackButton = attack;
    }

}
