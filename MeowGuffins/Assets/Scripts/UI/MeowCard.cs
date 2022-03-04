using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeowCard : MonoBehaviour
{
    [SerializeField]
    private Text selectedText;
    [SerializeField]
    private MeowObject meow;
    public MeowObject Meow => meow;
    [SerializeField]
    private Image image;

    private void Start()
    {
        image.sprite = meow.menuSprite;
    }

    public void OnClick()
    {
        SelectionScript.Instance.SelectCard(this);
    }

    public void Activate()
    {
        selectedText.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        selectedText.gameObject.SetActive(false);
    }
}
