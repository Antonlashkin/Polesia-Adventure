using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTipe
{
    Default,
    Food,
    Weapon,
    Instrument
}

public class ItemScriptableObject : ScriptableObject
{
    public ItemTipe itemTipe;
    public string itemName;
    public int maximumAmount;
    public string itemDiscription;
    public GameObject itemPrefab;
    public Sprite icon;
}
