using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] float m_Cost;
    [SerializeField] float m_RechargeTime;
    [SerializeField] AudioClip m_HiredSound;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(m_HiredSound, Camera.main.transform.position, PlayerPrefsManager.GetVolume());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetCost()
    {
        return m_Cost;
    }
    public float GetRechargeTime()
    {
        return m_RechargeTime;
    }
}
