using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float m_MoveSpeed;
    //private attributes
    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private float m_CurrentSpeed;

    private SpriteRenderer m_SpriteRenderer;
    private float m_TimeColdEfffect;
    private float m_Timer;

    // Start is called before the first frame update
    void Start()
    {
        m_Timer = 0f;
        m_TimeColdEfffect = 0f;
        m_SpriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        m_Animator = this.GetComponent<Animator>();
        m_CurrentSpeed = m_MoveSpeed;
        m_Rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TimeColdEfffect != 0)
        {
            m_Timer += Time.deltaTime;
            if(m_Timer > m_TimeColdEfffect)
            {
                IsColdAffected(false);
                m_TimeColdEfffect = 0f;
            }
        }
    }
    void FixedUpdate()
    {
        m_Rigidbody.velocity = Vector2.left * m_CurrentSpeed;
    }
    public void StopMoving()
    {
        m_CurrentSpeed = 0f;
    }
    public void StartMoving()
    {
        m_CurrentSpeed = m_MoveSpeed;
    }
    public float GetMoveSpeed()
    {
        return m_MoveSpeed;
    }
    public void IsColdAffected(bool value)
    {
        if (value == true)
        {
            m_SpriteRenderer.color = new Color(0.5f,0.5f,1,1);
            m_CurrentSpeed = m_MoveSpeed / 2;
        }
        else
        {
            m_SpriteRenderer.color = Color.white;
            m_CurrentSpeed = m_MoveSpeed;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<ColdEffect>() != null)
        {
            m_TimeColdEfffect = collider.GetComponent<ColdEffect>().GetColdTimeEffect();
            m_Timer = 0f;
        }
    }
}
