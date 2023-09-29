using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //화면 잠금을 위한 bool값
    bool doMovement = false;

    //화면 이동 속도
    public float panSpeed=5f;
    //화면이 이동하는 가장자리 범위
    float panBorderTickness = 10f;

    //화면 확대, 축소 속도
    public float scrollSpeed = 5f;

    //최대,최소 높이
    float minY = 5f;
    float maxY = 20f;

    //화면 기본값
    Vector3 originPosition;
    //화면 최대 이동 범위
    float maxTranslate = 10f;


    private void Start()
    {
        originPosition = transform.position;
    }
    void Update()
    {
        //ESC누르면 화면 잠금, 해제
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(doMovement) doMovement = false;
            else doMovement = true;
        }

        //화면 잠금 시에 카메라 이동X
        if (!doMovement) return;

        
        //화면의 가장자리로 가면 해당 방향으로 화면 이동 && 화면을 최대한 이동할 수 있는 거리는 maxTranslate
        if (Input.mousePosition.y >= Screen.height - panBorderTickness&&transform.position.z-originPosition.z<=maxTranslate)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); // 상
        }
        if (Input.mousePosition.y <= panBorderTickness && originPosition.z-transform.position.z <= maxTranslate)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);  //하
        }
        if (Input.mousePosition.x >= Screen.width - panBorderTickness && transform.position.x - originPosition.x <= maxTranslate)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World); //우
        }
        if (Input.mousePosition.x <= panBorderTickness && originPosition.x-transform.position.x<= maxTranslate)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);  //좌
        }

        //마우스 휠로 화면 확대, 축소
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        //scrollPos 는 현재 카메라 높이
        Vector3 scrollPos = transform.position;

        //scroll 입력값은 매우 낮기 때문에 1000을 곱한다.
        scrollPos.y -= scroll  *100* scrollSpeed * Time.deltaTime;

        //화면 높이 범위는 minY부터 minX
        scrollPos.y = Mathf.Clamp(scrollPos.y,minY,maxY);

        transform.position = scrollPos;
        
    }
}
