using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public Color higlightedColor;
    public GameObject pauseMenuUI;
    public Button resumeButton;
    public Button optionsButton;
    public Button mainMenuButton;

    [Header("InputReader")]
    public PlayerInputReader playerInput;
    public UIInputReader input;

    private int currentSelection = 0;
    private Button[] menuButtons;
    private TextMeshProUGUI[] buttonTexts;

    private void OnEnable()
    {
        input.ConfirmEvent += OnConfirm;
        input.UpEvent += OnUp;
        input.DownEvent += OnDown;
        input.OpenEvent += OnOpen;
    }
    private void OnDisable()
    {
        input.ConfirmEvent -= OnConfirm;
        input.UpEvent -= OnUp;
        input.DownEvent -= OnDown;
        input.OpenEvent -= OnOpen;
    }

    private void OnConfirm() {
        SelectOption();
    }

    private void OnOpen()
    {
        TogglePauseMenu();
    }

    private void OnUp()
    {
        if (pauseMenuUI.activeSelf)
        {
            currentSelection = Mathf.Max(0, currentSelection - 1);
            UpdateButtonSelection();
        }
    }
    private void OnDown()
    {
        if (pauseMenuUI.activeSelf) {   
            currentSelection = Mathf.Min(menuButtons.Length - 1, currentSelection + 1);
            UpdateButtonSelection();
        }
    }

    private void Start()
    {
        menuButtons = new Button[] { resumeButton, optionsButton, mainMenuButton };

        buttonTexts = new TextMeshProUGUI[menuButtons.Length];
        for (int i = 0; i < menuButtons.Length; i++)
        {
            buttonTexts[i] = menuButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            ConfigureButton(menuButtons[i], buttonTexts[i], i);
        }

        pauseMenuUI.SetActive(false);
        playerInput.EnablePlayerAction();
    }

    public void TogglePauseMenu()
    {
        if (optionsPanel.activeSelf)
        {
            return;
        }

        bool isActive = pauseMenuUI.activeSelf;
        pauseMenuUI.SetActive(!isActive);

        if (!isActive)
        {
            playerInput.DisablePlayerAction();
            Time.timeScale = 0f;
            UpdateButtonSelection();
        }
        else
        {
            playerInput.EnableAcionMap();
            Time.timeScale = 1f;
        }
    }

    private void UpdateButtonSelection()
    {

        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (i == currentSelection)
            {
                buttonTexts[i].color = higlightedColor;
            }
            else
            {
                buttonTexts[i].color = Color.white; 
            }
        }

        menuButtons[currentSelection].Select();
    }

    private void SelectOption()
    {
        switch (currentSelection)
        {
            case 0:
                ResumeGame();
                break;
            case 1:
                OpenOptions();
                break;
            case 2:
                ReturnToMainMenu();
                break;
        }
    }

    private void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        playerInput.EnablePlayerAction();
        Time.timeScale = 1f;
    }

    private void OpenOptions()
    {
        optionsPanel.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        playerInput.EnablePlayerAction();
        Loader.Load(Loader.Scene.MainMenu);
    }

    private void ConfigureButton(Button button, TextMeshProUGUI buttonText, int index)
    {
        button.onClick.AddListener(() =>
        {
            currentSelection = index;
            SelectOption();
        });

        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
        AddEvent(trigger, EventTriggerType.PointerEnter, () =>
        {
            currentSelection = index;
            UpdateButtonSelection();
        });
        AddEvent(trigger, EventTriggerType.PointerExit, () =>
        {
            buttonText.color = Color.white; 
        });
    }

    private void AddEvent(EventTrigger trigger, EventTriggerType eventType, System.Action action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType; 
        entry.callback.AddListener((_) => action());
        trigger.triggers.Add(entry);
    }

    public void CloseOptionsPanel()
    {
        optionsPanel.SetActive(false); 
        pauseMenuUI.SetActive(true);  
        currentSelection = 0;        
        UpdateButtonSelection();    
    }
}
