using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private bool used = false;

    public virtual void UseOn(BaseEntity entity)
    {
        Destroy(gameObject);
        used = true;
    }

    protected virtual bool CanBeUsedOn(BaseEntity entity)
    {
        return !used;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        BaseEntity entity = col.GetComponent<BaseEntity>();
        if (!CanBeUsedOn(entity)) return;
        UseOn(entity);
    }
}
