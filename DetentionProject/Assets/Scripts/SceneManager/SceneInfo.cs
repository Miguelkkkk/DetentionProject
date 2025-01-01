using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfo : MonoBehaviour
{
    public Vector2 entranceDirection;

    public Vector2 startingPosition;

    public SceneInfo(Vector2 entranceDirection, Vector2 startingPosition)
    {
        this.entranceDirection = entranceDirection;
        this.startingPosition = startingPosition;
    }   
}
