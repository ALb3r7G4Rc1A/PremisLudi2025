using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoosScript : MonoBehaviour
{
    public Animator animator;
    private bool isGoosLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
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
}
