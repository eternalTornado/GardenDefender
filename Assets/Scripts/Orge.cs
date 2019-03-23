using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orge : MonoBehaviour
{
    [SerializeField] float m_MovingSpeed;
    [SerializeField] float m_AttackRate;
    [SerializeField] float m_Damage;
    //private attributes
    private float m_CurrentSpeed;
    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private Coroutine m_CurrentCoroutine;
    private float m_CurrentLine;
    private Vector3 m_CurrentPosition;
    private DefenderHealth m_CurrentTarget;
    private int m_NumOfEncounter;

    private float m_AttackTimer;
    private bool m_IsAttacking;

    private bool m_IgnoreVelocity = false;
    // Start is called before the first frame update
    void Start()
    {
        m_NumOfEncounter = 0;
        m_IsAttacking = false;
        m_AttackTimer = m_AttackRate;
        m_CurrentTarget = null;
        m_CurrentLine = this.transform.position.y;
        m_CurrentSpeed = m_MovingSpeed;
        m_Rigidbody = this.GetComponent<Rigidbody2D>();
        m_Animator = this.GetComponent<Animator>();

        m_Animator.SetTrigger("Run");
    }

    // Update is called once per frame
    void Update()
    {
        //m_CurrentPosition = this.transform.position;
        //m_CurrentPosition.y = Mathf.Clamp(m_CurrentPosition.y, m_CurrentLine, 5f);
        this.transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, m_CurrentLine, 5f), transform.position.z);

        //transform.Translate(Vector3.left * m_CurrentSpeed * Time.deltaTime);


        if (m_CurrentTarget != null && m_IsAttacking)
        {
            m_AttackTimer += Time.deltaTime;
            if(m_AttackTimer >= m_AttackRate)
            {
                m_Animator.SetTrigger("Attack");
                m_AttackTimer = 0f;
            }
        }
    }
    void FixedUpdate()
    {
        if (m_IgnoreVelocity)
            return;
        m_Rigidbody.velocity = Vector2.left * m_CurrentSpeed;
        m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, Mathf.Clamp(m_Rigidbody.velocity.y, 0f, Mathf.Infinity));
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        DefenderHealth defenderHealth = collider.GetComponent<DefenderHealth>();
        if (defenderHealth != null)
        {
            if (m_NumOfEncounter < 1)
            {
                m_Animator.SetTrigger("Jump");
                //m_CurrentSpeed *= 3f;
                TestAddforce();
                m_NumOfEncounter++;
            }
            else
            {
                Debug.Log("Enter trigger2D for 2nd target");
                m_IsAttacking = true;
                m_CurrentTarget = defenderHealth;
                m_CurrentSpeed = 0f;
                Debug.Log("Current Speed: " + m_CurrentSpeed.ToString());
                Debug.Log("Current velocity: " + m_Rigidbody.velocity.ToString());
                Debug.Log("IgnoreVelocity: " + m_IgnoreVelocity.ToString());
            }
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "DefenderProjectile")
        {
            Debug.Log("Defender projectile ignored");
            return;
        }

        DefenderHealth defenderHealth = collider.GetComponent<DefenderHealth>();
        if (defenderHealth != null && collider.tag =="Defender")
        {
            Debug.Log("Trigger Exit Orge Called");
            if (m_NumOfEncounter < 1)
                return;

            Debug.Log("Exit of collider: " + collider.name);
            m_CurrentTarget = null;
            m_CurrentSpeed = m_MovingSpeed / 2f;
            m_Animator.SetTrigger("Walk");
        }
    }
    void TestAddforce()
    {
        Debug.Log("Addforce Called");
        Debug.Log("Current speed: " + m_Rigidbody.velocity.ToString());
        //m_Rigidbody.velocity = Vector2.zero;
        //Debug.Log("Current speed: " + m_Rigidbody.velocity.ToString());
        //m_Rigidbody.AddForce(Vector2.up * 250f + Vector2.left*5f);
        StartCoroutine(AddForceAlternative());
    }
    public void DealDamage()
    {
        m_CurrentTarget.UpdateHealth(-m_Damage);
    }
    IEnumerator AddForceAlternative ()
    {
        m_IgnoreVelocity = true;
        m_Rigidbody.velocity = Vector2.zero;
        m_Rigidbody.AddForce(Vector2.up * 275f + Vector2.left * 80f);
        yield return new WaitUntil(() => this.transform.position.y == m_CurrentLine);
        m_IgnoreVelocity = false;
    }
}
