using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    bool m_IsPlayerInRange;
    public GameEnding GameEnding;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit; //부딪히는 대상에 관한 정보로 해당 데이터를 설정

            if (Physics.Raycast(ray , out raycastHit)) // ray의 값을 raycastHit에 반환
            {
                if (raycastHit.collider.transform == player)
                    //스크립트가 플레이어 캐릭터가 공격 범위에 있음을 식별하고, 레이캐스트를 실행하여 부딪히는 대상이 있는지 파악할 수 있습니다
                {
                    GameEnding.CaughtPlayer();  
                } 
            }
        }
    }
}
