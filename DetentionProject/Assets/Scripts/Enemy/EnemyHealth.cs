using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int currentHealth = 0;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(1);
        }
        if (currentHealth <= 0) 
        {
            Death();
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        print(currentHealth);
    }
 
    public void Death()
    {
        print("morreu");
    }
}
