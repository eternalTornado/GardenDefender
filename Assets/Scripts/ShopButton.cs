using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    [SerializeField] GameObject m_DefenderPrefab;
    [SerializeField] AudioClip m_NotReadyAudio;
    [SerializeField] TextMeshProUGUI m_PriceText;
    private Slider m_Slider;

    void Start()
    {
        m_PriceText.text = m_DefenderPrefab.GetComponent<Defender>().GetCost().ToString();
        m_Slider = this.GetComponentInChildren<Slider>();
        m_Slider.value = 0f;
        m_Slider.maxValue = m_DefenderPrefab.GetComponent<Defender>().GetRechargeTime();
        StartCoroutine(Recharge());
    }
    void Update()
    {

    }
    public void OnClicked()
    {
        if(m_Slider.value < m_Slider.maxValue)
        {
            Debug.Log("THIS UNIT IS NOT READY");
            BuildManager.instance.SetDefenderToPlace(null);
            AudioSource.PlayClipAtPoint(m_NotReadyAudio, Camera.main.transform.position, PlayerPrefsManager.GetVolume());
            return;
        }

        if (!BuildManager.instance.m_IsBuildable)
        {
            BuildManager.instance.SetDefenderToPlace(null);
            return;
        }

        BuildManager.instance.SetDefenderToPlace(m_DefenderPrefab);
        BuildManager.instance.SetCurrentShopButton(this.GetComponent<ShopButton>());

        foreach(ShopButton shopButton in GameObject.FindObjectsOfType<ShopButton>())
        {
            shopButton.GetComponent<Image>().color = Color.gray;
        }
        this.GetComponent<Image>().color = Color.white;
    }
    IEnumerator Recharge()
    {
        while(m_Slider.value < m_Slider.maxValue)
        {
            m_Slider.value += 1f;
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
    public void StartRecharge()
    {
        m_Slider.value = 0f;
        StartCoroutine(Recharge());
    }
    public Slider GetSlider()
    {
        return m_Slider;
    }
}
