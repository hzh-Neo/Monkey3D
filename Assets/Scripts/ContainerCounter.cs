using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
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
}
