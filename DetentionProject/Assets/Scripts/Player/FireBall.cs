using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Para Light2D

public class FireBall : MonoBehaviour
{
    [Header("Fade Out Settings")]
    public float lifetime = 5f;
    public float fadeDuration = 1f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private ParticleSystem childParticleSystem;
    private Light2D childLight2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        childParticleSystem = GetComponentInChildren<ParticleSystem>();
        childLight2D = GetComponentInChildren<Light2D>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        StartCoroutine(DestroyAfterLifetime());
    }

    IEnumerator DestroyAfterLifetime()
    {
        if (childParticleSystem != null)
        {
            childParticleSystem.Stop();
        }
        yield return new WaitForSeconds(lifetime - fadeDuration);

        // Fade out do SpriteRenderer
        if (spriteRenderer != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                float t = elapsedTime / fadeDuration;
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1f, 0f, t));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        // Fade out do ParticleSystem
        if (childParticleSystem != null)
        {
            var mainModule = childParticleSystem.main;
            float initialAlpha = mainModule.startColor.color.a;

            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                float t = elapsedTime / fadeDuration;
                Color color = mainModule.startColor.color;
                color.a = Mathf.Lerp(initialAlpha, 0f, t);
                mainModule.startColor = new ParticleSystem.MinMaxGradient(color);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            childParticleSystem.Stop();
        }

        // Fade out da Light2D
        if (childLight2D != null)
        {
            float initialIntensity = childLight2D.intensity;

            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                float t = elapsedTime / fadeDuration;
                childLight2D.intensity = Mathf.Lerp(initialIntensity, 0f, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            childLight2D.intensity = 0f;
        }

        // Garante que o SpriteRenderer fique invisível no final
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        }

        Destroy(gameObject);
    }
}
