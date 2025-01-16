using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RonaldaDialogue : NPCDialogue
{
    private bool IsQuestActivated;

    public bool getQuestActivated() {
        return IsQuestActivated;
    }

    public void setQuestActivated(bool state)
    {
        IsQuestActivated = state;
    }
    private void Start()
    {
        
    }
}
