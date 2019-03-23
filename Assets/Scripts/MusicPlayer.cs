using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instace = null;
    private AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Awake()
    {
        if(instace == null)
        {
            instace = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        m_AudioSource = this.GetComponent<AudioSource>();
        m_AudioSource.volume = PlayerPrefsManager.GetVolume();
        if(m_AudioSource.volume <= 0)
        {
            PlayerPrefsManager.SetVolume(0.5f);
            m_AudioSource.volume = PlayerPrefsManager.GetVolume();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMusicVolume(float volume)
    {
        PlayerPrefsManager.SetVolume(volume);
        m_AudioSource.volume = PlayerPrefsManager.GetVolume();
    }
}
