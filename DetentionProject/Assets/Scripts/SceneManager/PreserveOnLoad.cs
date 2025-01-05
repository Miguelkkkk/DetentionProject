using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreserveOnLoad : MonoBehaviour
{
    private List<string> excludeScenes = new List<string> { "MainMenu", "PlayerSelection" };

    void Awake()
    {
        if (!excludeScenes.Contains(SceneManager.GetActiveScene().name))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                DontDestroyOnLoad(player);
            }

            GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");
            if (soundManager != null)
            {
                DontDestroyOnLoad(soundManager);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
