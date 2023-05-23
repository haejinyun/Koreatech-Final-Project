using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public bool isDrag = false;
    private Vector3 offset;
    private float distance = 13f;
    private Transform controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ��Ŭ�� ��
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ����ĳ��Ʈ�� ���콺 �����Ϳ��� �߻�
            RaycastHit click;

            if (Physics.Raycast(ray, out click) && click.transform.CompareTag("Controller")) // ����ĳ��Ʈ�� ���� ������Ʈ ����
            {
                controller = click.transform; // ������Ʈ ����
                offset = controller.position - ray.GetPoint(distance); // �Ÿ� ����
                isDrag = true;
            }
        }

        if (Input.GetMouseButtonUp(0)) // ���콺 ��Ŭ�� ����
        {
            isDrag = false;
            controller = null;
        }

        if (isDrag && controller != null) // ������Ʈ�� ���� ���� ��
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ����ĳ��Ʈ �߻�
            Vector3 pos = ray.GetPoint(distance) + offset; // �Ÿ� ����
            controller.position = pos; // ��ġ ��ȯ
        }
    }
}