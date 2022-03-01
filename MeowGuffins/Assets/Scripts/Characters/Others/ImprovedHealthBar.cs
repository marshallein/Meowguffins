using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImprovedHealthBar : MonoBehaviour
{
    public Image hpImage;
    public Image hpEffectImage;
    private float hp; 
    private float maxHp;
    private GameObject inActiveGameObjects;
    private string currentMeowName;
    private GameObject currentGameOject;
    private float hurtSpeed = 0.005f;
    private GameObject temp;
    void Start()
    {
        inActiveGameObjects = FindInActiveObjectByTag("Player");
        currentGameOject = GameObject.FindGameObjectWithTag("Player");
        currentMeowName = currentGameOject.name;
        maxHp = currentGameOject.GetComponent<HealthManager>().maxHp;
        hp = maxHp;
        temp = new GameObject();
    }

    GameObject FindInActiveObjectByTag(string tag)
    {
        int count = 0;
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {
                    if(objs[i].gameObject.name != currentMeowName)
                    {
                        return objs[i].gameObject;
                    }
                    count++;
                }
            }
        }
        return null;
    }


    [System.Obsolete]
    void Update()
    {
        if (!currentGameOject.active)
        {
            temp = currentGameOject;
            currentGameOject = inActiveGameObjects;
            inActiveGameObjects = temp;
        } 
        maxHp = currentGameOject.GetComponent<HealthManager>().maxHp;
        hp = currentGameOject.GetComponent<HealthManager>().hp;

        hpImage.fillAmount = hp / maxHp;

        if (hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
        }
        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }
    }
}
