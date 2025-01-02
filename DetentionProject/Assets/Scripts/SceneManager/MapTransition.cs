using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private int spawnPoint;
    [SerializeField] private Loader.Scene _mapToGo;
    private GameObject player;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu tem a tag "Player"
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
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
}
