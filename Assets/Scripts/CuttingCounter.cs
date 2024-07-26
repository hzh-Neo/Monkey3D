using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    private int cutProgress;
    private bool isCutting;
    private KitchenItem food;

    public override void interace(Player player)
    {
        if (isCutting)
        {
            stopCutFood();
            return;
        }
        if (player.HasItem())
        {
            if (!HasItem())
            {
                KitchenObject sliceItem = player.getItem().getItemSlice();
                if (sliceItem != null)
                {
                    food = player.getItem();
                    startCutFood();
                }
            }
        }
        else
        {
            if (HasItem())
            {
                player.setItem(getItem());
                getItem().setClearCounter(player);
            }
        }
    }

    private void Update()
    {
        if (isCutting)
        {
            cutProgress++;
            if (cutProgress >= 100)
            {
                isCutting = false;
                cutProgress = 0;
                if (food != null && food.getItemSlice() != null)
                {
                    endCutFood(food.getItemSlice());
                }
            }
        }
    }

    private void stopCutFood()
    {
        isCutting = false;
        cutProgress = 0;
        endCutFood(food.getItem());

    }

    private void startCutFood()
    {
        isCutting = true;
        cutProgress = 0;
    }

    private void endCutFood(KitchenObject kobj)
    {
        Transform trans = Instantiate(kobj.prefab, TopPosition);
        if (trans.TryGetComponent<KitchenItem>(out kitchenItem))
        {
            setItem(kitchenItem);
            kitchenItem.setClearCounter(this);
        }
        food.Destory();
    }

    public override CounterType getCounterType()
    {
        return CounterType.cuttingCounter;
    }
}
