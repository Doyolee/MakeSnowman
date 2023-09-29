using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //ȭ�� ����� ���� bool��
    bool doMovement = false;

    //ȭ�� �̵� �ӵ�
    public float panSpeed=5f;
    //ȭ���� �̵��ϴ� �����ڸ� ����
    float panBorderTickness = 10f;

    //ȭ�� Ȯ��, ��� �ӵ�
    public float scrollSpeed = 5f;

    //�ִ�,�ּ� ����
    float minY = 5f;
    float maxY = 20f;

    //ȭ�� �⺻��
    Vector3 originPosition;
    //ȭ�� �ִ� �̵� ����
    float maxTranslate = 10f;


    private void Start()
    {
        originPosition = transform.position;
    }
    void Update()
    {
        //ESC������ ȭ�� ���, ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(doMovement) doMovement = false;
            else doMovement = true;
        }

        //ȭ�� ��� �ÿ� ī�޶� �̵�X
        if (!doMovement) return;

        
        //ȭ���� �����ڸ��� ���� �ش� �������� ȭ�� �̵� && ȭ���� �ִ��� �̵��� �� �ִ� �Ÿ��� maxTranslate
        if (Input.mousePosition.y >= Screen.height - panBorderTickness&&transform.position.z-originPosition.z<=maxTranslate)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); // ��
        }
        if (Input.mousePosition.y <= panBorderTickness && originPosition.z-transform.position.z <= maxTranslate)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);  //��
        }
        if (Input.mousePosition.x >= Screen.width - panBorderTickness && transform.position.x - originPosition.x <= maxTranslate)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World); //��
        }
        if (Input.mousePosition.x <= panBorderTickness && originPosition.x-transform.position.x<= maxTranslate)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);  //��
        }

        //���콺 �ٷ� ȭ�� Ȯ��, ���
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        //scrollPos �� ���� ī�޶� ����
        Vector3 scrollPos = transform.position;

        //scroll �Է°��� �ſ� ���� ������ 1000�� ���Ѵ�.
        scrollPos.y -= scroll  *100* scrollSpeed * Time.deltaTime;

        //ȭ�� ���� ������ minY���� minX
        scrollPos.y = Mathf.Clamp(scrollPos.y,minY,maxY);

        transform.position = scrollPos;
        
    }
}
