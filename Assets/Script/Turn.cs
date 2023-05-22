using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{

    public Image targetImage; //�޴� �̹���
    public Button newIron; //�� ö�� ��ư
    public Button reStart; // ���¹�ư
    private bool isActive = false; //�޴� ��Ƽ�긦 ���� Flag

    private LineRenderer layser;        // ������
    private RaycastHit Collided_object; // �浹�� ��ü
    //private GameObject currentObject;   // ���� �ֱٿ� �浹�� ��ü�� �����ϱ� ���� ��ü

    public float raycastDistance = 100f; // ������ ������ ���� �Ÿ�

    public AudioClip clip; //����� Ŭ��!
    public AudioClip Maskclip;

    void Start()
    {
        State.welder_state = false; //ON/OFF�� ����� flag
        Debug.Log("���� ���� �� ����ũ ������ ����");
        // ��ũ��Ʈ�� ���Ե� ��ü�� ���� ��������� ������Ʈ�� �ְ��ִ�.
        layser = this.gameObject.AddComponent<LineRenderer>();

        // ������ �������� ���� ǥ��
        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f);
        layser.material = material;
        // �������� �������� 2���� �ʿ� �� ���� ������ ��� ǥ�� �� �� �ִ�.
        layser.positionCount = 2;
        // ������ ���� ǥ��
        layser.startWidth = 0.01f;
        layser.endWidth = 0.01f;
      

    }

    // Update is called once per frame
    void Update()
    {
        layser.SetPosition(0, transform.position); // ù��° ������ ��ġ
                                                   // ������Ʈ�� �־� �����ν�, �÷��̾ �̵��ϸ� �̵��� ���󰡰� �ȴ�.
                                                   //  �� �����(�浹 ������ ����)
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green, 0.5f);
        // �浹 ���� ��
        if (Physics.Raycast(transform.position, transform.forward, out Collided_object, raycastDistance))
        {
            layser.SetPosition(1, Collided_object.point);
            // �浹 ��ü�� �±װ� TurnBtn ��� //�ѱ�
            if ((Collided_object.collider.gameObject.CompareTag("TurnBtn")) && (State.welder_state == false))
            {
                // ��ŧ���� �� �����ܿ� A �κ��� ���� ���
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // ��ư�� ��ϵ� onClick �޼ҵ带 �����Ѵ�.
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
            else if((Collided_object.collider.gameObject.CompareTag("TurnBtn")) && (State.welder_state == true)) //����
            {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // ��ư�� ��ϵ� onClick �޼ҵ带 �����Ѵ�.
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
                // ��ŧ���� �� �����ܿ� A �κ��� ���� ��� //����ũ ���� ��Ȳ �� �Ⱦ��� ������������
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    State.mask_state = !State.mask_state;
                    if ( State.mask_state == true)
                    {
                        Debug.Log("Mask On!!");
                        // ��ư�� ��ϵ� onClick �޼ҵ带 �����Ѵ�.
                        //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        CallNextSceneDo();
                        SoundManager.instance.SFXPlay("click", Maskclip);

                        

                        //�� �̵�
                        // �̵��� ������ �� ������ �ϸ� �ٽ� �� ������ �̵��ؾ���.

                    }
                    else
                    {
                        // ��ư�� ��ϵ� onClick �޼ҵ带 �����Ѵ�.
                        //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        Debug.Log("Mask Off!!");                    
                        CallNextSceneStart();
                        SoundManager.instance.SFXPlay("click", Maskclip);



                        //�� �̵�
                        // �̵��� ������ �� ������ �ϸ� �ٽ� �� ������ �̵��ؾ���.
                    }
                }

            }

            if (Collided_object.collider.gameObject.CompareTag("MenuBtn"))
            {
                // ��ŧ���� �� �����ܿ� A �κ��� ���� ���
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // ��ư�� ��ϵ� onClick �޼ҵ带 �����Ѵ�.
                    //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    Debug.Log("Open Menu");
                    OnBtnClick();
                }
            }

            if (Collided_object.collider.gameObject.CompareTag("RestartBtn"))
            {
                // ��ŧ���� �� �����ܿ� A �κ��� ���� ���
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    OnRestartBtnClick();
                }
            }
            if (Collided_object.collider.gameObject.CompareTag("NewIron"))
            {
                // ��ŧ���� �� �����ܿ� A �κ��� ���� ���
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
        Debug.Log("���¹�ư����");
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
        //���⿡ �� �ʱ�ȭ �ϴ� �ڵ� ������ �ȴ�.
        Debug.Log("ö�� �ʱ�ȭ");
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
