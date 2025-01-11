using DialogueGraph.Runtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class NPCDialogue : MonoBehaviour {
    public RuntimeDialogueGraph DialogueSystem;
    public LineController LineController;

    [Header("UI References")]
    public GameObject SecondaryScreen;
    public GameObject PlayerContainer;
    public GameObject NpcContainer;
    public TMP_Text PlayerText;
    public TMP_Text NpcText;
    public TMP_Text NpcName;

    [Header("InputReader")]
    public UIInputReader input;
    public PlayerInputReader playerInput;

    public bool isInConversation = false;
    protected bool showingSecondaryScreen;
    protected bool showPlayer;
    protected bool isPlayerChoosing;
    protected bool shouldShowText;
    protected bool showingText;
    protected string textToShow;

    protected void OnEnable()
    {
        input.SkipDialogueEvent += OnSkipDialogue;
    }
    protected void OnDisable() 
    {
        input.SkipDialogueEvent -= OnSkipDialogue;
    }

    protected void OnSkipDialogue() {
        if (showingText)
        {
            showingText = false;
            (showPlayer ? PlayerContainer : NpcContainer).SetActive(false);
            (showPlayer ? PlayerText : NpcText).gameObject.SetActive(false);
        }  
    }

    public void StartConversation() {
        if (!isInConversation) { 
            DialogueSystem.ResetConversation();
            isInConversation = true;
            (showPlayer ? PlayerContainer : NpcContainer).SetActive(true);
            playerInput.DisablePlayerAction();
        }
    }

    protected void Update() {

        #region comments1
        //if (showingSecondaryScreen) {
        //    if (Input.GetKeyDown(KeyCode.Escape)) {
        //        showingSecondaryScreen = false;
        //        SecondaryScreen.SetActive(false);
        //    }
        //    return;
        //}
        #endregion

        if (!isInConversation || isPlayerChoosing) return;
        if (shouldShowText) {
            (showPlayer ? PlayerContainer : NpcContainer).SetActive(true);
            (showPlayer ? PlayerText : NpcText).gameObject.SetActive(true);
            (showPlayer ? PlayerText : NpcText).text = textToShow;
            showingText = true;
            shouldShowText = false;
        }
        if (!showingText) {
            if (DialogueSystem.IsConversationDone())
            {
                isInConversation = false;
                showingSecondaryScreen = false;
                showPlayer = false;
                isPlayerChoosing = false;
                shouldShowText = false;
                showingText = false;

                PlayerContainer.SetActive(false);
                NpcContainer.SetActive(false);
                playerInput.EnablePlayerAction();
                return;
            }

            bool isNpc = DialogueSystem.IsCurrentNpc();
            if (isNpc)
            {
                ActorData currentActor = DialogueSystem.GetCurrentActor();
                showPlayer = false;
                shouldShowText = true;
                textToShow = DialogueSystem.ProgressNpc();
                if (currentActor != null)
                {
                    NpcName.text = currentActor.Name;
                }
            }
            else
            {
                var currentLines = DialogueSystem.GetCurrentLines();
                isPlayerChoosing = true;
                PlayerContainer.SetActive(true);
                LineController.Owner = this;
                LineController.gameObject.SetActive(true);
                LineController.Initialize(currentLines);
            }
        }
    }

    public void PlayerSelect(int index) {
        LineController.gameObject.SetActive(false);
        textToShow = DialogueSystem.ProgressSelf(index);
        isPlayerChoosing = false;
        shouldShowText = true;
        showPlayer = true;
    }
    #region comments2

    //public void PlayGame(string node, int lineIndex) {
    //    showingSecondaryScreen = true;
    //    SecondaryScreen.SetActive(true);

    //    NpcContainer.SetActive(false);
    //    PlayerContainer.gameObject.SetActive(false);
    //    showingText = false;
    //    PlayerText.gameObject.SetActive(false);
    //    NpcText.gameObject.SetActive(false);
    //}

    //public void OpenShop(string node, int lineIndex) {
    //    showingSecondaryScreen = true;
    //    SecondaryScreen.SetActive(true);

    //    NpcContainer.SetActive(false);
    //    PlayerContainer.gameObject.SetActive(false);
    //    showingText = false;
    //    PlayerText.gameObject.SetActive(false);
    //    NpcText.gameObject.SetActive(false);
    //}
    #endregion
}