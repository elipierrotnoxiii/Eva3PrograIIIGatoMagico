using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimController : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void Walk(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }

    public void Dead()
    {
        animator.SetInteger("DeadType", Random.Range(0, 2));
        animator.SetBool("Dead", true);
    }

    public void Jump(bool isJumping)
    {
        animator.SetBool("Jump",isJumping);
    }

    public void Reset()
    {
        animator.SetTrigger("Reset");
    }
}
