using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class welderStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("���� ���� ������ ����");
    }

    // Update is called once per frame
    void Update()
    {
        if(State.welder_state == false)
        {
            //������ �Ұ���
            //��Ʈ�ѷ����� ���� �ǵ�� �ʿ�
            // �ƹ� ����Ʈ(���� �Ҹ� + �Ҳ� Ƣ�� ����Ʈ) ���� ��Ʈ�ѷ� ������ �︮��
        }
        else if(State.boots==false || State.gloves==false || State.helmet == false)
        {
            //��ȣ��� ���� ��Ȳ
            //��Ʈ�ѷ����� �µ� �ǵ�� �ʿ�
            //������ �����ϰ�, ����Ʈ �ְ� ���� �׷����⵵ �ϰ�
        }
/*        else if(��Ʈ�ѷ��� ���̰� Iron�ǿ� �浹���� �ʰ� ��ư�� ������ ��, �� ö���� �ƴ� ���� ������ �Ϸ��� ��){
            //��Ʈ�ѷ����� ���� �ǵ�� �ʿ�.
            //������ �����ϰ�, ����Ʈ �ְ�
        }*/
    }

    
}
