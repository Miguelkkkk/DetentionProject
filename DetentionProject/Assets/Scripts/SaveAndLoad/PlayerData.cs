using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public bool hasTakenSword;

    public PlayerData(string playerName, bool hasTakenSword)
    {
        this.playerName = playerName;
        this.hasTakenSword = hasTakenSword;
    }
}