using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObject something;
    [SerializeField] private Transform TopPosition;

    private KitchenItem kitchenItem;

    public override void interace(Player player)
    {
        if (HasItem())
        {
            if (!player.HasItem())
            {
                player.setItem(getItem());
                getItem().setClearCounter(Player.Instance);
            }
        }
        else
        {
            if (player.HasItem())
            {
                setItem(player.getItem());
                player.getItem().setClearCounter(this);
            }
            else
            {
                Transform trans = Instantiate(something.prefab, TopPosition);
                if (trans.TryGetComponent<KitchenItem>(out kitchenItem))
                {
                    setItem(kitchenItem);
                    kitchenItem.setClearCounter(this);
                }
            }
        }
    }

    public void playerHandle()
    {

    }
    /*******************************************************************************/

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

    /*******************************************************************************/

    public void setKitchenObj(KitchenObject _kitchenObject)
    {
        something = _kitchenObject;
    }

    public KitchenObject getKitchenObj()
    {
        return something;
    }

    public bool HasKitchenObj()
    {
        return something != null;
    }

    public void ClearKitchenObj()
    {
        something = null;
    }

}
