using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdEffect : MonoBehaviour
{
    [SerializeField] float m_EffectTime;
    [SerializeField] float m_EffectModifier;
    [SerializeField] Color m_EffectColor;

    private GameObject m_CurrentTarget;
    private SpriteRenderer m_SpriteRenderer;
    private BoxCollider2D m_BoxCollider;
    private Rigidbody2D m_Rigidbody;
    void Start()
    {
        m_CurrentTarget = null;
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        m_BoxCollider = this.GetComponent<BoxCollider2D>();
        m_Rigidbody = this.GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            m_CurrentTarget = collider.gameObject;
            StartCoroutine(ApplyEffect());
        }
    }
    IEnumerator ApplyEffect()
    {
        EnemyMovement enemyMovement = m_CurrentTarget.GetComponent<EnemyMovement>();
        enemyMovement.IsColdAffected(true);

        m_SpriteRenderer.enabled = false;
        m_BoxCollider.enabled = false;
        m_Rigidbody.bodyType = RigidbodyType2D.Kinematic;

        yield return new WaitForSeconds(m_EffectTime);

        enemyMovement.IsColdAffected(false);
    }
    public float GetColdTimeEffect()
    {
        return m_EffectTime;
    }
}
