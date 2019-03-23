using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float m_MaxHealth = 100f;
    private float m_CurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damage = collider.GetComponent<DamageDealer>();
        Projectile projectile = collider.GetComponent<Projectile>();
        if (damage != null && projectile != null)
        {
            m_CurrentHealth -= damage.GetDamage();
            AudioSource.PlayClipAtPoint(projectile.GetProjectileSoundFX(), Camera.main.transform.position, PlayerPrefsManager.GetVolume());
            Destroy(collider.gameObject);
        }

        if(m_CurrentHealth <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(this.gameObject);
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
