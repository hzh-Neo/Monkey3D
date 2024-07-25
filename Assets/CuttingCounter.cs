using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    public override void interace(Player player)
    {
        if (player.HasItem())
        {

        }
    }

    public override CounterType getCounterType()
    {
        return CounterType.cuttingCounter;
    }
}
