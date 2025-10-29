using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoosScriptTutorial : MonoBehaviour
{
    public Animator animator;
    private bool isGoosLeft;

    public void Attack(bool goodAttack)
    {
        animator.SetBool("GoodAttack", goodAttack);
        if (isGoosLeft)
        {
            animator.SetTrigger("LeftAttack");
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
    public void Hitted()
    {
        animator.SetTrigger("Hitted");
    }
}
