using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public ParticleSystem slimeParticle;
    private MaterialPropertyBlock _propertyBlock;
    private Renderer _renderer;
    private Collider2D _collider;
    private Coroutine _hitEffectCoroutine;

    [Header("Flash")]
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private float cooldownDuration = 0.3f;

    [Header("Knockback")]
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.2f;

    private bool canTakeDamage = true;
    private bool isFlashing = false;

    private bool isDead = false;


    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    public bool isDeafeated() { 
        return isDead;
    }
    private void Awake()
    {
        currentHealth = maxHealth;
        _renderer = GetComponent<Renderer>();
        _animator = this.gameObject.GetComponent<Animator>();
        if (_renderer == null)
        {
            Debug.LogError("Renderer not found on object! Make sure a Renderer component is attached.");
            return;
        }

        _collider = GetComponent<Collider2D>();
        if (_collider == null)
        {
            Debug.LogError("Collider2D not found on object! Make sure a Collider2D component is attached.");
            return;
        }

        _propertyBlock = new MaterialPropertyBlock();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D == null)
        {
            Debug.LogError("Rigidbody2D not found on object! Make sure a Rigidbody2D component is attached.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackHitBox"))
        {
            if (!canTakeDamage || isFlashing) return;

            CinemachineShake.Instance.shakeCamera(6f, .2f);
            slimeParticle.Play();
            TakeDamage(1, collision.transform);

            if (_hitEffectCoroutine == null)
            {
                _hitEffectCoroutine = StartCoroutine(HitEffect());
            }
        }
    }

    private IEnumerator HitEffect()
    {
        if (_renderer == null || _collider == null) yield break;

        isFlashing = true;

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

    public void TakeDamage(int damage, Transform attacker)
    {
        if (!canTakeDamage) return;
        currentHealth -= damage;

        if (_rigidbody2D != null)
        {
            Vector2 knockbackDirection = (transform.position - attacker.position).normalized;
            _rigidbody2D.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            Debug.Log(knockbackDirection);
            StartCoroutine(DisableMovement(knockbackDuration));
        }

        if (currentHealth <= 0)
        {
            _animator.SetTrigger("Death");
            isDead = true;
            StartCoroutine(WaitAndDestroy(1f));  
        }
    }
    private IEnumerator WaitAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);   
        Destroy(gameObject);
    }
    private IEnumerator DisableMovement(float duration)
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.isKinematic = true;
        yield return new WaitForSeconds(duration);
        _rigidbody2D.isKinematic = false;
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}