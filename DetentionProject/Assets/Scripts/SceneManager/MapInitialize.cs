using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitialize : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private List<GameObject> prefabsToInstantiate; // Lista de prefabs a serem instanciados

    void Awake()
    {
        foreach (GameObject prefab in prefabsToInstantiate)
        {
            if (!IsOnscene(prefab))
            {
                if (prefab.CompareTag("Player"))
                {
                    Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
                }
                else { 
                    Instantiate(prefab, Vector3.zero, Quaternion.identity);
                }
            }
        }
    }

    private bool IsOnscene(GameObject prefab)
    {
        string prefabName = prefab.name;

        GameObject[] sceneObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in sceneObjects)
        {
            if (obj.CompareTag("Player"))
            {
                return true; 
            }
        }

        GameObject dontDestroyRoot = GetDontDestroyOnLoadRoot();
        if (dontDestroyRoot != null)
        {
            foreach (Transform child in dontDestroyRoot.transform)
            {
                if (child.name == prefabName)
                {
                    return true; 
                }
            }
        }

        return false; 
    }

    private GameObject GetDontDestroyOnLoadRoot()
    {
        GameObject temp = new GameObject();
        DontDestroyOnLoad(temp);
        GameObject dontDestroyRoot = temp.transform.parent?.gameObject;
        Destroy(temp);
        return dontDestroyRoot;
    }
}
