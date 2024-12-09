using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour, IDamageable
{
    [Header("Input")]
    public InputReader input;

    [Header("Events")]
    public GameEvent onPlayerHealthChanged;

    [Header("Health")]
    [SerializeField] private int maxHealth;

    private bool isDead = false;

    private int currentHealth = 0;
    private bool canTakeDamage = true;

    public void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        onPlayerHealthChanged.Raise(this, currentHealth);
        CinemachineShake.Instance.shakeCamera(6f, .2f);
        if (currentHealth <= 0 && !isDead)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death() {
       Animator animator = GetComponent<Animator>();
       animator.SetTrigger("Death");
       isDead = true;
       input.Disable();

       yield return new WaitForSeconds(2);

       UnityEngine.SceneManagement.Scene currentScene = Loader.GetCurrentScene();
       Loader.Load((Loader.Scene)System.Enum.Parse(typeof(Loader.Scene), currentScene.name));
       input.Enable();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Damager"))
        {
            if (canTakeDamage)
            {
                StartCoroutine(DamageCooldown());
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        TakeDamage(1);

        canTakeDamage = false;

        yield return new WaitForSeconds(0.3f);

        canTakeDamage = true;
    }



}
