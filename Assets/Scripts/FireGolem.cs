using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGolem : MonoBehaviour
{
    [SerializeField] float m_Range;
    [SerializeField] float m_AttackRate;
    [SerializeField] float m_Damage;

    //this one is shown on inspector for debugging purpose
    [SerializeField] GameObject m_CurrentTarget;
    //private attributes
    private EnemyMovement m_EnemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        m_EnemyMovement = 
    }

    // Update is called once per frame
    void Update()
    {
        
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

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, m_Range);
    }
}
