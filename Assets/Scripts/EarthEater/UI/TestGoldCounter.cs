using System;
using System.Collections;
using System.Collections.Generic;
using Common.InventorySystem;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TestGoldCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private InventoryItemsManager itemsManager;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = string.Empty;
    }

    private void OnDestroy()
    {
        if (itemsManager != null)
        {
            itemsManager.InventoryChanged -= ItemsManager_OnInventoryChanged;
        }
    }

    public void Initialize(InventoryItemsManager itemsManager)
    {
        this.itemsManager = itemsManager;
        itemsManager.InventoryChanged += ItemsManager_OnInventoryChanged;
    }

    private void ItemsManager_OnInventoryChanged()
    { 
        if (itemsManager.StackableItems.TryGetValue(typeof(StackableItem), out IAmStackableItem stackableItem))
        {
            text.text = $"{stackableItem.StackAmount}";
        }
        else
        {
            text.text = string.Empty;
        }
    }
}
