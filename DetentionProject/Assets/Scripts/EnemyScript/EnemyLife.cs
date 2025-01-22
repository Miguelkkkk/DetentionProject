using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLife : MonoBehaviour, IDamageable
{
    [Header("General Settings")]
    [SerializeField] protected int maxHealth = 100;

    protected Animator _animator;

    protected float currentHealth;
    protected MaterialPropertyBlock _propertyBlock;
    protected Renderer _renderer;
    protected Collider2D _collider;
    protected Coroutine _hitEffectCoroutine;

    [Header("Flash Effect")]
    [SerializeField] protected float flashDuration = 0.1f;
    [SerializeField] protected float cooldownDuration = 0.3f;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.2f;


    protected bool canTakeDamage = true;
    protected bool isFlashing = false;

    protected SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rigidbody;

    protected virtual void Awake()
    {
        GetComponents();
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackHitBox"))
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            TakeDamage(1, knockbackDirection);
        }
        if (collision.CompareTag("FireBall"))
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            TakeDamage(2, knockbackDirection * 2);
        }
    }

    protected void GetComponents()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_renderer != null)
        {
            _propertyBlock = new MaterialPropertyBlock();
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void TakeDamage(int amount, Vector2 knockbackDirection)
    {
        if (!canTakeDamage || isFlashing || currentHealth <= 0) return;

        currentHealth -= amount;
        CinemachineShake.Instance.shakeCamera(6f, 0.2f);

        if (_hitEffectCoroutine == null)
        {
            _hitEffectCoroutine = StartCoroutine(HitEffect());
        }

        ApplyKnockback(knockbackDirection);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected void ApplyKnockback(Vector2 direction)
    {
        if (_rigidbody == null) return;
        _rigidbody.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(ResetVelocityAfterKnockback());
    }

    private IEnumerator ResetVelocityAfterKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        if (_rigidbody != null)
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }


    protected virtual void Die()
    {
        StartCoroutine(DestroyAfterSeconds(0.6f));
        _animator.SetTrigger("Death");
    }

    protected IEnumerator DestroyAfterSeconds(float seconds) 
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    protected IEnumerator HitEffect()
    {
        if (_renderer == null || _collider == null) yield break;

        isFlashing = true;
        canTakeDamage = false;

        float elapsedTime = 0f;

        _collider.enabled = false;

        while (elapsedTime < flashDuration)
        {
            float t = elapsedTime / flashDuration;
            ApplyFlashEffect(Mathf.Lerp(0f, 1f, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ApplyFlashEffect(0f);
        _collider.enabled = true;

        yield return new WaitForSeconds(cooldownDuration);

        canTakeDamage = true;
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
