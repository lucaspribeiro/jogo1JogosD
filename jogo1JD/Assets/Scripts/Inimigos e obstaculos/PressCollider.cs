using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class PressCollider : MonoBehaviour
{
    public BoxCollider2D collision;
    public Animator animator;
    public float pressTime;

    private void Start()
    {
        StartCoroutine(PressRoutine());
    }
    private void ActivatePress() 
    {
        collision.enabled = true;
        animator.SetTrigger("hit");
        Invoke(nameof(DeactivatePress), .25f);
    }

    private IEnumerator PressRoutine() 
    {
        while (true) 
        { 
            ActivatePress();
            yield return new WaitForSeconds(pressTime);
        }
    
    }

    private void DeactivatePress() 
    {
        collision.enabled = false;
    }
}
