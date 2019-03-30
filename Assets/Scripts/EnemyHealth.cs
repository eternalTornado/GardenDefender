using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float m_MaxHealth = 100f;
    private float m_CurrentHealth;
    private Animator m_Animator;
    private EnemyMovement m_EnemyMovement;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        m_EnemyMovement = this.GetComponent<EnemyMovement>();
        m_CurrentHealth = m_MaxHealth;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damage = collider.GetComponent<DamageDealer>();
        Projectile projectile = collider.GetComponent<Projectile>();

        if (projectile == null ||projectile.GetLineInstantiate() != this.transform.position.y)
            return;

        if (damage != null && projectile != null)
        {
            GameObject hitSFX = Instantiate(projectile.GetProjectileHitSFX(), projectile.transform.position, Quaternion.identity);
            Destroy(hitSFX.gameObject, 2f);
            m_CurrentHealth -= damage.GetDamage();
            AudioSource.PlayClipAtPoint(projectile.GetProjectileSoundFX(), Camera.main.transform.position, PlayerPrefsManager.GetVolume());
            Destroy(collider.gameObject);
        }

        if(m_CurrentHealth <= 0f)
        {
            m_Animator.SetTrigger("Die");
        }
    }
    public void Die()
    {
        FireGolem fireGolem;
        try
        {
            fireGolem = this.GetComponent<FireGolem>();
            GameObject explosionSFX = Instantiate(fireGolem.GetExplosionSFX(), this.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(fireGolem.GetExplosionSound(), Camera.main.transform.position, PlayerPrefsManager.GetVolume());

            Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, fireGolem.GetExplosionRange());
            foreach(Collider2D collider in colliders)
            {
                if (collider.tag == "Defender")
                    Destroy(collider.gameObject);
            }

            Destroy(explosionSFX.gameObject, 2f);
        }
        catch(Exception e)
        {
            Debug.Log("Exception caught: "+e.Message);
        }

        IceGolem iceGolem;
        try
        {
            iceGolem = this.GetComponent<IceGolem>();
            GameObject earthGolemPrefab = iceGolem.GetEarthGolemPrefab();

            for(int i = 0; i< 4; i++)
            {
                Vector3 offset = Vector3.zero;
                switch (i)
                {
                    case 0:
                        offset = new Vector3(1.4f, 0f, 0f);
                        break;
                    case 1:
                        offset = new Vector3(-1.4f, 0f, 0f);
                        break;
                    case 2:
                        offset = new Vector3(0f, 1.4f, 0f);
                        break;
                    case 3:
                        offset = new Vector3(0f, -1.4f, 0f);
                        break;
                    default:
                        break;
                }
                GameObject earthGolem = Instantiate(earthGolemPrefab, this.transform.position + offset, Quaternion.identity) as GameObject;
                if (earthGolem.transform.position.y > 3f || earthGolem.transform.position.y < -2.6f)
                    Destroy(earthGolem.gameObject);
            }
        }
        catch(Exception e)
        {
            Debug.Log("Exception caught: " + e.Message);
        }
        Destroy(this.gameObject);
    }
    public void StopMoving()
    {
        m_EnemyMovement.StopMoving();
    }
    public void UpdateHealth(float damage)
    {
        m_CurrentHealth += damage;
        if(m_CurrentHealth <= 0f)
        {
            Die();
        }
    }
    public float GetMaxHealth()
    {
        return m_MaxHealth;
    }
    public float GetCurrentHealth()
    {
        return m_CurrentHealth;
    }
}
