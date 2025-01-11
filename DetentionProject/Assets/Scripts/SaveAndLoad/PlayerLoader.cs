using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject player;
    public GameObject playerHand;
    public Sprite sofiaHand;
    public Sprite andreHand;
    public GameObject activeWeapon;
    public GameEvent onPlayerIconChanged;
    public GameEvent onPlayerTextChanged;
    private PlayerData loadedPlayer;

    //private bool hasChangeIcon;
    //private bool hasChangeText;

    public RuntimeAnimatorController defaultAnimatorController;
    public RuntimeAnimatorController alternateAnimatorController;

    void Awake()
    {
        loadedPlayer = SaveManager.LoadPlayerData();
        if (loadedPlayer != null)
        {
            Animator playerAnimator = player.GetComponent<Animator>();
            if (loadedPlayer.hasTakenSword) {
                activeWeapon.SetActive(true);
            }
            if (playerAnimator != null)
            {
                switch (loadedPlayer.playerName)
                {
                    case "Sofia":
                        //playerText.SetText("Sofia");
                        playerAnimator.runtimeAnimatorController = alternateAnimatorController;
                        playerHand.GetComponent<SpriteRenderer>().sprite = sofiaHand;
                        break;

                    case "Andre":
                        //playerText.SetText("Andre");
                        playerAnimator.runtimeAnimatorController = defaultAnimatorController;
                        playerHand.GetComponent<SpriteRenderer>().sprite = andreHand;
                        break;
                }
            }
        }
    }

    private void Update()
    {
        
        onPlayerIconChanged.Raise(this, loadedPlayer.playerName);
        onPlayerTextChanged.Raise(this, loadedPlayer.playerName);

    }
}
