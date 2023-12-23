using UnityEngine;

[CreateAssetMenu(fileName = "Droped Item", menuName = "Inventory/Items/New Destroyable Item")]
public class ScriptableDestroyableObject : ScriptableObject
{
    public float maxStateAmount;
    public GameObject DropedItemPrefab;
    public string itemToDestroy;
}
