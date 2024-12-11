using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int currentHealth = 0;

    [SerializeField] public float knockbackForce;

    [SerializeField] private Rigidbody2D _enemyRigidBody;
    [SerializeField] private Animator _enemyAnimator;

    [Header("Flash")]
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private float cooldownDuration = 0.3f;

    private MaterialPropertyBlock _propertyBlock;
    private Renderer _renderer;
    private Coroutine _hitEffectCoroutine;
    private bool isFlashing = false;

    private static readonly int HitTrigger = Animator.StringToHash("Hit");
    private static readonly int DeathTrigger = Animator.StringToHash("Death");

    void Awake()
    {
        currentHealth = maxHealth;
        _enemyAnimator = GetComponent<Animator>();
        _enemyRigidBody = GetComponent<Rigidbody2D>();

        _renderer = GetComponent<Renderer>();
        if (_renderer == null)
        {
            Debug.LogError("Renderer not found on object! Make sure a Renderer component is attached.");
            return;
        }

        _propertyBlock = new MaterialPropertyBlock();
    }

    public void TakeDamage(int damage)
    {
        if (isFlashing) return;

        _enemyAnimator.SetTrigger(HitTrigger);
        currentHealth -= damage;

        if (_hitEffectCoroutine == null)
        {
            _hitEffectCoroutine = StartCoroutine(HitEffect());
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
        print(currentHealth);
    }

    public void DamageKnockback(Vector2 direction)
    {
        if (_enemyRigidBody != null)
        {
            _enemyRigidBody.AddForce(direction * knockbackForce);
        }
        TakeDamage(1);
    }

    IEnumerator Death()
    {
        _enemyRigidBody.bodyType = RigidbodyType2D.Static;
        _enemyAnimator.SetTrigger(DeathTrigger);
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackHitBox"))
        {
            TakeDamage(1);
        }
    }

    private IEnumerator HitEffect()
    {
        isFlashing = true;

        float elapsedTime = 0f;

        while (elapsedTime < flashDuration)
        {
            float t = elapsedTime / flashDuration;
            ApplyFlashEffect(Mathf.Lerp(0f, 1f, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ApplyFlashEffect(0f);

        yield return new WaitForSeconds(cooldownDuration);

        isFlashing = false;
        _hitEffectCoroutine = null;
    }

    private void ApplyFlashEffect(float hitEffectAmount)
    {
        if (_renderer == null) return;

        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetFloat("_HitEffectAmount", hitEffectAmount);
        _renderer.SetPropertyBlock(_propertyBlock);
    }
}