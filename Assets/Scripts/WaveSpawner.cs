using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //[SerializeField] int m_NumOfWaves = 5;
    [SerializeField] GameObject m_SpawnField;
    [SerializeField] GameObject m_WinCanvas;
    //[SerializeField] GameObject[] m_EnemyPrefab;
    [SerializeField] WaveConfig m_Wave;
    [SerializeField] float m_TimeBeforeNextWave = 15f;
    
    private int holyShite;
    private List<Transform> m_SpawnPoint;
    private int randomFactor;
    private int m_NumOfEnemiesOnField;


    private float m_TimerNextWave;
    // Start is called before the first frame update
    void Start()
    {
        m_TimerNextWave = 0f;
        m_WinCanvas.SetActive(false);
        Time.timeScale = 1f;
        holyShite = 0;
        m_SpawnPoint = new List<Transform>();
        foreach(Transform point in m_SpawnField.transform)
        {
            m_SpawnPoint.Add(point);
        }
        //m_Wave.UpdateWave(index);

        InvokeRepeating("UpdateNumOfEnemies", m_Wave.GetStartTime(), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        m_TimerNextWave += Time.deltaTime;
        if(m_TimerNextWave>= m_TimeBeforeNextWave)
        {
            Invoke("UpdateNumOfEnemies", 0f);
            m_TimerNextWave = 0f;
        }
    }
    private void UpdateNumOfEnemies()
    {
        m_NumOfEnemiesOnField = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (m_NumOfEnemiesOnField <= 0)
        {
            holyShite++;
            if (holyShite > 10 && !m_Wave.IsEndlessRound())
            {
                m_WinCanvas.SetActive(true);
                CancelInvoke("UpdateNumOfEnemies");
                BuildManager.instance.SetDefenderToPlace(null);
                BuildManager.instance.m_IsBuildable = false;
                //Do we really need to stop the time ?
                //Time.timeScale = 0f;
            }
            else
            {
                m_Wave.UpdateWave(holyShite);
                StartCoroutine(SpawnEnemies());
            }
        }
    }
    IEnumerator SpawnEnemies()
    {
        //Prevent random from generating same results
        List<int> randomList = new List<int>();
        int randomPoint;
        int randomEnemy;
        for(int i = 0; i<m_Wave.GetNumOfEnemies(); i++)
        {
            randomPoint = Random.Range(0, m_SpawnPoint.Count);
            if (randomList.Contains(randomPoint))
            {
                randomPoint = Random.Range(0, m_SpawnPoint.Count);
            }
            randomList.Add(randomPoint);
            if (randomList.Count >= m_SpawnPoint.Count)
                randomList.Clear();
            randomEnemy = Random.Range(0, m_Wave.GetCurrentWaveList().Count);
            GameObject enemy = GameObject.Instantiate(m_Wave.GetCurrentWaveList()[randomEnemy], m_SpawnPoint[randomPoint].position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
    IEnumerator SpawnEndlessEnemies()
    {
        yield return null;
    }
}
