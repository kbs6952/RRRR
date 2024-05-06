using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Common Player Data")]
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;

    [Header("플레이어 제약 조건")]
    public bool isPerformingAction = false;
    public bool applyRootMotin = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool canCombo = false;

    [Header("PlayerManager Scripts")]
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;
    [HideInInspector] public PlayerMovemnetManager playerMovemnetManager;

    private void Awake()
    {
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerMovemnetManager = GetComponent<PlayerMovemnetManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
}
