using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCam : MonoBehaviour
{
    [Header("ī�޶� ���� ����")]
    [SerializeField] private float distance;
    [SerializeField] private float camSpeed;
    [SerializeField] private Transform playerTr;
    [SerializeField] private int limitAngle;

    [Header("���� ����")]
    [SerializeField] private bool inverseX;
    [SerializeField] private bool inverseY;

    float rotationX;
    float rotationY;

    Vector3 camDistance;

    public Quaternion camLookRotation => Quaternion.Euler(0, rotationY, 0);
    private void Start()
    {
        
    }
    private void Update()
    {
        ThirdCamControl();
    }
    private void ThirdCamControl()
    {
        float inverseXValue = inverseX ? -1 : 1;
        float inverseYValue = inverseY ? -1 : 1;

        rotationX -= Input.GetAxis("Mouse Y") * camSpeed * inverseYValue;       // ���� �̵�
        rotationX = Mathf.Clamp(rotationX, -limitAngle, limitAngle);

        rotationY += Input.GetAxis("Mouse X") * camSpeed * inverseXValue;       // �¿� �̵�

        var playerRotation = Quaternion.Euler(rotationX, rotationY, 0);

        transform.rotation = playerRotation;

        Vector3 focusPos = playerTr.position;
        transform.position = focusPos - playerRotation * new Vector3(0, 0, distance);
        var camFoward = transform.forward;
                                 


        #region ���� § 3��Ī �ڵ�
        // x�� z���� ���󰡸��

        // camDistance = new Vector3((float)transform.position.x - playerTr.position.x, 0, (float)transform.position.z - playerTr.position.z);

        // transform.position = playerTr.position -camDistance;

        #endregion



    }
}
