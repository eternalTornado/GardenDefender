using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip m_Hover;
    [SerializeField] AudioClip m_Click;

    public void EnterSound()
    {
        AudioSource.PlayClipAtPoint(m_Hover, Camera.main.transform.position, PlayerPrefsManager.GetVolume());
    }
    public void ClickSound()
    {
        AudioSource.PlayClipAtPoint(m_Click, Camera.main.transform.position, PlayerPrefsManager.GetVolume());
    }
}
