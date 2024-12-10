using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject player;
    public GameEvent onPlayerIconChanged;

    public RuntimeAnimatorController defaultAnimatorController;
    public RuntimeAnimatorController alternateAnimatorController;

    void Awake()
    {
        PlayerData loadedPlayer = SaveManager.LoadPlayerData();
        if (loadedPlayer != null)
        {
            onPlayerIconChanged.Raise(this, loadedPlayer.playerName);
            Debug.Log("Jogador carregado: " + loadedPlayer.playerName);

            Animator playerAnimator = player.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                switch (loadedPlayer.playerName)
                {
                    case "Sofia":
                        playerAnimator.runtimeAnimatorController = alternateAnimatorController;
                        break;

                    case "Andre":
                        playerAnimator.runtimeAnimatorController = defaultAnimatorController;
                        break;
                }
            }
        }
    }
}
