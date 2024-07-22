using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    [Header("Spawn To")]
    [SerializeField] private SceneField _sceneLoad;

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

    public void Play() {
        SceneManager.LoadScene(_sceneLoad);
    }

    public void ExitApplication() { 
        Application.Quit();
    }

}
