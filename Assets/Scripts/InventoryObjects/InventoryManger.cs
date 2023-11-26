using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManger : MonoBehaviour
{
    [SerializeField] Transform inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] GameObject backgroundPanel;
    private bool isOpenMenu = false;
    void Start()
    {
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
        backgroundPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isOpenMenu)
            {
                backgroundPanel.SetActive(false);
                inventoryPanel.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isOpenMenu = false;
            }
            else
            {
                backgroundPanel.SetActive(true);
                inventoryPanel.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                isOpenMenu = true;
            }
        }
    }

}
