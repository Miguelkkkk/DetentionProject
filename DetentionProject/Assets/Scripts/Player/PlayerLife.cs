using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour, IDamageable
{
    [Header("Input")]
    public PlayerInputReader input;

    [Header("Events")]
    public GameEvent onPlayerHealthChanged;

    [Header("Health")]
    [SerializeField] private int maxHealth;

    [Header("Flash")]
    [SerializeField] private float flashDuration = 0.1f;

    private bool isInTrigger = false;

    private bool isDead = false;

    private int currentHealth = 0;
    private bool canTakeDamage = true;

    private SpriteRenderer spriteRenderer;
    private Material flashMaterial;

    private Rigidbody2D _playerRigidBody;



    public void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Clone o material para evitar alterações em outros objetos
            flashMaterial = Instantiate(spriteRenderer.material);
            spriteRenderer.material = flashMaterial;
        }
    }


    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        if (isDead) return; 

        currentHealth -= damage;
        onPlayerHealthChanged.Raise(this, currentHealth);
        CinemachineShake.Instance.shakeCamera(6f, .2f);
        StartCoroutine(FlashEffect());

        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0.3f, 0.3f);
            Invoke(nameof(StopVibration), 0.3f);
        }

        if (currentHealth <= 0 && !isDead)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Death");
        isDead = true;
        canTakeDamage = false;
        input.DisablePlayerAction();
        _playerRigidBody.bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(2);

        UnityEngine.SceneManagement.Scene currentScene = Loader.GetCurrentScene();
        Loader.Load((Loader.Scene)Enum.Parse(typeof(Loader.Scene), currentScene.name));
        input.EnablePlayerAction();
    }

    public void TriggerStay(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Damager") && canTakeDamage && !isDead)
        {
            if (!isInTrigger)
            {
                StartCoroutine(DamageCooldown());
            }
        }
        if (collision.gameObject.CompareTag("Enemy") && canTakeDamage && !isDead)
        {
            if (!isInTrigger)
            {
                StartCoroutine(DamageCooldown());
            }
        }
    }

    public void TriggerEnter(Collider2D collision) {
        if (collision.gameObject.CompareTag("EnemyBullet") && canTakeDamage && !isDead)
        {
            TakeDamage(1,Vector2.zero);
        }
    }

    public void TriggerExit(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Damager") ||
            collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("EnemyBullet"))
        {
            isInTrigger = false;

            if (DamageCooldown() != null)
            {
                StopCoroutine(DamageCooldown());
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        isInTrigger = true;

        while (isInTrigger && canTakeDamage && !isDead)
        {
            TakeDamage(1, Vector2.zero);
            canTakeDamage = false;

            yield return new WaitForSeconds(0.3f);
            canTakeDamage = true;

            if (!isInTrigger || isDead)
            {
                break; 
            }
        }

        isInTrigger = false;
    }



    private IEnumerator FlashEffect()
    {
        if (flashMaterial != null)
        {
            float elapsedTime = 0f;

            while (elapsedTime < flashDuration)
            {
                float t = elapsedTime / flashDuration;
                flashMaterial.SetFloat("_HitEffectAmount", Mathf.Lerp(0f, 1f, t));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            flashMaterial.SetFloat("_HitEffectAmount", 0f);
        }
    }
    void StopVibration()
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0f, 0f); // Para os motores
        }
    }
}
