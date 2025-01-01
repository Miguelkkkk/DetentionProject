using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private Loader.Scene _mapToGo;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Loader.Load(_mapToGo);
    }
}
