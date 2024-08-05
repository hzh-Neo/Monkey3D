using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "table/����������Ʒ", fileName = "createTableOn")]
public class KitchenObject : ScriptableObject
{
    public Transform prefab;
    public KitchenObject prefabSlice;
    public Sprite sprite;
    public string itemName;
    public bool isSlice;
    public Color color;
}
