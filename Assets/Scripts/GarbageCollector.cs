using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }
}
