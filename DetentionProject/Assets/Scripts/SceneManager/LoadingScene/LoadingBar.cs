using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image image;

    private void Awake()
    {
        image.fillAmount = 0;
    }

    void Update()
    {
        image.fillAmount = Loader.GetLoadingProgress();  
    }
}
