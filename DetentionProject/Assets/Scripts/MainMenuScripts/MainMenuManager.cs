using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    public GameObject continueButton;
    public Button continueButtonComponent; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (SaveManager.LoadPlayerData() == null)
        {
            ContinueButtonInactive();
        }
    }

    private void ContinueButtonInactive()
    {
        if (continueButton != null)
        {
            CanvasGroup canvasGroup = continueButton.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = continueButton.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0.5f;
            canvasGroup.interactable = false; 
            canvasGroup.blocksRaycasts = false; 
        }
    }

    public void Continue()
    {
        Loader.Load(Loader.Scene.Tutorial);
    }

    public void newGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
