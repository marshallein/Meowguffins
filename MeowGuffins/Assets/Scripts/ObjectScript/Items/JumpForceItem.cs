using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForceItem : Item
{
    [SerializeField]
    protected JumpForceScriptable jumpForceSO;
    public JumpForceScriptable JumpForceSO { get => jumpForceSO; }

    public override void UseOn(BaseEntity entity)
    {
        base.UseOn(entity);

        var meow = entity as BaseMeow;

        if (jumpForceSO.isEternal == true)
        {
            meow.AddEternalItem(jumpForceSO);
        }
        else
        {
            meow.moveController.BoostJumpForce(jumpForceSO);
        }
    }

    protected override bool CanBeUsedOn(BaseEntity entity)
    {
        return base.CanBeUsedOn(entity) && entity is BaseMeow;
    }
}
