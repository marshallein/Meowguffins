using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour
{
    public string textToDisplay;
    public TextMesh textMesh;

    private void Awake()
    {
        textMesh.text = textToDisplay;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("detect");
        if (collision.gameObject.tag == "Player")
        {
            textMesh?.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        Debug.Log("detect out");
        if (collision.gameObject.tag == "Player")
        {
            textMesh?.gameObject.SetActive(false);
        }
    }
}
