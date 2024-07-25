using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObject something;

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
        }
    }

    public void playerHandle()
    {

    }

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

    public override CounterType getCounterType() { 
        return CounterType.clearCounter; 
    }
}
