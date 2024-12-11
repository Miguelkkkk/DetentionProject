using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : InteractableObject
{
    public Animator newAnimator;
    private void Awake()
    {
        _animator = newAnimator;
        _renderer = GetComponent<SpriteRenderer>();
    }

    public new void Interact()
    {
        if (isInRange && !hasInteracted) { 
            _animator.SetTrigger("Open");
            hasInteracted = true;
            StartCoroutine(WaitAndLoadScene(2f)); 
        }
    }

    private IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); 
        Loader.Load(Loader.Scene.Thanks); 
    }

    void Update()
    {
        isInRange = GetComponentInChildren<Interactor>().isInRange;
        if (isInRange && !hasInteracted)
        {
            UpdateOutline(true);
        }
        else
        {
            UpdateOutline(false);
        }
    }
}
