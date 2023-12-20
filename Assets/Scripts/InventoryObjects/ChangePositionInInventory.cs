using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangePositionInInventory : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool _isDragging = false;
    bool _isLeft = false;
    private GameObject lastSlot;
    private GameObject newSlot;
    private GameObject firstPanel;
    private GameObject secondPanel;
    private GameObject _dropedItem;
    [SerializeField] GameObject person;

    private void Update()
    {
        if (_isDragging && _isLeft)
        {
            firstPanel.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, firstPanel.transform.position.z);
        }
    }

    public void OnPointerDown(PointerEventData pointdown)
    {
        if (pointdown.button == PointerEventData.InputButton.Left)
        {
            _isLeft = true;
        }
        else
        {
            _isLeft = false;
        }
        if (pointdown.pointerPressRaycast.gameObject.transform.parent.GetChild(0).GetComponent<Image>().sprite != null && _isLeft)
        {
            firstPanel = pointdown.pointerPressRaycast.gameObject.transform.parent.gameObject;
            lastSlot = firstPanel.transform.parent.gameObject;
            _isDragging = true;
            firstPanel.transform.SetParent(transform.parent.parent);
            firstPanel.GetComponentInChildren<Image>().raycastTarget = false;
        }
        if (!_isLeft)
        {
            //menu
        }
    }

    public void OnPointerUp(PointerEventData pointup)
    {
        if (_isLeft)
        {
            if (firstPanel == null)
            {
                return;
            }
            firstPanel.transform.position = lastSlot.transform.position;
            firstPanel.transform.SetParent(lastSlot.transform);
            secondPanel = pointup.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
            GameObject inventoryPanel = pointup.pointerCurrentRaycast.gameObject;
            if (secondPanel.name == "AssociationObject")
            {
                newSlot = secondPanel.transform.parent.gameObject;
                if (secondPanel.transform.GetChild(0).GetComponent<Image>().sprite != null)
                {
                    //Add items in stack
                    if (lastSlot.GetComponent<InventorySlot>().item.name == newSlot.GetComponent<InventorySlot>().item.name && int.Parse(secondPanel.transform.GetChild(1).GetComponent<TMP_Text>().text) != newSlot.GetComponent<InventorySlot>().item.maximumAmount)
                    {
                        if (lastSlot.GetComponent<InventorySlot>().amount + newSlot.GetComponent<InventorySlot>().amount > newSlot.GetComponent<InventorySlot>().item.maximumAmount)
                        {
                            lastSlot.GetComponent<InventorySlot>().amount -= newSlot.GetComponent<InventorySlot>().item.maximumAmount - newSlot.GetComponent<InventorySlot>().amount;
                            firstPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = (lastSlot.GetComponent<InventorySlot>().amount).ToString();
                            newSlot.GetComponent<InventorySlot>().amount = newSlot.GetComponent<InventorySlot>().item.maximumAmount;
                            secondPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = (newSlot.GetComponent<InventorySlot>().amount).ToString();
                        }
                        else
                        {
                            newSlot.GetComponent<InventorySlot>().amount = newSlot.GetComponent<InventorySlot>().amount + lastSlot.GetComponent<InventorySlot>().amount;
                            secondPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = (newSlot.GetComponent<InventorySlot>().amount).ToString();
                            DeleteItemFromInventory();
                        }
                        Debug.Log("Add");
                    }
                    else
                    {
                        SwapItemsInInventory();
                    }
                }
                else
                {
                    RemoveItemInInventory();
                }

                if (lastSlot.name == "RightHand")
                {
                    ObjectInHand.RemoveItemFromHand(person.transform.GetChild(4).gameObject);
                    if (lastSlot.transform.GetComponent<InventorySlot>().item != null)
                    {
                        ObjectInHand.AddItemInHand(person.transform.GetChild(4).gameObject, lastSlot.gameObject.GetComponent<InventorySlot>().item.itemPrefab);
                    }
                }
                else if (lastSlot.name == "LeftHand")
                {
                    ObjectInHand.RemoveItemFromHand(person.transform.GetChild(5).gameObject);
                    if (lastSlot.transform.GetComponent<InventorySlot>().item != null)
                    {
                        ObjectInHand.AddItemInHand(person.transform.GetChild(5).gameObject, lastSlot.gameObject.GetComponent<InventorySlot>().item.itemPrefab);
                    }
                }
                if (newSlot.name == "RightHand")
                {
                    if (person.transform.GetChild(4).childCount != 0)
                    {
                        ObjectInHand.RemoveItemFromHand(person.transform.GetChild(4).gameObject);
                    }
                    ObjectInHand.AddItemInHand(person.transform.GetChild(4).gameObject, newSlot.gameObject.GetComponent<InventorySlot>().item.itemPrefab);
                }
                else if (newSlot.name == "LeftHand")
                {
                    if (person.transform.GetChild(5).childCount != 0)
                    { 
                        ObjectInHand.RemoveItemFromHand(person.transform.GetChild(5).gameObject);
                    }
                    ObjectInHand.AddItemInHand(person.transform.GetChild(5).gameObject, newSlot.gameObject.GetComponent<InventorySlot>().item.itemPrefab);
                }
            }
            else if (secondPanel.transform.parent != null || inventoryPanel.name == "InvrntoryPanel" || inventoryPanel.name == "QuickSlots" || inventoryPanel.name == "Hands")
            {
                firstPanel.GetComponentInChildren<Image>().raycastTarget = true;
                _isDragging = false;
                return;
            }
            else
            {
                if (lastSlot.name == "RightHand")
                {
                    ObjectInHand.RemoveItemFromHand(person.transform.GetChild(4).gameObject);
                }
                else if (lastSlot.name == "LeftHand")
                {
                    ObjectInHand.RemoveItemFromHand(person.transform.GetChild(5).gameObject);
                }
                //Delete item from inventory
                firstPanel.GetComponentInChildren<Image>().raycastTarget = true;
                _isDragging = false;
                DropItemFromInventory();
            }


            firstPanel.GetComponentInChildren<Image>().raycastTarget = true;
            _isDragging = false;
        }
    }


    private void SwapItemsInInventory()
    {
        SwapSlot();
        Sprite imageInLastSlot = secondPanel.transform.GetChild(0).GetComponent<Image>().sprite;
        Sprite imageInNewSlot = firstPanel.transform.GetChild(0).GetComponent<Image>().sprite;
        string textInLastSlot = secondPanel.transform.GetChild(1).GetComponent<TMP_Text>().text;
        string textInNewSlot = firstPanel.transform.GetChild(1).GetComponent<TMP_Text>().text;
        lastSlot.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = imageInLastSlot;
        lastSlot.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = textInLastSlot;
        newSlot.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = imageInNewSlot;
        newSlot.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = textInNewSlot;
    }

    private void RemoveItemInInventory()
    {
        SwapSlot();
    }

    private void DropItemFromInventory()
    {
        _dropedItem = Instantiate(lastSlot.GetComponent<InventorySlot>().item.itemPrefab);
        _dropedItem.GetComponent<Item>().amount = lastSlot.GetComponent<InventorySlot>().amount;
        _dropedItem.transform.position = new Vector3(person.transform.position.x, person.transform.position.y, person.transform.position.z);

        DeleteItemFromInventory();
    }



    private void SwapSlot()
    {
        firstPanel.transform.SetParent(newSlot.transform);
        secondPanel.transform.SetParent(lastSlot.transform);

        firstPanel.transform.position = newSlot.transform.position;
        secondPanel.transform.position = lastSlot.transform.position;

        InventorySlot swapSlot = new InventorySlot();
        swapSlot.amount = lastSlot.GetComponent<InventorySlot>().amount;
        swapSlot.item = lastSlot.GetComponent<InventorySlot>().item;
        swapSlot.isEmpty = lastSlot.GetComponent<InventorySlot>().isEmpty;
        swapSlot.icon = lastSlot.GetComponent<InventorySlot>().icon;
        swapSlot.itemAmountText = lastSlot.GetComponent<InventorySlot>().itemAmountText;

        lastSlot.GetComponent<InventorySlot>().amount = newSlot.GetComponent<InventorySlot>().amount;
        lastSlot.GetComponent<InventorySlot>().item = newSlot.GetComponent<InventorySlot>().item;
        lastSlot.GetComponent<InventorySlot>().isEmpty = newSlot.GetComponent<InventorySlot>().isEmpty;
        lastSlot.GetComponent<InventorySlot>().icon = newSlot.GetComponent<InventorySlot>().icon;
        lastSlot.GetComponent<InventorySlot>().itemAmountText = newSlot.GetComponent<InventorySlot>().itemAmountText;

        newSlot.GetComponent<InventorySlot>().amount = swapSlot.amount;
        newSlot.GetComponent<InventorySlot>().item = swapSlot.item;
        newSlot.GetComponent<InventorySlot>().isEmpty = swapSlot.isEmpty;
        newSlot.GetComponent<InventorySlot>().icon = swapSlot.icon;
        newSlot.GetComponent<InventorySlot>().itemAmountText = swapSlot.itemAmountText;
    }

    private void DeleteItemFromInventory()
    {
        lastSlot.GetComponent<InventorySlot>().amount = 0;
        lastSlot.GetComponent<InventorySlot>().item = null;
        lastSlot.GetComponent<InventorySlot>().isEmpty = true;
        lastSlot.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        lastSlot.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = null;
        lastSlot.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
    }
}