using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour
{
    [SerializeField] GameObject m_GoldCoinPrefab;
    [SerializeField] float m_IncomeRate = 10f;
    [SerializeField] float m_CurrencyPerTime = 25f;
    [SerializeField] float m_RandomFactor = 4f; 
    private float m_Timer;
    private float m_CurrentIncomeRate;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentIncomeRate = m_IncomeRate;
        m_Timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if(m_Timer>= m_CurrentIncomeRate)
        {
            GameObject coin = Instantiate(m_GoldCoinPrefab, transform.position, Quaternion.identity);
            coin.transform.parent = this.transform;
            StartCoroutine(AddCurrency(m_CurrencyPerTime));
            m_CurrentIncomeRate = Random.Range(m_IncomeRate - m_RandomFactor, m_IncomeRate + m_RandomFactor);
            m_Timer = 0f;
        }
    }
    IEnumerator AddCurrency(float amount)
    {
        float countVar = amount;
        while(countVar > 0)
        {
            countVar--;
            ResourceManager.instance.UpdateResourceText(1);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
