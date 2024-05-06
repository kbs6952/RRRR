using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovemnetManager : MonoBehaviour
{
    PlayerManager player;

    [Header("�÷��̾� �Ŵ��� ��ũ��Ʈ")]
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;


    [Header("�÷��̾� �Է� ���� ����")]
    [SerializeField] private float moveSpeed;           // �÷��̾��� �̵���
    [SerializeField] private float jumpPower;           // �÷��̾��� ������
    [SerializeField] private float runSpeed;            // �÷��̾��� �޸��� �ӵ�

    [SerializeField] private CharacterController cCon;



    [Header("ī�޶� ���� ����")]
    [SerializeField] private float smoothRotation;      // ī�޶��� �ڿ������� ȸ���� ���� ����ġ      
    [SerializeField] ThirdCam thirdCam;
    Quaternion pRotation;

    [Header("���� ���� ����")]
    [SerializeField] private float gravityModifier = 3f;    // �÷��̾ �������� �ӵ�(����)�� ������ ����
    [SerializeField] private Vector3 groundCheckPoint;    // ���� �Ǻ��ϱ� ���� üũ����Ʈ
    [SerializeField] private float groundCheckRadius;       // ���� üũ�ϴ� ���� ũ�� 
    [SerializeField] private LayerMask groundLayer;         // üũ�� ���̾ ������ �Ǻ��ϴ� ���� 
    private bool isGrounded;                                // true�̸� ������ ���� false�̸� �Ұ���

    private float activeMoveSpeed;                          // ������ �÷��̾ �̵��� ��ġ�� ������ ����
    private Vector3 movement;                               // �÷��̾ ������ �̵��� ����� �Ÿ� ��

    [Header("�ִϸ�����")]
    private Animator playerAnimator;                        // 3D ĳ������ �ִϸ��̼��� ��������ֱ� ���� �ִϸ��̼�
    // Start is called before the first frame update

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    void Start()
    {
        cCon = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleActionInput();
    }
    private void HandleMovement()
    {
        if (player.isPerformingAction) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 pInput = new Vector3(h, 0, v).normalized;
        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        Vector3 moveDirection = thirdCam.transform.forward * pInput.z + thirdCam.transform.right * pInput.x;      // �÷��̾ �̵��� ������ �����ϴ� ����
        moveDirection.y = 0;

        // 4. �÷��̾��� �̵��ӵ��� �ٸ��� ���ִ� �ڵ� (�޸��� ����)

        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = runSpeed;
            moveAmount++;
            playerAnimator.SetBool("isRun", true);
        }
        else
        {
            activeMoveSpeed = moveSpeed;
            playerAnimator.SetBool("isRun", false);
        }



        // 5. ������ �ϱ����� ���� (�߷°��.

        float yValue = movement.y;                                  // �������� �ִ� y�� ũ�⸦ ����
        movement = moveDirection * activeMoveSpeed;                 // ��ǥ�� �̵��� x, 0 , z ���Ͱ��� ���� (y���� ���Ʒ�)
        movement.y = yValue;                                        // �߷¿� ��� ���� �޵��� �Ҿ���� ������ �ٽ� �ҷ��´�.

        // ���������Ǵ� ��Ȳ�� �߻�. �ذ��ؾߵ�
        GroundCheck();

        if (isGrounded)
        {
            movement.y = 0;                                     
        }
        // ����Ű�� �Է��Ͽ� ����
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerAnimator.CrossFade("Jump", 0.2f);         // �ι�° �Ű����� : ���� state���� �����ϰ� ���� �ִϸ��̼��� �ڵ����� blend���ִ� �ð�
            movement.y = jumpPower;
        }

        movement.y += Physics.gravity.y * gravityModifier * Time.deltaTime;



        // Character Controller�� ����Ͽ� �÷��̾ ������
        if (moveAmount > 0)
        {
            pRotation = Quaternion.LookRotation(moveDirection);
           
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, pRotation, smoothRotation);
        cCon.Move(movement * Time.deltaTime);
        playerAnimator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);        // dempTime : ù��° ����(���� ��),�ι�° ����(��ȭ��Ű�� ���� ��),



    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius, groundLayer);    // Ground�� ���̾�� �������� groundCheckRadius�� groundCheckPoint�� �浹�ϸ�
                                                                                                                         // True�� ��ȯ.�ƴϸ� false
        playerAnimator.SetBool("isGround", isGrounded);
    }
    private void OnDrawGizmos() // ���� �������ʴ� ��üũ �Լ��� ����ȭ�ϱ����� �Լ�
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius);
    }

    private void HandleActionInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HandleAttackAction();
        }
    }
    private void HandleAttackAction()
    {

        player.playerAnimationManager.PlayerTargetActionAnimation("ATK0", true);

    }
   
}
