using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float m_MoveUpSpeed;
    [SerializeField] float m_TimeToDestroySelf = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * m_MoveUpSpeed * Time.deltaTime);
        Destroy(this.gameObject, m_TimeToDestroySelf);
    }
}
