using UnityEngine;

[CreateAssetMenu(fileName = "Component Item", menuName = "Inventory/Items/New Component Item")]

public class ComponentItem : ItemScriptableObject
{
    public void Start()
    {
        itemTipe = ItemTipe.Component;
    }
}
