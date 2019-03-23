using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAttacking : MonoBehaviour
{
    [SerializeField] GameObject m_ProjectilePrefab;
    [SerializeField] GameObject m_GunPos;
    [SerializeField] float m_Range;
    [SerializeField] float m_FireRate;
    [SerializeField] bool m_IsMelee =false;
    [SerializeField] float m_MeleeDamage;
    //put private attribues here
    [SerializeField] GameObject m_CurrentTarget;
    private Animator m_Animator;
    private float m_Timer;
    // Start is called before the first frame update
    void Start()
    {
        m_Timer = 0f;
        m_CurrentTarget = null;
        m_Animator = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_CurrentTarget != null)
        {
            m_Timer += Time.deltaTime;
            if(m_Timer >= m_FireRate)
            {
                m_Animator.SetTrigger("Attack");
                m_Timer = 0f;
            }
        }
    }
    private void UpdateTarget()
    {
        GameObject target = null;
        float closestDistanceToTarget = m_Range;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            if(enemy.transform.position.y == transform.position.y && enemy.transform.position.x > transform.position.x)
            {
                if(Vector2.Distance(this.transform.position, enemy.transform.position) < closestDistanceToTarget)
                {
                    target = enemy;
                }
            }
        }
        m_CurrentTarget = target;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_Range);
    }
    public void Fire()
    {
        GameObject projectile = Instantiate(m_ProjectilePrefab, m_GunPos.transform.position, Quaternion.identity) as GameObject;
        projectile.transform.parent = this.transform;
        projectile.GetComponent<Projectile>().SetTarget(m_CurrentTarget);
    }
    public void Slash()
    {
        EnemyHealth enemyHealth = m_CurrentTarget.GetComponent<EnemyHealth>();
    }
}
