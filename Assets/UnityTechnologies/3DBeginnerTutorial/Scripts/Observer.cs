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
            RaycastHit raycastHit; //�ε����� ��� ���� ������ �ش� �����͸� ����

            if (Physics.Raycast(ray , out raycastHit)) // ray�� ���� raycastHit�� ��ȯ
            {
                if (raycastHit.collider.transform == player)
                    //��ũ��Ʈ�� �÷��̾� ĳ���Ͱ� ���� ������ ������ �ĺ��ϰ�, ����ĳ��Ʈ�� �����Ͽ� �ε����� ����� �ִ��� �ľ��� �� �ֽ��ϴ�
                {
                    GameEnding.CaughtPlayer();  
                } 
            }
        }
    }
}
