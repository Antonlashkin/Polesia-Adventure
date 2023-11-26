using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Food Item", menuName ="Inventory/Items/New Food Item")]
public class FoodItem : ItemScriptableObject
{
    public float healAmount;

    public void Start()
    {
        itemTipe = ItemTipe.Food;
    }
}
