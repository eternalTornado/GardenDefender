using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGolem : MonoBehaviour
{
    [SerializeField] float m_Range;
    [SerializeField] float m_AttackRate;
    [SerializeField] float m_Damage;
    [SerializeField] GameObject m_EarthGolemPrefab;

    [Header("Shown for Debugging purpose only")]
    [SerializeField] GameObject m_CurrentTarget;

    //Private Attributes
    private EnemyMovement m_EnemyMovement;
    private Animator m_Animator;
    private float m_TimerBeforeAttack;
    void Start()
    {
        m_TimerBeforeAttack = 0f;
        m_EnemyMovement = this.GetComponent<EnemyMovement>();
        m_Animator = this.GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentTarget != null)
        {
            m_TimerBeforeAttack += Time.deltaTime;
            if (m_TimerBeforeAttack >= m_AttackRate)
            {
                m_Animator.SetTrigger("Attack");
                m_TimerBeforeAttack = 0f;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, m_Range);
    }
    void UpdateTarget()
    {
        GameObject target = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, m_Range);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Defender")
            {
                if (collider.transform.position.y == this.transform.position.y && collider.transform.position.x < this.transform.position.x)
                {
                    target = collider.gameObject;
                }
            }
        }
        m_CurrentTarget = target;
        if (m_CurrentTarget != null)
        {
            m_EnemyMovement.StopMoving();
            m_Animator.SetBool("isWalking", false);
            CancelInvoke("UpdateTarget");
        }
    }
    public void DealDamage()
    {
        if (m_CurrentTarget == null)
        {
            Debug.Log("Target is no longer there");
            return;
        }

        DefenderHealth m_DefenderHealth = m_CurrentTarget.GetComponent<DefenderHealth>();
        m_DefenderHealth.UpdateHealth(-m_Damage);
    }
    public void ToggleUpdateTarget()
    {
        if (m_CurrentTarget == null && !IsInvoking("UpdateTarget"))
        {
            m_EnemyMovement.StartMoving();
            m_Animator.SetBool("isWalking", true);
            InvokeRepeating("UpdateTarget", 0f, 1f);
        }
    }
    public GameObject GetEarthGolemPrefab()
    {
        return m_EarthGolemPrefab;
    }
}
