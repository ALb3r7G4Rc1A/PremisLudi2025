using System;
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
    public GameObject drops;
    private float fillAmount;
    private bool isShineMaterialActive;
    public GameObject rightShadow;
    public GameObject leftShadow;

    void Start()
    {
        drops.SetActive(false);
    }
    void Update()
    {

        if (isShineMaterialActive && gameObject.GetComponent<SpriteRenderer>().material.GetFloat("_Fill") < 0.99f)
        {
            fillAmount = Mathf.Lerp(gameObject.GetComponent<SpriteRenderer>().material.GetFloat("_Fill"), 1, Time.deltaTime * 4);
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Fill", fillAmount);
        }
        else
        {
            rightShadow.SetActive(false);
            leftShadow.SetActive(false);
        }
    }
    public void Attack(bool goodAttack)
    {
        if (goodAttack)
        {
            drops.SetActive(false);
        }
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
    public void Shine()
    {
        if (!isShine)
        {
            animator.SetTrigger("Streak");
            isShine = true;
            gameObject.GetComponent<SpriteRenderer>().material = shineMaterial;
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Fill", 0f);
            isShineMaterialActive = true;
            if (isGoosLeft)
            {
                leftShadow.SetActive(true);
            }
            else
            {
                rightShadow.SetActive(true);
            }
        }
    }
    public void Hitted(int badStreak)
    {
        if (badStreak == 3)
        {
            drops.SetActive(true);
        }
        animator.SetTrigger("Hitted");
        isShine = false;
        gameObject.GetComponent<SpriteRenderer>().material = normalMaterial;
        isShineMaterialActive = false;
    }
}
