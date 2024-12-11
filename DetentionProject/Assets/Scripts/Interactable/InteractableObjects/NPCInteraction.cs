using System.Collections;
using UnityEngine;

public class NPCInteraction : InteractableObject
{
    public GameObject obj;
    private CanvasGroup _objCanvasGroup;
    private new bool isInRange;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _objCanvasGroup = obj.GetComponent<CanvasGroup>(); 

        if (_objCanvasGroup == null)
        {
            // If no CanvasGroup is found, add one.
            _objCanvasGroup = obj.AddComponent<CanvasGroup>();
        }

        _objCanvasGroup.alpha = 0f; // Initializes the object as invisible.
        obj.SetActive(false); // Initially deactivate the object.
    }

    public new void Interact()
    {
        StartCoroutine(ShowAndHideObject());
    }

    private IEnumerator ShowAndHideObject()
    {
        obj.SetActive(true); // Make the object active.

        float fadeDuration = 1f; // Duration for the object to appear.
        float fadeSpeed = 1f / fadeDuration;

        // Gradually appear the object (fade in).
        while (_objCanvasGroup.alpha < 1f)
        {
            _objCanvasGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        _objCanvasGroup.alpha = 1f;

        // Wait for 3 seconds before disappearing.
        yield return new WaitForSeconds(3f);

        // Gradually disappear the object (fade out).
        while (_objCanvasGroup.alpha > 0f)
        {
            _objCanvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        _objCanvasGroup.alpha = 0f;
        obj.SetActive(false); // Deactivate the object after it disappears.
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
