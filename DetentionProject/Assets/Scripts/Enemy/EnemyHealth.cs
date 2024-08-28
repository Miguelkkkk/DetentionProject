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

    public void TakeDamage(int damage)
    {
        _enemyAnimator.SetTrigger("Hit");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
        print(currentHealth);
    }

    public void DamageKnockback(Vector2 direction)
    {
        Debug.Log(direction);
        _enemyRigidBody.AddForce(direction * knockbackForce);
        TakeDamage(1);
    }

    IEnumerator Death()
    {
        _enemyRigidBody.bodyType = RigidbodyType2D.Static;
        _enemyAnimator.SetTrigger("Death");
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
