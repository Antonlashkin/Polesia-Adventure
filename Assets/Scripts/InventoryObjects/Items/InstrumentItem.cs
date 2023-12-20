using UnityEngine;

[CreateAssetMenu(fileName = "Food Item", menuName = "Inventory/Items/New Instrument Item")]

public class InstrumentItem : ItemScriptableObject
{
    public float stateAmount;

    public void Start()
    {
        itemTipe = ItemTipe.Instrument;
    }
}
