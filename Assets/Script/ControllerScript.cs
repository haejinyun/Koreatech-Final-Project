using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public GameObject ballPrefab; // 생성할 구체 프리팹
    public float delay; // 호출 간격
    public int hitCount;
    private GameObject lastBall;
    public PickUpScript ps;
    public float distance;
    public float scale;
    public Transform sparkle;

    void Start()
    {
        ps = FindObjectOfType<PickUpScript>();
        InvokeRepeating("SpawnBall", 0f, delay); // 일정한 간격으로 함수 호출
    }

    private void SpawnBall()
    {
        if (Input.GetMouseButton(1) && ps.isDrag) // 컨트롤러를 쥐고 있을 때 / 마우스 우클릭 시
        {
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Pan");

            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distance, layerMask))
            {
                Vector3 ballPos = hit.point; // 레이 충돌 지점을 구해서 ballPos에 할당
                GameObject newBall = Instantiate(ballPrefab, ballPos, hit.transform.rotation); // 용접 흔적 생성
                Transform newSparkle = Instantiate(sparkle, ballPos, hit.transform.rotation);
                Destroy(newSparkle.gameObject, 0.2f);
                // 통신 (온도 상승)

                if (lastBall != null && Vector3.Distance(lastBall.transform.position, newBall.transform.position) < 0.3f) // 이전 흔적과 거리가 가까운 경우
                {
                    Vector3 newScale = lastBall.transform.localScale + new Vector3(0.1f, 0f, 0.1f); // 이전 흔적을 기반으로 하여 스케일 증가

                    if (newScale.magnitude <= scale) // 흔적의 크기가 1f 이하인 경우에만 스케일 증가
                    {
                        newBall.transform.localScale = newScale; // 스케일 증가
                        Destroy(lastBall); // 이전 흔적 삭제
                    }
                }

                lastBall = newBall; // 흔적 업데이트

                if (hit.transform.CompareTag("Count")) // 빈 게임 오브젝트에 충돌한 경우
                {
                    hitCount++;
                    Debug.Log("Hit count : " + hitCount);
                    Destroy(hit.transform.gameObject);

                    if (hitCount == 5)
                    {
                        CancelInvoke(); // InvokeRepeating()으로 호출한 함수를 정지
                                        // 통신 진동?
                    }
                }
            }
        }
        else
        {
            // 통신 (온도 하강)
        }
    }
}