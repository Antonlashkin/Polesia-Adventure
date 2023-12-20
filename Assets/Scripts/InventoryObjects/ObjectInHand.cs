using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInHand : MonoBehaviour
{
    //public GameObject handObject;

    public static void AddItemInHand(GameObject hand, GameObject item)
    {
        Instantiate(item, hand.transform.position,item.transform.rotation, hand.transform);
    }

    public static void RemoveItemFromHand(GameObject hand)
    {
        Destroy(hand.transform.GetChild(0).gameObject);
    }
}
