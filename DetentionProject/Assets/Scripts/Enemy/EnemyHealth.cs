using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Idamageble : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int currentHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
            CinemachineShake.Instance.shakeCamera(6f, .2f);
        }
        if (maxHealth == 0) 
        {
            Death();
        }
    }
    void IDamageable.TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    void TestDefense(int damage, int defense)
    {
        if (damage > defense)
        {
            TakeDamage(damage);
        }
    }
    void Death()
    {
        print("morreu");
    }
}
