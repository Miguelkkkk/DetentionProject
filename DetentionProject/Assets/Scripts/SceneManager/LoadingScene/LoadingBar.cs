using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image image;

    void Update()
    {
        image.fillAmount = Loader.GetLoadingProgress();  
    }
}
