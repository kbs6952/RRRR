using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCam : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
    [SerializeField] private float smoothValue;


    Vector3 offset;

    Vector3 targetCamPos;       // ī�޶� ���������� ���������� ��ġ
    void Start()
    {
        offset = transform.position - playerTr.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        TopViewMode();
    }
    
    private void TopViewMode()
    {
        #region ī�޶� �÷��̾�� ���� �ӵ��� �̵���
        //transform.position = playerTr.position + offset;
        // offset = transform.position - playerTr.position; 
        #endregion


        #region ���������� �̿��� �ε巯�� ī�޶� �̵�
        targetCamPos = playerTr.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothValue * Time.deltaTime); 
        #endregion
    }
}
