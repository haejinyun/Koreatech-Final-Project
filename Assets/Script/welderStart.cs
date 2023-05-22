using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class welderStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("용접 실행 페이지 들어옴");
    }

    // Update is called once per frame
    void Update()
    {
        if(State.welder_state == false)
        {
            //용접이 불가능
            //컨트롤러에서 진동 피드백 필요
            // 아무 이펙트(용접 소리 + 불꽃 튀는 이펙트) 없이 컨트롤러 진동만 울리게
        }
        else if(State.boots==false || State.gloves==false || State.helmet == false)
        {
            //보호장비가 없는 상황
            //컨트롤러에서 온도 피드백 필요
            //용접은 가능하게, 이펙트 있고 용접 그려지기도 하게
        }
/*        else if(컨트롤러의 레이가 Iron판에 충돌되지 않고 버튼이 눌렸을 때, 즉 철판이 아닌 곳에 용접을 하려할 때){
            //컨트롤러에서 진동 피드백 필요.
            //용접은 가능하게, 이펙트 있게
        }*/
    }

    
}
