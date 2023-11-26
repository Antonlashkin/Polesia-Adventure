using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public int amount;
    public bool isEmpty = true;
    public GameObject icon;
    public TMP_Text itemAmountText;

    private void Start()
    {
        icon = transform.GetChild(0).gameObject;
        itemAmountText = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void SetIcon(Sprite _icon)
    {
        icon.GetComponent<Image>().sprite = _icon;
        icon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
}

