using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject m_StandardDefender;
    //private attribues
    private GameObject m_DefenderToPlace;
    private ShopButton m_CurrentShopButton;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        m_DefenderToPlace = null;
        m_CurrentShopButton = null;
    }
    public GameObject GetDefenderToPlace()
    {
        return m_DefenderToPlace;
    }
    public void SetDefenderToPlace(GameObject prefab)
    {
        m_DefenderToPlace = prefab;
    }
    public ShopButton GetCurrentShopButton()
    {
        return m_CurrentShopButton;
    }
    public void SetCurrentShopButton(ShopButton shopButton)
    {
        m_CurrentShopButton = shopButton;
    }
}
