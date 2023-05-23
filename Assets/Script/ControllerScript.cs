using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public GameObject ballPrefab; // ������ ��ü ������
    public float delay; // ȣ�� ����
    public int hitCount;
    private GameObject lastBall;
    public PickUpScript ps;
    public float distance;
    public float scale;
    public Transform sparkle;

    void Start()
    {
        ps = FindObjectOfType<PickUpScript>();
        InvokeRepeating("SpawnBall", 0f, delay); // ������ �������� �Լ� ȣ��
    }

    private void SpawnBall()
    {
        if (Input.GetMouseButton(1) && ps.isDrag) // ��Ʈ�ѷ��� ��� ���� �� / ���콺 ��Ŭ�� ��
        {
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Pan");

            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distance, layerMask))
            {
                Vector3 ballPos = hit.point; // ���� �浹 ������ ���ؼ� ballPos�� �Ҵ�
                GameObject newBall = Instantiate(ballPrefab, ballPos, hit.transform.rotation); // ���� ���� ����
                Transform newSparkle = Instantiate(sparkle, ballPos, hit.transform.rotation);
                Destroy(newSparkle.gameObject, 0.2f);
                // ��� (�µ� ���)

                if (lastBall != null && Vector3.Distance(lastBall.transform.position, newBall.transform.position) < 0.3f) // ���� ������ �Ÿ��� ����� ���
                {
                    Vector3 newScale = lastBall.transform.localScale + new Vector3(0.1f, 0f, 0.1f); // ���� ������ ������� �Ͽ� ������ ����

                    if (newScale.magnitude <= scale) // ������ ũ�Ⱑ 1f ������ ��쿡�� ������ ����
                    {
                        newBall.transform.localScale = newScale; // ������ ����
                        Destroy(lastBall); // ���� ���� ����
                    }
                }

                lastBall = newBall; // ���� ������Ʈ

                if (hit.transform.CompareTag("Count")) // �� ���� ������Ʈ�� �浹�� ���
                {
                    hitCount++;
                    Debug.Log("Hit count : " + hitCount);
                    Destroy(hit.transform.gameObject);

                    if (hitCount == 5)
                    {
                        CancelInvoke(); // InvokeRepeating()���� ȣ���� �Լ��� ����
                                        // ��� ����?
                    }
                }
            }
        }
        else
        {
            // ��� (�µ� �ϰ�)
        }
    }
}