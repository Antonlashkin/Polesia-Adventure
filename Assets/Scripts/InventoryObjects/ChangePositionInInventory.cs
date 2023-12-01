using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ChangePositionInInventory : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool _isDragging = false;
    private GameObject lastSlot;
    private GameObject newSlot;
    private GameObject firstPanel;
    private GameObject secondPanel;
    private GameObject _dropedItem;
    private void Update()
    {
        if (_isDragging)
        {
            firstPanel.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, firstPanel.transform.position.z);
        }
    }

    public void OnPointerDown(PointerEventData pointdown)
    {
        if (pointdown.pointerPressRaycast.gameObject.transform.parent.GetChild(0).GetComponent<Image>().sprite != null)
        {
            firstPanel = pointdown.pointerPressRaycast.gameObject.transform.parent.gameObject;
            lastSlot = firstPanel.transform.parent.gameObject;
            _isDragging = true;
            firstPanel.transform.SetParent(transform.parent.parent);
            firstPanel.GetComponentInChildren<Image>().raycastTarget = false;
        }
    }

    public void OnPointerUp(PointerEventData pointup)
    {
        if (firstPanel == null)
        {
            return;
        }
        firstPanel.transform.position = lastSlot.transform.position;
        firstPanel.transform.SetParent(lastSlot.transform);
        //Debug.Log(secondPanel.name);
        secondPanel = pointup.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
        if (secondPanel.name == "AssociationObject")
        {
            newSlot = secondPanel.transform.parent.gameObject;
            if (secondPanel.transform.GetChild(0).GetComponent<Image>().sprite != null)
            {
                SwapItemsInInventory();
            }
            else
            {
                RemoveItemInInventory();
            }
            //newSlot = null;
            //secondPanel = null;
        }
        else if(secondPanel.transform.parent != null)
        {
            firstPanel.GetComponentInChildren<Image>().raycastTarget = true;
            _isDragging = false;
            return;
        }
        else
        {
            _dropedItem = Instantiate(lastSlot.GetComponent<InventorySlot>().item.itemPrefab);
            _dropedItem.transform.position = new Vector3(0, 0, 0);
            DropItemFromInventory();
        }
        firstPanel.GetComponentInChildren<Image>().raycastTarget = true;
        _isDragging = false;
        //lastSlot = null;
        //firstPanel = null;
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
        Debug.Log("Swap");
    }

    private void RemoveItemInInventory()
    {
        SwapSlot();
        //Sprite imageInNewSlot = firstPanel.transform.GetChild(0).GetComponent<Image>().sprite;
        //Sprite imageInLastSlot = secondPanel.transform.GetChild(0).GetComponent<Image>().sprite;
        //string textInNewSlot = firstPanel.transform.GetChild(1).GetComponent<TMP_Text>().text;
        //string textInLastSlot = secondPanel.transform.GetChild(1).GetComponent<TMP_Text>().text;
        //newSlot.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = imageInNewSlot;
        //newSlot.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //newSlot.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = textInNewSlot;
        //lastSlot.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        //lastSlot.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = imageInLastSlot;
        //lastSlot.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = textInLastSlot;
        Debug.Log("Remove");
    }

    private void DropItemFromInventory()
    {
        Debug.Log("Drop item");
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
}
