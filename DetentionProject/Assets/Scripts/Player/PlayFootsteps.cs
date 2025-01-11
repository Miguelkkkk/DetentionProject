using SmallHedge.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{

    public void PlayFootSteps() {
        SoundManager.PlaySound(SoundType.Footsteps);
    }
}
