using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCam : MonoBehaviour
{
    [SerializeField] private Transform viewPort;
    [SerializeField] private float mouseSpeed;
    [SerializeField] private bool invertMouse;
    [SerializeField] private int limitAngle = 60;
    [SerializeField] private Transform firstCam;
    [SerializeField] private float smoothValue;



    float limitMouse;
    Vector2 mouseInput;


    float rotationX;
    float rotationY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float invertValue = invertMouse ? -1 : 1;

        rotationX -= Input.GetAxis("Mouse Y") * mouseSpeed * invertValue;       // 상하 이동
        rotationX = Mathf.Clamp(rotationX, -limitAngle, limitAngle);            

        rotationY += Input.GetAxis("Mouse X") * mouseSpeed;                     // 좌우 이동


        // 상하 이동
        viewPort.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // 좌우 이동
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotationY, transform.rotation.eulerAngles.z);


        
       
        //// 좌우 이동
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
        //    transform.rotation.eulerAngles.y+ mouseInput.x,
        //    transform.rotation.eulerAngles.z);

        //// 상하 이동
        //limitMouse -= mouseInput.y;
        //limitMouse = Mathf.Clamp(limitMouse, -limitAngle, limitAngle);

        //viewPort.rotation = Quaternion.Euler(limitMouse,
        //   viewPort.rotation.eulerAngles.y,
        //   viewPort.rotation.eulerAngles.z);

       
  
    }
    private void LateUpdate()
    {
        //선형보간법으로 부드럽게 움직여보기
        // 선형보간을 이용한 부드러운 1인칭시점이동
        firstCam.position = Vector3.Lerp(firstCam.position, viewPort.position, smoothValue * Time.deltaTime);
        firstCam.rotation = Quaternion.Lerp(firstCam.rotation,viewPort.rotation, smoothValue * Time.deltaTime);
    }
}
