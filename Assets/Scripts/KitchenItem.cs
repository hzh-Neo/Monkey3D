using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (clearCounter == null)
        {
            transform.SetParent(null);
            return;
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

    public KitchenObject getItem()
    {
        return kitchenObject;
    }

    public KitchenObject getItemSlice()
    {
        return kitchenObject.prefabSlice;
    }

    public bool IsSlice()
    {
        return kitchenObject.prefabSlice == null || kitchenObject.isSlice;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Destory()
    {
        GameObject.Destroy(gameObject);
    }
}
