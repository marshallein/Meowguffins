using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMapControll : MonoBehaviour
{

    public GameObject Boss;
    public GameObject SpikeStart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Boss.SetActive(true);
            SpikeStart.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
