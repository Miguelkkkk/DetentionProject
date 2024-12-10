using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
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

    private SpriteRenderer spriteRenderer;
    private Material flashMaterial;

    [SerializeField] private float flashDuration = 0.1f;

    public void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Clone o material para evitar alterações em outros objetos
            flashMaterial = Instantiate(spriteRenderer.material);
            spriteRenderer.material = flashMaterial;
        }
    }


    public void TakeDamage(int damage)
    {
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
        input.Disable();

        yield return new WaitForSeconds(2);

        UnityEngine.SceneManagement.Scene currentScene = Loader.GetCurrentScene();
        Loader.Load((Loader.Scene)Enum.Parse(typeof(Loader.Scene), currentScene.name));
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

    private IEnumerator FlashEffect()
    {
        if (flashMaterial != null)
        {
            // Define a duração do flash como 0.1 segundos
            float elapsedTime = 0f;

            // Cria o efeito de flash usando Lerp para aumentar e diminuir
            while (elapsedTime < flashDuration)
            {
                float t = elapsedTime / flashDuration;
                // Interpola o valor entre 0 e 1, para um efeito de flash
                flashMaterial.SetFloat("_HitEffectAmount", Mathf.Lerp(0f, 1f, t));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Retorna ao valor inicial após o efeito
            flashMaterial.SetFloat("_HitEffectAmount", 0f);
        }
    }




    private IEnumerator DamageCooldown()
    {
        TakeDamage(1);

        canTakeDamage = false;

        yield return new WaitForSeconds(0.3f);

        canTakeDamage = true;
    }

    void StopVibration()
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0f, 0f); // Para os motores
        }
    }
}
