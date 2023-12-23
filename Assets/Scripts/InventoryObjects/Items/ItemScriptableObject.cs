using UnityEngine;

public enum ItemTipe
{
    Default,
    Food,
    Weapon,
    Instrument,
    Component
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
