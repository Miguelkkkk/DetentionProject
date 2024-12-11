using System.Collections;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    private MaterialPropertyBlock _propertyBlock;
    private Renderer _renderer;
    private Collider2D _collider; // Armazena o collider para controle
    private Coroutine _hitEffectCoroutine;

    [Header("Flash")]
    [SerializeField] private float flashDuration = 0.1f; // Duração do flash
    [SerializeField] private float cooldownDuration = 0.3f; // Duração do cooldown entre os flashes

    private bool canTakeDamage = true;
    private bool isFlashing = false; // Controla se o efeito de flash está em andamento

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        // Verificar e obter o Renderer
        _renderer = GetComponent<Renderer>();
        if (_renderer == null)
        {
            Debug.LogError("Renderer not found on object! Make sure a Renderer component is attached.");
            return;
        }

        // Verificar e obter o Collider2D
        _collider = GetComponent<Collider2D>();
        if (_collider == null)
        {
            Debug.LogError("Collider2D not found on object! Make sure a Collider2D component is attached.");
            return;
        }

        // Inicializar o MaterialPropertyBlock
        _propertyBlock = new MaterialPropertyBlock();

        // Inicializar o SpriteRenderer
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackHitBox"))
        {
            if (!canTakeDamage || isFlashing) return; // Impede dano se já estiver em cooldown ou flashing

            CinemachineShake.Instance.shakeCamera(6f, .2f);

            // Inverter a imagem
            InvertImage();

            if (_hitEffectCoroutine == null)
            {
                _hitEffectCoroutine = StartCoroutine(HitEffect());
            }
        }
    }

    private IEnumerator HitEffect()
    {
        // Começar o efeito de flash
        if (_renderer == null || _collider == null) yield break; // Verifica se os componentes ainda são válidos

        isFlashing = true; // Marca que o efeito está ocorrendo

        float elapsedTime = 0f;

        // Desabilitar o Collider temporariamente para impedir novos hits
        _collider.enabled = false;

        while (elapsedTime < flashDuration)
        {
            float t = elapsedTime / flashDuration;
            ApplyFlashEffect(Mathf.Lerp(0f, 1f, t)); // Aumenta a intensidade do flash
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Devolver ao estado normal
        ApplyFlashEffect(0f);

        // Habilitar o Collider novamente após o efeito de dano
        _collider.enabled = true;

        // Aguardar o cooldown antes de permitir outro flash
        yield return new WaitForSeconds(cooldownDuration);

        // Finaliza o efeito de flash
        isFlashing = false;
        _hitEffectCoroutine = null;
    }

    private void ApplyFlashEffect(float hitEffectAmount)
    {
        if (_renderer == null) return; // Verifica se o Renderer ainda é válido

        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetFloat("_HitEffectAmount", hitEffectAmount); // Supondo que o shader tenha essa propriedade
        _renderer.SetPropertyBlock(_propertyBlock);
    }

    private void InvertImage()
    {
        if (_spriteRenderer != null)
        {
            // Inverter a imagem horizontalmente (flipX)
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}
