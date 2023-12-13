using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayMovement : MonoBehaviour //Mono: 하나의 Behaviour : 행동
{
    Vector3 m_Movement;
    Animator m_Animator;
    public float turnSpeed = 10f;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;

    Quaternion m_Rotation = Quaternion.identity; // Quaternion = 회전값 identity = 0
    
    
    void Start()
    {
        // <ㅁ> 꺽쇠 안의 내용물 "ㅁ"을 제너릭 메서드 라고 한다`
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent <AudioSource>();
    }

    // Root에서 순차적으로 차근차근 진행해라 => FixedUpdate 

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        m_Movement.Set(horizontal,0f,vertical);
        m_Movement.Normalize();
        // Mathf.Approximately(a,b); a,b값을 소수점 단위로 비교후 비슷하면 참,다르면 거짓
        bool hasHorizontalInput = !Mathf.Approximately(horizontal,0f); 
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking",isWalking);
        if (isWalking)
        {
            m_AudioSource.Play();
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

       
        m_Rotation = Quaternion.LookRotation(desiredForward);    
    }

    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
