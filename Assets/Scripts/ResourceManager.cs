using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] float m_StartResource = 100f;
    [SerializeField] TextMeshProUGUI m_ResourceText;
    public static ResourceManager instance;
    //Private attributes
    private float m_CurrentResource;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }
    void Start()
    {
        m_CurrentResource = m_StartResource;
        m_ResourceText.text = m_CurrentResource.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetResource()
    {
        return m_CurrentResource;
    }
    public void UpdateResourceText(float amount)
    {
        m_CurrentResource += amount;
        m_ResourceText.text = m_CurrentResource.ToString();
    }
}
