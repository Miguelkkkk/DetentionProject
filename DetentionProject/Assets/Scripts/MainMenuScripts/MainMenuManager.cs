using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    
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
    }

    public void Continue() {
        Loader.Load(Loader.Scene.TestScene);
    }

    public void newGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitApplication() { 
        Application.Quit();
    }

}
