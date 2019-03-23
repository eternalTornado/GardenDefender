using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] float m_Volume;
    [SerializeField] AudioClip m_LoadSound;
    //Put private attributes here
    private AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = this.GetComponent<AudioSource>();
        m_AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
