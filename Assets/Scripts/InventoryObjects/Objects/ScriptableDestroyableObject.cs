using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food Item", menuName = "Inventory/Items/New Destroyable Item")]
public class ScriptableDestroyableObject : ScriptableObject
{
    public float maxStateAmount;
    public GameObject DropedItemPrefab;
    public string itemToDestroy;
}
