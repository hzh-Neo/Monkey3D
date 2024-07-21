using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    public KitchenObject kitchenObject;

    private IKitchenObjectParent CC;

    public void setClearCounter(IKitchenObjectParent clearCounter)
    {
        if (CC != null)
        {
            CC.ClearItem();
            removeCounter();
        }
        CC = clearCounter;
        transform.parent = clearCounter.getTransform();
        transform.localPosition = Vector3.zero;
    }


    public KitchenItem getClearCounter()
    {
        return CC.getItem();
    }

    public void removeCounter()
    {
        CC = null;
    }

    public bool HasItem()
    {
        return kitchenObject != null;
    }
}
