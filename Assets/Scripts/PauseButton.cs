using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClicked()
    {
        //To prevent player from placing through pause Menu
        BuildManager.instance.SetDefenderToPlace(null);
        BuildManager.instance.m_IsBuildable = false;
    }
    public void MakeBuildable()
    {
        BuildManager.instance.m_IsBuildable = true;
    }
}
