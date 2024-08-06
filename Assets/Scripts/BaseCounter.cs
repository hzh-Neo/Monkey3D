using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CounterType
{
    clearCounter,
    contaionCounter,
    cuttingCounter,
    trashCounter,
    cookCounter,
    NoSet
}

public class BaseCounter : MonoBehaviour
{
    public virtual void interace(Player player) { }
    [SerializeField] protected Transform TopPosition;

    protected KitchenItem kitchenItem;

    public void ClearItem()
    {
        kitchenItem = null;
    }

    public Transform getTransform()
    {
        return TopPosition;
    }

    public bool HasItem()
    {
        return kitchenItem != null;
    }

    public KitchenItem getItem()
    {
        return kitchenItem;
    }

    public void setItem(KitchenItem item)
    {
        kitchenItem = item;
    }

    public virtual CounterType getCounterType() { return CounterType.NoSet; }
}
