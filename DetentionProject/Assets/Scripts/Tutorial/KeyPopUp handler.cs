using System.Collections.Generic;
using UnityEngine;

public class RegionTriggerUI : MonoBehaviour
{
    public GameObject uiElement;
    public GameObject toastFather;
    public List<GameObject> toasts;
    public float fadeDuration = 1f; 
    public float displayDuration = 5f; 

    private CanvasGroup canvasGroup;
    private bool hasTriggered = false;

    private void Start()
    {
        canvasGroup = uiElement.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = uiElement.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0;
        uiElement.SetActive(false);

        for (int i = 0; i < toastFather.transform.childCount; i++)
        {
            GameObject toast = toastFather.transform.GetChild(i).gameObject;
            toasts.Add(toast);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; 
            StopAllCoroutines(); 
            uiElement.SetActive(true);

            for (int i = 0; i < toasts.Count; i++) {
                if (toasts[i] != uiElement) {
                    toasts[i].gameObject.SetActive(false);
                }
               
            }
            StartCoroutine(FadeInAndOut());
        }
    }

    private System.Collections.IEnumerator FadeInAndOut()
    {
        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(displayDuration);

        yield return StartCoroutine(FadeOut());

        uiElement.SetActive(false); 
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f; 
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f; 

    }
}
