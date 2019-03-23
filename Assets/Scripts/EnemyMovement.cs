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

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        m_CurrentSpeed = m_MoveSpeed;
        m_Rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
           
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
}
