using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCam : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
    [SerializeField] private float smoothValue;


    Vector3 offset;

    Vector3 targetCamPos;       // 카메라가 최종적으로 움직여야할 위치
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
        #region 카메라가 플레이어와 같은 속도로 이동함
        //transform.position = playerTr.position + offset;
        // offset = transform.position - playerTr.position; 
        #endregion


        #region 선형보간을 이용한 부드러운 카메라 이동
        targetCamPos = playerTr.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothValue * Time.deltaTime); 
        #endregion
    }
}
