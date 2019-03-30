using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGolem : MonoBehaviour
{
    [SerializeField] float m_Range;
    [SerializeField] float m_AttackRate;
    [SerializeField] float m_Damage;

    [Header("Explosion Config")]
    [SerializeField] GameObject m_ExplosionSFX;
    [SerializeField] float m_ExplosionRange;
    [SerializeField] AudioClip m_ExplosionSound;

    [Header("Shown in Inspector for debugging purpose only")]
    [SerializeField] GameObject m_CurrentTarget;
    //private attributes
    private EnemyMovement m_EnemyMovement;
    private Animator m_Animator;
    private float m_TimerToAttack;
    // Start is called before the first frame update
    void Start()
    {
        m_TimerToAttack = 0f;
        m_EnemyMovement = this.GetComponent<EnemyMovement>();
        m_Animator = this.GetComponent<Animator>();

        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentTarget != null)
        {
            m_TimerToAttack += Time.deltaTime;
            if (m_TimerToAttack >= m_AttackRate)
            {
                m_Animator.SetTrigger("Attack");
                m_TimerToAttack = 0f;
            }
        }
    }

    void UpdateTarget()
    {
        GameObject target = null;
        float minDistance = m_Range;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, m_Range);
        foreach(Collider2D collider in colliders)
        {
            if(collider.tag == "Defender" && this.transform.position.y == collider.transform.position.y && this.transform.position.x > collider.transform.position.x)
            {
                if(target == null)
                    target = collider.gameObject;
                else
                {
                    minDistance = Vector2.Distance(this.transform.position, target.transform.position);
                    if(Vector2.Distance(this.transform.position, collider.transform.position) < minDistance)
                    {
                        target = collider.gameObject;
                    }
                }
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
        Gizmos.DrawWireSphere(this.transform.position, m_ExplosionRange);
    }
    public void DealDamageToTarget()
    {
        if (m_CurrentTarget == null)
        {
            Debug.Log("Current target is no longer there");
            return;
        }

        //If currentTarget is not null
        DefenderHealth defenderHealth =  m_CurrentTarget.GetComponent<DefenderHealth>();
        defenderHealth.UpdateHealth(-m_Damage);
    }
    public void ToggleTatgetUpdate()
    {
        if(m_CurrentTarget== null && !IsInvoking("UpdateTarget"))
        {
            m_EnemyMovement.StartMoving();
            m_Animator.SetBool("isWalking", true);
            InvokeRepeating("UpdateTarget", 0f, 0.2f);
        }
    }
    public GameObject GetExplosionSFX()
    {
        return m_ExplosionSFX;
    }
    public float GetExplosionRange()
    {
        return m_ExplosionRange;
    }
    public AudioClip GetExplosionSound()
    {
        return m_ExplosionSound;
    }
}
