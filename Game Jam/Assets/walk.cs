using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{

    Animator animator;
    public string animationName;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayAnimation();
    }


    void PlayAnimation()
    {
        animator.Play("walk");
        }
}