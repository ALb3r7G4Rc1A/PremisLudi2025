using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoosScript : MonoBehaviour
{
    public Animator animator;
    public Animator animatorWhite;
    private bool isGoosLeft;

    public void Attack()
    {
        if (isGoosLeft)
        {
            animator.SetTrigger("LeftAttack");
            animatorWhite.SetTrigger("LeftAttack");
        }
        else
        {
            animator.SetTrigger("RightAttack");
            animatorWhite.SetTrigger("RightAttack");
        }
    }
    
    public void Turn(bool right)
    {
        if (isGoosLeft && right) //La paraula ve de la dreta i el gos mira esquerra
        {
            animator.SetTrigger("LeftToRightTurn");
            animatorWhite.SetTrigger("LeftToRightTurn");
             isGoosLeft = false;
        }
        else if (!isGoosLeft && !right) //La paraula ve de la esquerra i el gos mira dreta
        {
            animator.SetTrigger("RightToLeftTurn");
            animatorWhite.SetTrigger("RightToLeftTurn");
            isGoosLeft = true;
        }

    }
}
