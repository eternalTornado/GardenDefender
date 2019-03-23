using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private MusicPlayer m_MusicPlayer;
    private Slider m_Slider;
    // Start is called before the first frame update
    void Start()
    {
        m_MusicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
        m_Slider = this.GetComponent<Slider>();
        m_Slider.onValueChanged.AddListener(ValueChangeCheck);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ValueChangeCheck(float volume)
    {
        m_MusicPlayer.SetMusicVolume(m_Slider.normalizedValue);
        PlayerPrefsManager.SetVolume(m_Slider.normalizedValue);
    }
}
