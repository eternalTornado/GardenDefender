using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject m_BombExplosion;
    [SerializeField] AudioClip m_ExplosionSound;
    [SerializeField] float m_TimeBeforeExplode;
    [SerializeField] float m_ExplodeRange;

    private float m_Timer;
    // Start is called before the first frame update
    void Start()
    {
        m_Timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if(m_Timer >= m_TimeBeforeExplode)
        {
            DealDamage();
            SFX();
        }
    }
    private void SFX()
    {
        GameObject sfx = Instantiate(m_BombExplosion, this.transform.position, Quaternion.identity) as GameObject;
        Destroy(sfx.gameObject, 4f);
        AudioSource.PlayClipAtPoint(m_ExplosionSound, Camera.main.transform.position, PlayerPrefsManager.GetVolume());
        Destroy(this.gameObject);
    }
    private void DealDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, m_ExplodeRange);
        foreach(Collider2D collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Destroy(collider.gameObject);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, m_ExplodeRange);
    }
}
