using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : SingletonMonoBehaviour<Inventory>
{
    private List<Item> _items;

    public static Action<Item> OnItemAdded;
    public static Action<Item> OnItemRemoved;

    public void AddItem(Item item)
    {
        _items.Add(item);
        OnItemAdded?.Invoke(item);
    }
    
    public void RemoveItem(Item item)
    {
        _items.Remove(item);
        OnItemRemoved?.Invoke(item);
    }
}
