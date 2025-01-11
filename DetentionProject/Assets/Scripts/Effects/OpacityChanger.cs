using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityChanger : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float _opacity = 0.7f;
    [SerializeField] private float _opacityfadeTime = .4f;

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeTree(_spriteRenderer, _opacityfadeTime, _spriteRenderer.color.a, _opacity));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeTree(_spriteRenderer, _opacityfadeTime, _spriteRenderer.color.a, 1f));
        }
    }
    private IEnumerator FadeTree(SpriteRenderer _spriteTransparency, float _fadeTime, float _startValue, float _targetTransparency)
    {
        float _timeElapsed = 0;
        while (_timeElapsed < _fadeTime)
        {
            _timeElapsed += Time.deltaTime;
            float _newAlpha = Mathf.Lerp(_startValue, _targetTransparency, _timeElapsed / _fadeTime);
            _spriteTransparency.color = new Color(_spriteTransparency.color.r, _spriteTransparency.color.g, _spriteTransparency.color.b, _newAlpha);
            yield return null;
        }
    }
}
