using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] GameObject m_DefenderPrefab;
    [SerializeField] AudioClip m_NotReadyAudio;
    private Slider m_Slider;

    void Start()
    {
        m_Slider = this.GetComponentInChildren<Slider>();
        m_Slider.value = 0f;
        StartCoroutine(Recharge());
    }
    void Update()
    {

    }
    public void OnClicked()
    {
        if(m_Slider.value < 1f)
        {
            Debug.Log("THIS UNIT IS NOT READY");
            BuildManager.instance.SetDefenderToPlace(null);
            AudioSource.PlayClipAtPoint(m_NotReadyAudio, Camera.main.transform.position, PlayerPrefsManager.GetVolume());
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
        while(m_Slider.value < 1f)
        {
            m_Slider.value += 0.001f;
            yield return new WaitForSeconds(0.01f);
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
