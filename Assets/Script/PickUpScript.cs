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
        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 레이캐스트를 마우스 포인터에서 발사
            RaycastHit click;

            if (Physics.Raycast(ray, out click) && click.transform.CompareTag("Controller")) // 레이캐스트에 닿은 오브젝트 구분
            {
                controller = click.transform; // 오브젝트 집기
                offset = controller.position - ray.GetPoint(distance); // 거리 유지
                isDrag = true;
            }
        }

        if (Input.GetMouseButtonUp(0)) // 마우스 좌클릭 해제
        {
            isDrag = false;
            controller = null;
        }

        if (isDrag && controller != null) // 오브젝트를 집고 있을 때
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 레이캐스트 발사
            Vector3 pos = ray.GetPoint(distance) + offset; // 거리 유지
            controller.position = pos; // 위치 변환
        }
    }
}