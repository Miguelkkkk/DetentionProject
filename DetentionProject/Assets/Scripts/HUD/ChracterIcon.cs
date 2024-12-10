using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChracterIcon : MonoBehaviour
{
    public Image characterIcon;
    public Sprite icon1;  
    public Sprite icon2;

    public void changeIcon(Component sender, object data)
    {
        if (data is string characterName)
        {
            switch (characterName)
            {
                case "Sofia":
                    characterIcon.sprite = icon1;
                    break;

                case "Andre":
                    characterIcon.sprite = icon2;
                    break;

                default:
                    characterIcon.sprite = null;
                    break;
            }
        }
        else
        {
            // Caso 'data' não seja uma string, trata o erro
            Debug.LogError("Data não é uma string válida.");
        }
    }

}
