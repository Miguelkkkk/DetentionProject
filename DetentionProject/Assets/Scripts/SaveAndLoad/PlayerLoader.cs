using TMPro;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI playerText;
    public GameObject playerHand;
    public Sprite sofiaHand;
    public Sprite andreHand;
    public GameObject activeWeapon;
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
            if (loadedPlayer.hasTakenSword) {
                activeWeapon.SetActive(true);
            }
            if (playerAnimator != null)
            {
                switch (loadedPlayer.playerName)
                {
                    case "Sofia":
                        playerText.SetText("Sofia");
                        playerAnimator.runtimeAnimatorController = alternateAnimatorController;
                        playerHand.GetComponent<SpriteRenderer>().sprite = sofiaHand;
                        break;

                    case "Andre":
                        playerText.SetText("Andre");
                        playerAnimator.runtimeAnimatorController = defaultAnimatorController;
                        playerHand.GetComponent<SpriteRenderer>().sprite = andreHand;
                        break;
                }
            }
        }
    }
}
