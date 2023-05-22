using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LayserPointer : MonoBehaviour
{

    private LineRenderer layser;        // ������
    private RaycastHit Collided_object; // �浹�� ��ü
    private GameObject currentObject;   // ���� �ֱٿ� �浹�� ��ü�� �����ϱ� ���� ��ü

    public float raycastDistance = 100f; // ������ ������ ���� �Ÿ�

    public AudioClip clip; //����� Ŭ��!
    public AudioClip bgclip;
    // Start is called before the first frame update
    void Start()
    {
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
        StartCoroutine("NextStep");
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
            // �浹 ��ü�� �±װ� Mask ���
            if (Collided_object.collider.gameObject.CompareTag("Mask"))
            {
                // ��ŧ���� �� �����ܿ� A �κ��� ���� ���
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // ��ư�� ��ϵ� onClick �޼ҵ带 �����Ѵ�.
                    //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    Debug.Log("MASK CLICK");            
                    State.helmet = true;                 
                    Collided_object.collider.gameObject.SetActive(false);
                    SoundManager.instance.SFXPlay("click", clip);
                }

                /*                else
                                {
                                    Collided_object.collider.gameObject.GetComponent<Button>().OnPointerEnter(null);
                                    currentObject = Collided_object.collider.gameObject;
                                }*/
            }
            // �浹 ��ü�� �±װ� Glooves ���
            if (Collided_object.collider.gameObject.CompareTag("Glooves"))
            {
                // ��ŧ���� �� �����ܿ� A �κ��� ���� ���
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // ��ư�� ��ϵ� onClick �޼ҵ带 �����Ѵ�.
                    //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    Debug.Log("Glooves CLICK");
                    State.gloves = true;
                    Collided_object.collider.gameObject.SetActive(false);
                    SoundManager.instance.SFXPlay("click", clip);

                }

                /*                else
                                {
                                    Collided_object.collider.gameObject.GetComponent<Button>().OnPointerEnter(null);
                                    currentObject = Collided_object.collider.gameObject;
                                }*/
            }
            // �浹 ��ü�� �±װ� Boots ���
            if (Collided_object.collider.gameObject.CompareTag("Boots"))
            {
                // ��ŧ���� �� �����ܿ� A �κ��� ���� ���
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // ��ư�� ��ϵ� onClick �޼ҵ带 �����Ѵ�.
                    //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    Debug.Log("Boots CLICK");
                    State.boots = true;
                    Collided_object.collider.gameObject.SetActive(false);
                    SoundManager.instance.SFXPlay("click", clip);

                }
                /*                else
                                {
                                    Collided_object.collider.gameObject.GetComponent<Button>().OnPointerEnter(null);
                                    currentObject = Collided_object.collider.gameObject;
                                }*/
            }
            // �浹 ��ü�� �±װ� Other ���
            if (Collided_object.collider.gameObject.CompareTag("Other"))
            {
                // ��ŧ���� �� �����ܿ� A �κ��� ���� ���
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    // ��ư�� ��ϵ� onClick �޼ҵ带 �����Ѵ�.
                    //Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    Debug.Log("Other CLICK");
                    Collided_object.collider.gameObject.SetActive(false);
                    SoundManager.instance.SFXPlay("click", clip);

                }

                /*                else
                                {
                                    Collided_object.collider.gameObject.GetComponent<Button>().OnPointerEnter(null);
                                    currentObject = Collided_object.collider.gameObject;
                                }*/
            }
        }

        else
        {
            // �������� ������ ���� ���� ������ ������ �ʱ� ���� ���̸�ŭ ��� �����.
            layser.SetPosition(1, transform.position + (transform.forward * raycastDistance));

            // �ֱ� ������ ������Ʈ�� Button�� ���
            // ��ư�� ���� �����ִ� �����̹Ƿ� �̰��� Ǯ���ش�.
            if (currentObject != null)
            {
                currentObject.GetComponent<Button>().OnPointerExit(null);
                currentObject = null;
            }

        }
    }

    private void LateUpdate()
    {
        // ��ư�� ���� ���        
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            layser.material.color = new Color(255, 255, 255, 0.5f);
        }

        // ��ư�� �� ���          
        else if (OVRInput.GetUp(OVRInput.Button.One))
        {
            layser.material.color = new Color(0, 195, 255, 0.5f);
            Debug.Log("helmet state" + State.helmet);
            Debug.Log("gloves state" + State.gloves);
            Debug.Log("boots state" + State.boots);

        }
    }

    IEnumerator NextStep()
    {
        yield return new WaitUntil(() => State.helmet && State.gloves && State.boots);
        Debug.Log("Ready");
        //�� �̵� ( Turn����)
        CallNextScene();
    }

    void CallNextScene()
    {
        SceneManager.LoadScene("StartWelder");
    }

    /*    IEnumerator playSound()
        {
           audioSource.Play();
        }*/

}
