using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItem : Item
{
    [SerializeField]
    protected DamageScriptable damageSO;
    public DamageScriptable DamageSO { get => damageSO; }

    public override void UseOn(BaseEntity entity)
    {
        base.UseOn(entity);

        var meow = entity as BaseMeow;
        meow.BoostDamage(damageSO);
    }

    protected override bool CanBeUsedOn(BaseEntity entity)
    {
        return base.CanBeUsedOn(entity) && entity is BaseMeow;
    }
}
