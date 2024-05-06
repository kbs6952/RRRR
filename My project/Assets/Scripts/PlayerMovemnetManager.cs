using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovemnetManager : MonoBehaviour
{
    PlayerManager player;

    [Header("플레이어 매니저 스크립트")]
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;


    [Header("플레이어 입력 제어 변수")]
    [SerializeField] private float moveSpeed;           // 플레이어의 이동력
    [SerializeField] private float jumpPower;           // 플레이어의 점프력
    [SerializeField] private float runSpeed;            // 플레이어의 달리기 속도

    [SerializeField] private CharacterController cCon;



    [Header("카메라 제어 변수")]
    [SerializeField] private float smoothRotation;      // 카메라의 자연스러운 회전을 위한 가중치      
    [SerializeField] ThirdCam thirdCam;
    Quaternion pRotation;

    [Header("점프 제어 변수")]
    [SerializeField] private float gravityModifier = 3f;    // 플레이어가 떨어지는 속도(낙하)를 제어할 변수
    [SerializeField] private Vector3 groundCheckPoint;    // 땅을 판별하기 위한 체크포인트
    [SerializeField] private float groundCheckRadius;       // 땅을 체크하는 구의 크기 
    [SerializeField] private LayerMask groundLayer;         // 체크할 레이어가 땅인지 판별하는 변수 
    private bool isGrounded;                                // true이면 점프가 가능 false이면 불가능

    private float activeMoveSpeed;                          // 실제로 플레이어가 이동할 위치를 저장할 변수
    private Vector3 movement;                               // 플레이어가 실제로 이동할 방향과 거리 값

    [Header("애니메이터")]
    private Animator playerAnimator;                        // 3D 캐릭터의 애니메이션을 실행시켜주기 위한 애니메이션
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
        Vector3 moveDirection = thirdCam.transform.forward * pInput.z + thirdCam.transform.right * pInput.x;      // 플레이어가 이동할 방향을 저장하는 변수
        moveDirection.y = 0;

        // 4. 플레이어의 이동속도를 다르게 해주는 코드 (달리기 가능)

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



        // 5. 점프를 하기위한 계산식 (중력계산.

        float yValue = movement.y;                                  // 떨어지고 있는 y의 크기를 저장
        movement = moveDirection * activeMoveSpeed;                 // 좌표에 이동할 x, 0 , z 벡터값을 저장 (y값은 위아래)
        movement.y = yValue;                                        // 중력에 계속 힘이 받도록 잃어버린 변수를 다시 불러온다.

        // 다중점프되는 상황이 발생. 해결해야됨
        GroundCheck();

        if (isGrounded)
        {
            movement.y = 0;                                     
        }
        // 점프키를 입력하여 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerAnimator.CrossFade("Jump", 0.2f);         // 두번째 매개변수 : 현재 state에서 실행하고 싶은 애니메이션을 자동으로 blend해주는 시간
            movement.y = jumpPower;
        }

        movement.y += Physics.gravity.y * gravityModifier * Time.deltaTime;



        // Character Controller를 사용하여 플레이어를 움직임
        if (moveAmount > 0)
        {
            pRotation = Quaternion.LookRotation(moveDirection);
           
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, pRotation, smoothRotation);
        cCon.Move(movement * Time.deltaTime);
        playerAnimator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);        // dempTime : 첫번째 변수(이전 값),두번째 변수(변화시키고 싶은 값),



    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius, groundLayer);    // Ground인 레이어와 반지름이 groundCheckRadius인 groundCheckPoint가 충돌하면
                                                                                                                         // True를 반환.아니면 false
        playerAnimator.SetBool("isGround", isGrounded);
    }
    private void OnDrawGizmos() // 눈에 보이지않는 땅체크 함수를 가시화하기위한 함수
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
