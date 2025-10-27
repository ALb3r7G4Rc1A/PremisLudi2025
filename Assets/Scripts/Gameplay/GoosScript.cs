using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoosScript : MonoBehaviour
{
    public Animator animator;
    private bool isGoosLeft;
    private bool isShine = false;
    public Material shineMaterial;
    public Material normalMaterial;

    public void Attack(bool sino)
    {
        if (isGoosLeft)
        {
            if (sino)
            {
                animator.SetTrigger("LeftAttackGood");
            }
            else
            {
                animator.SetTrigger("LeftAttackBad");
            }
        }
        else
        {
            animator.SetTrigger("RightAttack");
        }
    }

    public void Turn(bool right)
    {
        if (isGoosLeft && right) //La paraula ve de la dreta i el gos mira esquerra
        {
            animator.SetTrigger("LeftToRightTurn");
            isGoosLeft = false;
        }
        else if (!isGoosLeft && !right) //La paraula ve de la esquerra i el gos mira dreta
        {
            animator.SetTrigger("RightToLeftTurn");
            isGoosLeft = true;
        }

    }
    public void Shine()
    {
        if (!isShine)
        {
            Debug.Log("SHINE");
            animator.SetTrigger("Streak");
            isShine = true;
            gameObject.GetComponent<SpriteRenderer>().material = shineMaterial;
        }
    }
    public void Hitted()
    {
        animator.SetTrigger("Hitted");
        isShine = false;
        gameObject.GetComponent<SpriteRenderer>().material = normalMaterial;
    }
}
