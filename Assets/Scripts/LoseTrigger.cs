using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    [SerializeField] GameObject m_LoseCanvas;
    void Start()
    {
        m_LoseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
       if(collider.tag == "Enemy")
        {
            m_LoseCanvas.SetActive(true);
            Time.timeScale = 0f;
            BuildManager.instance.SetDefenderToPlace(null);
        }
    }
}
