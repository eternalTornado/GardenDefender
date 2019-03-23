using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] float m_AttackRate = 1f;
    //private attributes
    private DamageDealer m_DamageDealer;
    private Animator m_Animator;
    private EnemyMovement m_EnemyMovement;
    private DefenderHealth m_CurrentTarget;
    private float m_Timer;
    private bool m_IsAttacking;
    // Start is called before the first frame update

    void Start()
    {
        m_CurrentTarget = null;
        m_IsAttacking = false;
        m_Timer = 0f;
        m_DamageDealer = this.GetComponent<DamageDealer>();
        m_Animator = this.GetComponent<Animator>();
        m_EnemyMovement = this.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsAttacking && m_CurrentTarget!=null)
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= m_AttackRate)
            {
                m_Animator.SetTrigger("Attack");
                m_Timer = 0f;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Defender")
        {
            m_CurrentTarget = collider.gameObject.GetComponent<DefenderHealth>();
            m_EnemyMovement.StopMoving();
            m_Animator.SetBool("isWalking", false);
            m_IsAttacking = true;
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        m_CurrentTarget = null;
        m_IsAttacking = false;
        m_Animator.SetBool("isWalking", true);
        m_EnemyMovement.StartMoving();
    }
    public void DealDamage()
    {
        m_CurrentTarget.UpdateHealth(-m_DamageDealer.GetDamage());
    }
}
