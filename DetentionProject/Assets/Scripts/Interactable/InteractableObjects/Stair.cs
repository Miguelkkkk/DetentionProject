using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stair : InteractableObject
{
    [SerializeField] private int spawnPoint;
    [SerializeField] private Loader.Scene _mapToGo;
    private GameObject player;

    public void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        isInRange = GetComponentInChildren<Interactor>().isInRange;
        if (isInRange && !hasInteracted)
        {
            UpdateOutline(true);
        }
        else
        {
            UpdateOutline(false);
        }
    }
    public void Climb()
    {
        if (isInRange)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Loader.Load(_mapToGo);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (player == null) return;
        string spawnPointTag = "SpawnPoint" + (int)spawnPoint;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(spawnPointTag);

        if (spawnPoints.Length > 0)
        {
            GameObject spawnObject = spawnPoints[0];
            player.transform.position = spawnObject.transform.position;
        }
    }


    public new void Interact()
    {
        Climb();
    }
}
