using System;
using System.Collections;
using UnityEngine;

// Custom attribute to mark fields as read-only
public class UpdateProgress : EventArgs
{
    public float progress;
}

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    private Animator anim;
    public event EventHandler<UpdateProgress> handleUpdateProgress;
    private float cutProgress = 0;
    private float maxProgress = 100;
    public Color foodColor = new Color(255, 214, 92, 255);
    private bool isCutting;
    private KitchenItem food;
    private bool isCoroutineRunning = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

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
                    foodColor = player.getItem().kitchenObject.color;
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
            if (!isCoroutineRunning)
            {
                StartCoroutine(incubeProgress());
            }

            if (cutProgress >= GameData.Instance.maxCutProgress)
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

    private IEnumerator incubeProgress()
    {
        isCoroutineRunning = true;
        cutProgress += GameData.Instance.cutSpeed;
        yield return new WaitForSeconds(0.03f);
        handleUpdateProgress.Invoke(this, new UpdateProgress() { progress = cutProgress / maxProgress });
        isCoroutineRunning = false;
    }

    private void stopCutFood()
    {
        isCutting = false;
        cutProgress = 0;
        endCutFood(food.getItem());
        handleUpdateProgress.Invoke(this, new UpdateProgress() { progress = cutProgress / maxProgress });
    }

    private void startCutFood()
    {
        isCutting = true;
        cutProgress = 0;
        //food.Hide();
        setItem(food);
        food.setClearCounter(this);
        anim.SetBool("Cutting", true);
    }

    private void endCutFood(KitchenObject kobj)
    {
        anim.SetBool("Cutting", false);
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
