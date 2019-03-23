using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderHealth : MonoBehaviour
{
    [SerializeField] float m_MaxHealth;
    //Private attributes
    [SerializeField] float m_CurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealth(float damage)
    {
        m_CurrentHealth += damage;
        if(m_CurrentHealth <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(this.gameObject);
        //Add more effect please
    }
}
