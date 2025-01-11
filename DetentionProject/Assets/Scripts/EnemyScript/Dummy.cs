using UnityEngine;

public class Dummy : EnemyLife
{
    private void InvertImage()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }

    public override void TakeDamage(int amount, Vector2 knockbackDirection)
    {
        if (!canTakeDamage || isFlashing || currentHealth <= 0) return;

        currentHealth -= amount;
        CinemachineShake.Instance.shakeCamera(6f, 0.2f);

        if (_hitEffectCoroutine == null)
        {
            _hitEffectCoroutine = StartCoroutine(HitEffect());
        }

        ApplyKnockback(knockbackDirection);

        InvertImage();
    }
}
