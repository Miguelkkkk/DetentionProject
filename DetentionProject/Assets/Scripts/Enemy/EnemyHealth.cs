using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int currentHealth = 0;

    [SerializeField] public float knockbackForce;

    private Rigidbody2D _enemyRigidBody;
    private Animator _enemyAnimator;

    void Awake()
    {
        currentHealth = maxHealth;
        _enemyAnimator = GetComponent<Animator>();
        _enemyRigidBody = GetComponent<Rigidbody2D>();
     
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(1);
        }
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        print(currentHealth);
    }

    IEnumerator Death()
    {
        _enemyRigidBody.bodyType = RigidbodyType2D.Static;
        _enemyAnimator.SetTrigger("Death");
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("AttackHitBox"))
        {
            _enemyAnimator.SetTrigger("Hit");

            Vector2 direction = (transform.position - collision.transform.position).normalized;
            _enemyRigidBody.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            currentHealth -= 1;
            if (currentHealth <= 0)
            {
                StartCoroutine(Death());
            }
        }
    }
}
