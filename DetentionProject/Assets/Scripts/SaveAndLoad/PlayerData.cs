using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int maxHealth;
    public float maxStamina;
    public string playerName;

    public PlayerData(string playerName, int maxHealth, float maxStamina)
    {
        this.playerName = playerName;
        this.maxHealth = maxHealth;
        this.maxStamina = maxStamina;
    }
}