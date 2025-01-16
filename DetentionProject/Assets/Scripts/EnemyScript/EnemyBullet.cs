using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rigidBody;
    [SerializeField] private float speed;
    private Vector2 startPosition;
    private float distance;
    [SerializeField] private float maxDistance;
    [SerializeField] private List<string> tags;

    void Start()
    {
        startPosition = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rigidBody.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot - 90);
    }


    void Update()
    {
        distance = Vector2.Distance(startPosition, transform.position);
        if (distance > 20)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in tags)
        {
            if (collision.CompareTag(tag))
            {
                Destroy(gameObject);
            }
        }  
    }
}
