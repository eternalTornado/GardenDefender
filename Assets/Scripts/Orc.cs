using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    [SerializeField] float m_Range;
    [SerializeField] float m_AttackRate;
    [SerializeField] float m_Damage;

    //private attributes
    [SerializeField] GameObject m_CurrentTarget;
    private EnemyMovement m_EnemyMovement;
    private Animator m_Animator;
    private float m_Timer;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        m_EnemyMovement = this.GetComponent<EnemyMovement>();
        m_Timer = m_AttackRate;
        m_CurrentTarget = null;
        if (m_CurrentTarget == null)
        {
            InvokeRepeating("UpdateTarget", 0f, 0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_CurrentTarget != null)
        {
            m_Timer += Time.deltaTime;
            if(m_Timer >= m_AttackRate)
            {
                m_Animator.SetTrigger("Attack");
                m_Timer = 0f;
            }
        }
    }
    private void UpdateTarget()
    {
        GameObject target = null;
        float closestRangeToTarget = m_Range;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Defender");
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(this.transform.position, enemy.transform.position);
            if (distance < closestRangeToTarget && this.transform.position.y == enemy.transform.position.y)
            {
                closestRangeToTarget = distance;
                target = enemy;
            }
        }
        m_CurrentTarget = target;

        if(m_CurrentTarget != null)
        {
            m_EnemyMovement.StopMoving();
            m_Animator.SetBool("isWalking", false);
            if (IsInvoking("UpdateTarget"))
            {
                CancelInvoke("UpdateTarget");
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, m_Range);
    }
    public void DealDamage()
    {
        DefenderHealth defenderHealth = m_CurrentTarget.GetComponent<DefenderHealth>();
        defenderHealth.UpdateHealth(-m_Damage);
    }
    public void ToggleUpdateTarget()
    {
        if(m_CurrentTarget == null && !IsInvoking("UpdateTarget"))
        {
            InvokeRepeating("UpdateTarget", 0f, 0.2f);
            m_EnemyMovement.StartMoving();
            m_Animator.SetBool("isWalking", true);
        }
    }
}
