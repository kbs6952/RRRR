using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    PlayerManager player;

    [Header("�ִϸ��̼� ���� ����")]
    private Animator animator;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayerTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotin = true, 
        bool canRotate = false, bool canMove = false) // �ִϸ��̼� Ŭ���� �̸��� ȣ���Ͽ� �� playerManager���� �ִϸ��̼��� ���� ȣ���� �� �ְ� ĸ��ȭ�� �Լ�
    {
        animator.CrossFade(targetAnimation,0.2f);
        player.isPerformingAction = isPerformingAction;
        player.applyRootMotin= applyRootMotin; ;
        player.canRotate= canRotate;
        player.canCombo= canMove;
    }
    
    
}
