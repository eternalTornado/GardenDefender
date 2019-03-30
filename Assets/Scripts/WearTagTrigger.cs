using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearTagTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Untagged" && collider.GetComponent<EnemyHealth>()!=null)
            collider.gameObject.tag = "Enemy";
    }

}
