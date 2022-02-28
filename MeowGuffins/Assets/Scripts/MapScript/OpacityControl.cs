using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpacityControl : MonoBehaviour
{
    Tilemap m_tileMap;


    // Start is called before the first frame update
    void Start()
    {
        m_tileMap = GetComponent<Tilemap>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player enter the leaf");
            m_tileMap.color = new Color(1f, 1f, 1f, 0.4f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            m_tileMap.color = new Color(1f, 1f, 1f, 1f);
        }

    }
}
