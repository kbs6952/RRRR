using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [Header("애니메이션 제어 변수")]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void SetAttck()
    {
        animator.SetTrigger("doAttack");
    }
    public void AnimationTest()
    {
        Debug.Log("공격 첫번째 애니메이션이 실행되었다.");
    }
}
