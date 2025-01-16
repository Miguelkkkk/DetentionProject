using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ChracterText : MonoBehaviour
{
    public TextMeshProUGUI chracterText;

    public void changeIcon(Component sender, object data)
    {
        if (data is string characterName)
        {   
            chracterText.text = characterName;   
        }
    }
}
