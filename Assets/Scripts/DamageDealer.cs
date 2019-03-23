using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float m_Damage;
    public float GetDamage()
    {
        return m_Damage;
    }
}
