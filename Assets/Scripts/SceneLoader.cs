using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float m_DelayTime;
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadNextSceneDelay(m_DelayTime);
        }
    }
    public void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
    public void ReLoadGame()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(1);
    }
    private void LoadNextSceneDelay(float delayTime)
    {
        StartCoroutine(LoadNextSceneWithDelay(delayTime));
    }
    IEnumerator LoadNextSceneWithDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        LoadNextScene();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ReloadCurrentScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }
}
