using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneScript : MonoBehaviour
{
    private EnenyMeleeBehaviour m_parent;

    private void Awake()
    {
        m_parent = GetComponentInParent<EnenyMeleeBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            m_parent.target = collision.transform;
            m_parent.inRange = true;
            m_parent.hotZone.SetActive(true);
        }
    }
}
