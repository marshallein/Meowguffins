using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    [SerializeField]
    protected HealthScriptable healthSO;
    public HealthScriptable HealthSO { get => healthSO; }

    public override void UseOn(BaseEntity entity)
    {
        base.UseOn(entity);

        entity.Heal(healthSO.amount);
    }

    protected override bool CanBeUsedOn(BaseEntity entity)
    {
        return base.CanBeUsedOn(entity) && entity is BaseMeow;
    }
}
