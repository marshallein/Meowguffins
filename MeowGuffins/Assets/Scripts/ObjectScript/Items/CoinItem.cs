using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CoinItem: Item
{
    [SerializeField]
    protected CoinScriptable coinSO;
    public CoinScriptable CoinSO { get => coinSO; }

    public override void UseOn(BaseEntity entity)
    {
        base.UseOn(entity);
        EventManager.OnCoinCollected();
    }
    protected override bool CanBeUsedOn(BaseEntity entity)
    {
        return base.CanBeUsedOn(entity) && entity is BaseMeow;
    }
}

