using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayMovement : MonoBehaviour //Mono: �ϳ��� Behaviour : �ൿ
{
    Vector3 m_Movement;
    Animator m_Animator;
    public float turnSpeed = 10f;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;

    Quaternion m_Rotation = Quaternion.identity; // Quaternion = ȸ���� identity = 0
    
    
    void Start()
    {
        // <��> ���� ���� ���빰 "��"�� ���ʸ� �޼��� ��� �Ѵ�`
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent <AudioSource>();
    }

    // Root���� ���������� �������� �����ض� => FixedUpdate 

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        m_Movement.Set(horizontal,0f,vertical);
        m_Movement.Normalize();
        // Mathf.Approximately(a,b); a,b���� �Ҽ��� ������ ���� ����ϸ� ��,�ٸ��� ����
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
