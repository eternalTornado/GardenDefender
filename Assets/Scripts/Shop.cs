using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject m_Fallen1Prefab;
    [SerializeField] GameObject m_Fallen2Prefab;
    [SerializeField] GameObject m_Fallen3Prefab;
    [SerializeField] GameObject m_GoldMinePrefab;

    private ShopButton[] m_ShopButtons;

    void Start()
    {
        m_ShopButtons = this.GetComponentsInChildren<ShopButton>();
    }

    public void PurchaseFallenAngel1()
    {
        BuildManager.instance.SetCurrentShopButton(m_ShopButtons[0]);
        if(m_ShopButtons[0].GetSlider().value < 1f)
        {
            Debug.Log("UNIT NOT READY");
            BuildManager.instance.SetDefenderToPlace(null);
        }
        else
        {
            Debug.Log("UNIT 1 CHOSEN");
            BuildManager.instance.SetDefenderToPlace(m_Fallen1Prefab);
        }
    }
    public void PurchaseFallenAngel2()
    {
        BuildManager.instance.SetCurrentShopButton(m_ShopButtons[1]);
        if(m_ShopButtons[1].GetSlider().value < 1f)
        {
            Debug.Log("UNIT NOT READY");
            BuildManager.instance.SetDefenderToPlace(null);
        }
        else
        {
            Debug.Log("UNIT 2 CHOSEN");
            BuildManager.instance.SetDefenderToPlace(m_Fallen2Prefab);
        }
    }
    public void PurchaseFallenAngel3()
    {
        BuildManager.instance.SetCurrentShopButton(m_ShopButtons[2]);
        if(m_ShopButtons[2].GetSlider().value < 1f)
        {
            Debug.Log("UNIT NOT READY");
            BuildManager.instance.SetDefenderToPlace(null);
        }
        else
        {
            Debug.Log("UNIT 3 CHOSEN");
            BuildManager.instance.SetDefenderToPlace(m_Fallen3Prefab);
        }
    }
    public void PurchaseGoldMine()
    {
        BuildManager.instance.SetCurrentShopButton(m_ShopButtons[3]);
        if(m_ShopButtons[3].GetSlider().value < 1f)
        {
            Debug.Log("UNIT NOT READY");
            BuildManager.instance.SetDefenderToPlace(null);
        }
        else
        {
            Debug.Log("UNIT GOLD MINE CHOSEN");
            BuildManager.instance.SetDefenderToPlace(m_GoldMinePrefab);
        }
    }
}
