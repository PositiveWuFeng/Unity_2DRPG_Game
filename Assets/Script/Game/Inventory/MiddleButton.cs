using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiddleButton : MonoBehaviour,IPointerClickHandler
{
    private InventoryScript inventoryScript;

    private void Awake()
    {
        inventoryScript=GetComponentInParent<InventoryScript>();
    }
    private void Start()
    {
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            InventoryManager.MyInstance.UseItem(inventoryScript.name);
        }
    }
}
