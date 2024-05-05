using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Common Player Data")]
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;

    [Header("�÷��̾� ���� ����")]
    public bool isPerformingAction = false;
    public bool applyRootMotin = false;
}
