using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float m_Speed;
    [SerializeField] float m_TimeToHitTarget = 1f;
    [SerializeField] bool m_IsStraight;
    [SerializeField] AudioClip m_StabSound;

    private Rigidbody2D m_Rigidbody2D;
    private GameObject m_Target;
    private Vector2 predictedVelocity;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        predictedVelocity = GetVelocity(this.gameObject, m_Target, m_TimeToHitTarget);
        if (!m_IsStraight)
        {
            this.m_Rigidbody2D.velocity = predictedVelocity;
        }
        //Debug.Log(predictedVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsStraight)
        {
            m_Rigidbody2D.isKinematic = true;
            m_Rigidbody2D.velocity = Vector2.right * m_Speed;
        }
        else
        {
            //this.transform.rotation = Quaternion.Euler(0f, 0f, m_Rigidbody2D.velocity.x * m_Rigidbody2D.velocity.y);
            Quaternion vari = Quaternion.Euler(0f, 0f, m_Rigidbody2D.velocity.x * m_Rigidbody2D.velocity.y);
            transform.rotation = Quaternion.Slerp(this.transform.rotation, vari, 0.1f);
        }
    }
    private Vector2 GetVelocity(GameObject shooter, GameObject target, float designedTime)
    {
        //EnemyMovement targetMovement = target.GetComponent<EnemyMovement>();
        if (target != null)
        {
            Vector2 targetVelocity = target.GetComponent<Rigidbody2D>().velocity;
            Vector3 offset = targetVelocity * designedTime;
            Vector2 distance = shooter.transform.position - (target.transform.position + offset);
            float Vx = Mathf.Abs(distance.x / designedTime);
            float Vy = Mathf.Abs(distance.y / designedTime - 0.5f * (-9.8f) * designedTime);

            return new Vector2(Vx, Vy);
        }
        else
            return Vector2.zero;
    }
    public void SetTarget(GameObject target)
    {
        m_Target = target;
    }
    public AudioClip GetProjectileSoundFX()
    {
        return m_StabSound;
    }
}
