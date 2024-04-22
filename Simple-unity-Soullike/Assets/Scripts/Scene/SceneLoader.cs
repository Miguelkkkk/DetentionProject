using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneLoader 
{
    public enum Scene { 
        MainMenu,
        TestScene,
    }

    public static void Load(Scene scene)
    {
        // Load the loading scene
        SceneManager.LoadScene(scene.ToString());
    }
}
