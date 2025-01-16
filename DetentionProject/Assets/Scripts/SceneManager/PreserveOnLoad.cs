using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreserveOnLoad : MonoBehaviour
{
    private List<string> excludeScenes = new List<string> { "MainMenu", "PlayerSelection" };

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!excludeScenes.Contains(scene.name))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && player.scene.name == scene.name)
            {
                DontDestroyOnLoad(player);
            }

            GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");
            if (soundManager != null && soundManager.scene.name == scene.name)
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
