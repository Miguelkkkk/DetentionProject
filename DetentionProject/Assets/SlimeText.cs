using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlimeText : MonoBehaviour
{
    public GameObject targetObject;
    public TextMeshProUGUI textMeshPro;

    private int initialChildCount;

    void Start()
    {
        if (targetObject != null)
        {
            initialChildCount = targetObject.transform.childCount;
            UpdateText();
        }
    }

    void UpdateText()
    {
        if (targetObject != null && textMeshPro != null)
        {
            int currentChildCount = targetObject.transform.childCount;
            int remaining = initialChildCount - currentChildCount;
            if(currentChildCount <= 0) {
                Loader.Load(Loader.Scene.Thanks);
            }
            textMeshPro.text = $"{remaining}/{initialChildCount}";
        }
    }

    void Update()
    {
        UpdateText();
    }
}