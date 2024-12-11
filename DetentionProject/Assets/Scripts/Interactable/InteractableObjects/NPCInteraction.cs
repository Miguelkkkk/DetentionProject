using System.Collections;
using UnityEngine;

public class NPCInteraction : InteractableObject
{
    public GameObject obj;
    private CanvasGroup _objCanvasGroup;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _objCanvasGroup = obj.GetComponent<CanvasGroup>(); 

        if (_objCanvasGroup == null)
        {
            _objCanvasGroup = obj.AddComponent<CanvasGroup>();
        }

        _objCanvasGroup.alpha = 0f; 
        obj.SetActive(false);
    }

    public new void Interact()
    {
        if (isInRange)
        {
            StartCoroutine(ShowAndHideObject());
        }
    }

    private IEnumerator ShowAndHideObject()
    {
        obj.SetActive(true);

        float fadeDuration = 1f; 
        float fadeSpeed = 1f / fadeDuration;

   
        while (_objCanvasGroup.alpha < 1f)
        {
            _objCanvasGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        _objCanvasGroup.alpha = 1f;

          yield return new WaitForSeconds(2f);

   
        while (_objCanvasGroup.alpha > 0f)
        {
            _objCanvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        _objCanvasGroup.alpha = 0f;
        obj.SetActive(false); 
    }

    void Update()
    {
        isInRange = GetComponentInChildren<Interactor>().isInRange;
        if (isInRange)
        {
            UpdateOutline(true);
        }
        else
        {
            UpdateOutline(false);
        }
    }
}
