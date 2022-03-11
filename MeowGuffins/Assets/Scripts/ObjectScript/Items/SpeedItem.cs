using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item
{
    [SerializeField]
    protected SpeedScriptable speedSO;
    public SpeedScriptable SpeedSO { get => speedSO; }

    public override void UseOn(BaseEntity entity)
    {
        base.UseOn(entity);

        var meow = entity as BaseMeow;
        
        if (speedSO.isEternal == true)
        {
            meow.AddEternalItem(speedSO);
        }
        else
        {
            meow.moveController.BoostSpeed(speedSO);
        }
    }

    protected override bool CanBeUsedOn(BaseEntity entity)
    {
        return base.CanBeUsedOn(entity) && entity is BaseMeow;
    }
}
