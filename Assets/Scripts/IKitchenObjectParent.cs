using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public void ClearItem();

    public Transform getTransform();

    public bool HasItem();

    public KitchenItem getItem();

    public void setItem(KitchenItem item);

}
