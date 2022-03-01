using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpImage;
    public Image hpEffectImage;
    [SerializeField]
    private float hurtSpeed = 0.005f;

    [SerializeField]
    private BaseEntity entity;

    // Update is called once per frame
    void Update()
    {
        var maxHealth = entity is BaseMeow ? (entity as BaseMeow).MeowObject.Health : 100f;
        hpImage.fillAmount = entity.Health / maxHealth;

        if(hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
        }
        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }
    }
}
