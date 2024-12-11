using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FadeInText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Referência ao componente TextMeshPro
    public float fadeDuration = 2f;     // Duração do fade-in

    private void Start()
    {
        // Começa com o texto invisível
        Color textColor = textMeshPro.color;
        textColor.a = 0f;
        textMeshPro.color = textColor;

        // Inicia a cor do texto gradualmente com fade-in
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Loader.Load(Loader.Scene.MainMenu);
        }
    }

    private IEnumerator FadeIn()
    {
        float timeElapsed = 0f;
        Color initialColor = textMeshPro.color;

        // Fade-in até alcançar 100% de opacidade
        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            textMeshPro.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Garante que o texto fique totalmente visível ao final
        textMeshPro.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
    }
}

