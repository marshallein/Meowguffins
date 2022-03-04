using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImprovedHealthBar : MonoBehaviour
{
    public Image hpImage;
    public Image hpEffectImage;
    private BaseMeow inactiveMeow;
    private string currentMeowName;
    private BaseMeow currentMeow;
    private float hurtSpeed = 0.005f;
    void Start()
    {
        inactiveMeow = FindInActiveObjectByTag("Player").GetComponent<BaseMeow>();
        currentMeow = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseMeow>();
        currentMeowName = currentMeow.name;
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
                    if (objs[i].gameObject.name != currentMeowName)
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
        if (!currentMeow.enabled)
        {
            var temp = currentMeow;
            currentMeow = inactiveMeow;
            inactiveMeow = temp;
        }
        var active = MeowObjectManager.Instance.ActiveMeow;
        var maxHp = active.MeowObject.Health;
        var hp = active.Health;

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
