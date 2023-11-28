using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ChangePositionInInventory : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool _isDragging = false;
    private GameObject oldSlot;
    private GameObject firstPanel;
    private GameObject secondPanel;
    private void Update()
    {
        if (_isDragging)
        {
            firstPanel.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, firstPanel.transform.position.z);
        }
    }

    public void OnPointerDown(PointerEventData pointdown)
    {
        //Debug.Log(gameObject.name);
        //Debug.Log(pointdown.pointerPressRaycast.gameObject.name);
        //oldSlot = firstPanel.transform.parent.gameObject;
        if (pointdown.pointerPressRaycast.gameObject.transform.parent.GetChild(0).GetComponent<Image>().sprite != null)
        {
            firstPanel = pointdown.pointerPressRaycast.gameObject.transform.parent.gameObject;
            _isDragging = true;
            firstPanel.transform.SetParent(transform.parent.parent);
            firstPanel.GetComponentInChildren<Image>().raycastTarget = false;
        }
    }

    public void OnPointerUp(PointerEventData pointup)
    {
        //Debug.Log(gameObject.name);
        //Debug.Log(pointup.pointerPressRaycast.gameObject.name);
        if (firstPanel == null)
        {
            return;
        }
        //transform.SetParent(oldSlot.transform);
        //transform.position = oldSlot.transform.position;
        secondPanel = pointup.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
        Debug.Log(secondPanel.name);
        //Debug.Log(secondPanel.transform.parent.name);
        if (secondPanel.name == "AssociationObject")
        {
            if (secondPanel.transform.GetChild(0).GetComponent<Image>().sprite != null)
            {
                Debug.Log("Change");
            }
            else
            {
                Debug.Log("Remove");
            }
        }
        else
        {
            Debug.Log("Drop item");
        }
        _isDragging = false;
    }
}
