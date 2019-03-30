using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave Config", fileName ="Wave")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] float m_StartTime = 18f;
    [SerializeField] int m_RandomFactor = 3;
    [SerializeField] GameObject[] m_EnemyPrefabs;
    [SerializeField] bool m_IsEndlessRound;

    private int m_NumOfEnemies = 0;
    private float m_Progress = 0f;
    private List<GameObject> m_CurrentWaveEnemyList = new List<GameObject>();
    public void UpdateWave(int index)
    {
        int numOfEnemySpecies;
        switch (index)
        {
            case 1:
                m_NumOfEnemies = 1;
                Debug.Log("Number of Enemies: " + m_NumOfEnemies);
                m_CurrentWaveEnemyList.Clear();
                m_CurrentWaveEnemyList.Add(m_EnemyPrefabs[0]);
                break;
            case 2:
                m_NumOfEnemies = 1;
                Debug.Log("Number of Enemies: " + m_NumOfEnemies);
                m_CurrentWaveEnemyList.Clear();
                m_CurrentWaveEnemyList.Add(m_EnemyPrefabs[0]);
                break;
            case 3:
                m_NumOfEnemies = 2;
                Debug.Log("Number of Enemies: " + m_NumOfEnemies);
                m_CurrentWaveEnemyList.Clear();
                m_CurrentWaveEnemyList.Add(m_EnemyPrefabs[0]);
                break;
            //From case 4, everything should be randomized
            case 4:
                m_NumOfEnemies = Random.Range(2, 2+m_RandomFactor);
                Debug.Log("Number of Enemies: " + m_NumOfEnemies);
                m_CurrentWaveEnemyList.Clear();
                numOfEnemySpecies = m_EnemyPrefabs.Length > 1 ? Random.Range(0, m_EnemyPrefabs.Length - 1) : 1;
                for (int i = 0; i < numOfEnemySpecies; i++)
                {
                    m_CurrentWaveEnemyList.Add(m_EnemyPrefabs[i]);
                }
                break;
            case 5:
                m_NumOfEnemies = Random.Range(1, 1 + m_RandomFactor);
                Debug.Log("Number of Enemies: " + m_NumOfEnemies);
                m_CurrentWaveEnemyList.Clear();
                numOfEnemySpecies = m_EnemyPrefabs.Length > 1 ? Random.Range(0, m_EnemyPrefabs.Length - 1) : 1;
                for (int i = 0; i < numOfEnemySpecies; i++)
                {
                    m_CurrentWaveEnemyList.Add(m_EnemyPrefabs[i]);
                }
                break;
            case 6:
                m_NumOfEnemies = (int)Random.Range(2, 2+m_RandomFactor);
                Debug.Log("Number of Enemies: " + m_NumOfEnemies);
                m_CurrentWaveEnemyList.Clear();
                numOfEnemySpecies = m_EnemyPrefabs.Length > 1 ? Random.Range(0, m_EnemyPrefabs.Length - 1) : 1;
                for (int i = 0; i < numOfEnemySpecies; i++)
                {
                    m_CurrentWaveEnemyList.Add(m_EnemyPrefabs[i]);
                }
                break;
            //Make sure to spawn the most power evemy this wave
            case 7:
                m_NumOfEnemies = 3;
                Debug.Log("Number of Enemies: " + m_NumOfEnemies);
                m_CurrentWaveEnemyList.Clear();
                m_CurrentWaveEnemyList.Add(m_EnemyPrefabs[m_EnemyPrefabs.Length-1]);
                break;
            case 8:
                m_NumOfEnemies = (int)Random.Range(3, 3+m_RandomFactor);
                Debug.Log("Number of Enemies: " + m_NumOfEnemies);
                m_CurrentWaveEnemyList.Clear();
                for(int i = 0; i < m_EnemyPrefabs.Length; i++)
                {
                    m_CurrentWaveEnemyList.Add(m_EnemyPrefabs[i]);
                }
                break;
            case 9:
                m_NumOfEnemies = 5;
                Debug.Log("Number of Enemies: " + m_NumOfEnemies);
                m_CurrentWaveEnemyList.Clear();
                for (int i = 0; i < m_EnemyPrefabs.Length; i++)
                {
                    m_CurrentWaveEnemyList.Add(m_EnemyPrefabs[i]);
                }
                break;
            case 10:
                m_NumOfEnemies = 10;
                Debug.Log("Number of Enemies: " + m_NumOfEnemies);
                m_CurrentWaveEnemyList.Clear();
                for (int i = 0; i < m_EnemyPrefabs.Length; i++)
                {
                    m_CurrentWaveEnemyList.Add(m_EnemyPrefabs[i]);
                }
                break;
            default:
                m_NumOfEnemies += 2;
                Debug.Log("Number of Enemies: " + m_NumOfEnemies);
                m_CurrentWaveEnemyList.Clear();
                for (int i = 0; i < m_EnemyPrefabs.Length; i++)
                {
                    m_CurrentWaveEnemyList.Add(m_EnemyPrefabs[i]);
                }
                break;
        }
    }
    public void UpdateProgress(float amount)
    {
        m_Progress += amount;
    }
    public float GetProgress()
    {
        return m_Progress;
    }
    public float GetStartTime()
    {
        return m_StartTime;
    }
    public int GetNumOfEnemies()
    {
        return m_NumOfEnemies;
    }
    public List<GameObject> GetCurrentWaveList()
    {
        return m_CurrentWaveEnemyList;
    }
    public bool IsEndlessRound()
    {
        return m_IsEndlessRound;
    }
}
