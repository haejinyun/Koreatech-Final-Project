using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{

    public Image targetImage; //메뉴 이미지
    public Button newIron; //새 철판 버튼
    public Button reStart; // 리셋버튼
    private bool isActive = false; //메뉴 엑티브를 위한 Flag

    private LineRenderer layser;        // 레이저
    private RaycastHit Collided_object; // 충돌된 객체
    //private GameObject currentObject;   // 가장 최근에 충돌한 객체를 저장하기 위한 객체

    public float raycastDistance = 100f; // 레이저 포인터 감지 거리

    public AudioClip clip; //재생할 클립!
    public AudioClip Maskclip;

    void Start()
    {
        State.welder_state = false; //ON/OFF를 담당할 flag
        Debug.Log("용접 전원 및 마스크 페이지 들어옴");
        // 스크립트가 포함된 객체에 라인 렌더러라는 컴포넌트를 넣고있다.
        layser = this.gameObject.AddComponent<LineRenderer>();

        // 라인이 가지개될 색상 표현
        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f);
        layser.material = material;
        // 레이저의 꼭지점은 2개가 필요 더 많이 넣으면 곡선도 표현 할 수 있다.
        layser.positionCount = 2;
        // 레이저 굵기 표현
        layser.startWidth = 0.01f;
        layser.endWidth = 0.01f;
      

    }

    // Update is called once per frame
    void Update()
    {
        layser.SetPosition(0, transform.position); // 첫번째 시작점 위치
                                                   // 업데이트에 넣어 줌으로써, 플레이어가 이동하면 이동을 따라가게 된다.
                                                   //  선 만들기(충돌 감지를 위한)
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green, 0.5f);
        // 충돌 감지 시
        if (Physics.Raycast(transform.position, transform.forward, out Collided_object, raycastDistance))
        {
            layser.SetPosition(1, Collided_object.point);
            // 충돌 객체의 태그가 TurnBtn 경우 //켜기
            if ((Collided_object.collider.gameObject.CompareTag("TurnBtn")) && (State.welder_state == false))
            {
                // 오큘러스 고 리모콘에 A 부분을 누를 경우
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // 버튼에 등록된 onClick 메소드를 실행한다.
                    //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    Debug.Log("Turnon Btn Click");
                    Collided_object.collider.gameObject.transform.rotation = Quaternion.Euler(-63, 0, 0);
                    State.welder_state = true;
                    SoundManager.instance.SFXPlay("click", clip);


                }

                /*                else
                                {
                                    Collided_object.collider.gameObject.GetComponent<Button>().OnPointerEnter(null);
                                    currentObject = Collided_object.collider.gameObject;
                                }*/
            }
            else if((Collided_object.collider.gameObject.CompareTag("TurnBtn")) && (State.welder_state == true)) //끄기
            {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // 버튼에 등록된 onClick 메소드를 실행한다.
                    //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    Debug.Log("Turnoff Btn Click");
                    Collided_object.collider.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    State.welder_state = false;
                    SoundManager.instance.SFXPlay("click", clip);

                }

                /*                else
                                {
                                    Collided_object.collider.gameObject.GetComponent<Button>().OnPointerEnter(null);
                                    currentObject = Collided_object.collider.gameObject;
                                }*/
            }

            if ((Collided_object.collider.gameObject.CompareTag("Mask")))
            {
                // 오큘러스 고 리모콘에 A 부분을 누를 경우 //마스크 쓰는 상황 즉 안쓰고 있을때여야지
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    State.mask_state = !State.mask_state;
                    if ( State.mask_state == true)
                    {
                        Debug.Log("Mask On!!");
                        // 버튼에 등록된 onClick 메소드를 실행한다.
                        //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        CallNextSceneDo();
                        SoundManager.instance.SFXPlay("click", Maskclip);

                        

                        //씬 이동
                        // 이동한 씬에서 이 행위를 하면 다시 이 씬으로 이동해야해.

                    }
                    else
                    {
                        // 버튼에 등록된 onClick 메소드를 실행한다.
                        //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        Debug.Log("Mask Off!!");                    
                        CallNextSceneStart();
                        SoundManager.instance.SFXPlay("click", Maskclip);



                        //씬 이동
                        // 이동한 씬에서 이 행위를 하면 다시 이 씬으로 이동해야해.
                    }
                }

            }

            if (Collided_object.collider.gameObject.CompareTag("MenuBtn"))
            {
                // 오큘러스 고 리모콘에 A 부분을 누를 경우
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // 버튼에 등록된 onClick 메소드를 실행한다.
                    //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    Debug.Log("Open Menu");
                    OnBtnClick();
                }
            }

            if (Collided_object.collider.gameObject.CompareTag("RestartBtn"))
            {
                // 오큘러스 고 리모콘에 A 부분을 누를 경우
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    OnRestartBtnClick();
                }
            }
            if (Collided_object.collider.gameObject.CompareTag("NewIron"))
            {
                // 오큘러스 고 리모콘에 A 부분을 누를 경우
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    OnNewIronBtnClick();
                }
            }

        }
    }


    public void OnBtnClick()
    {
        isActive = !isActive;
        targetImage.gameObject.SetActive(isActive);
    }
    public void OnRestartBtnClick()
    {
        Debug.Log("리셋버튼눌림");
        State.boots = false;
        State.gloves = false;
        State.helmet = false;
        State.mask_state = false;
        State.welder_state = false;
/*        Debug.Log("boots" + State.boots);
        Debug.Log("gloves" + State.gloves);
        Debug.Log("helmet" + State.helmet);
        Debug.Log("mask_state" + State.mask_state);
        Debug.Log("welder_state" + State.welder_state);*/
        CallFirstScene();
    }
    public void OnNewIronBtnClick()
    {
        //여기에 판 초기화 하는 코드 넣으면 된다.
        Debug.Log("철판 초기화");
    }
    void CallNextSceneStart()
    {
        SceneManager.LoadScene("StartWelder");
    }

    void CallNextSceneDo()
    {
        SceneManager.LoadScene("DoWelder");
    }

    void CallFirstScene()
    {
        SceneManager.LoadScene("TestScene1");
    }
}
