using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] Color m_HoverColor;
    //Put private attributes here
    private Color origin;
    private Vector3 m_OriginalPos;
    private Renderer m_Renderer;
    private GameObject m_Defender;
    void Start()
    {
        m_OriginalPos = this.transform.position;
        m_Renderer = GetComponent<Renderer>();
        origin = m_Renderer.material.color;
        //m_Renderer.material.color = new Color(origin.r, origin.g, origin.b);
        InvokeRepeating("UpdateDefenderStatus", 0f, 1f);
    }
    void OnMouseOver()
    {
        m_Renderer.material.color = m_HoverColor;
    }
    void OnMouseExit()
    {
        m_Renderer.material.color = origin;
    }
    void OnMouseDown()
    {
        if (m_Defender != null)
        {
            Debug.Log("Can't build there");
            return;
        }
        if (BuildManager.instance.GetDefenderToPlace() == null)
            return; 

        GameObject defender = BuildManager.instance.GetDefenderToPlace();
        float defenderCost = defender.GetComponent<Defender>().GetCost();
        if (ResourceManager.instance.GetResource() < defenderCost)
        {
            Debug.Log("NOT ENOUGH CURRENCY TO HIRE THIS UNIT");
            return;
        }

        m_Defender = Instantiate(defender, this.transform.position, Quaternion.identity);
        m_Defender.transform.parent = this.transform;
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
        ResourceManager.instance.UpdateResourceText(-defenderCost);
        BuildManager.instance.GetCurrentShopButton().StartRecharge();
    }
    private void UpdateDefenderStatus()
    {
        if (m_Defender != null)
            return;

        this.transform.position = m_OriginalPos;
    }
}
