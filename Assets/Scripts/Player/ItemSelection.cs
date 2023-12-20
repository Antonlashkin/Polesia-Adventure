using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelection : MonoBehaviour
{
    [SerializeField] private GameObject Text;
    [SerializeField] private LayerMask Item;
    [SerializeField] GameObject inventory;

    // Update is called once per frame
    void Update()
    {
        Collider2D collision = Physics2D.OverlapBox(transform.position, new Vector2(4, 4), 0, Item);
        if (collision != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject destroyalble = collision.gameObject;
                if (AddItem(destroyalble.GetComponent<Item>().scriptableObject, destroyalble.GetComponent<Item>().amount))
                {
                    Destroy(destroyalble);
                }
                else
                {
                    Debug.Log("Full Inventory!");
                }
            }
            Text.SetActive(true);
        }
        else
        {
            Text.SetActive(false);
        }
        
    }


    private bool AddItem(ItemScriptableObject _item, int _amount)
    {
        foreach (InventorySlot slot in inventory.GetComponent<InventoryManger>().slots)
        {
            if (slot.item == _item)
            {
                if (slot.amount + _amount <= _item.maximumAmount)
                {
                    slot.amount += _amount;
                    slot.itemAmountText.text = slot.amount.ToString();
                    slot.isEmpty = false;
                    return true;
                }
                else if (slot.amount < _item.maximumAmount)
                {
                    _amount = slot.amount + _amount - _item.maximumAmount;
                    slot.amount = _item.maximumAmount;
                    slot.itemAmountText.text = slot.amount.ToString();
                }
            }
            else if (slot.isEmpty)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.SetIcon(_item.icon);
                slot.itemAmountText.text = slot.amount.ToString(); 
                slot.isEmpty = false;
                return true;
            }
        }
        return false;
    }
}
