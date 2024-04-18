using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Unity.VisualScripting.ReorderableList;
using Unity.VisualScripting;
using System;
using UnityEditor;
using static UnityEditor.Progress;
using UnityEngine.UIElements;
using System.Linq;

public class Inventory : MonoBehaviour
{

    [SerializeField] private List<AssetItem> Items = new List<AssetItem>();
    [SerializeField] private Inventory_Cell _inventory_Cell_Template;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingparent;
    [SerializeField] private Transform _container_quick;
    [SerializeField] private Transform _container_weapons;
    bool _menu_active = false;

    private void Start()
    {

    }

    private void Update()
    {
    }

    /*public void Render(List<AssetItem> Items)
    {
        var inv_cell = new Inventory_Cell();
        for (int i=0; i<Items.Count; i++)
        {
            bool Had_In_Inventory;
            var cell = Instantiate(_inventory_Cell_Template, _container);
            cell.Init(_draggingparent);
            for(int j=0; j<_container.childCount; j++)
            {
                inv_cell = _container.GetChild(j);
                if (Items[i].Name !=  
                {

                }
            }
            cell.Render(Items[i]);
        }

    }*/


    public void Update_Count_Item_In_Cell(int index, AssetItem item)
    {
        Inventory_Cell cell = _container.GetChild(index).GetComponent<Inventory_Cell>();
        cell._count_items.text = item.count_Element.ToString();
    }

    public void Clear_Inventory()
    {
        foreach (Transform child in _container)
        {
            print(child.name);
            Destroy(child.gameObject);
        }
    }

    public void Add_Element_In_Cell(String name, Sprite sprite, String tag)
    {

        bool need_New_Cell = true;
        for (int i = 0; i < _container.childCount; i++)
        {

            _inventory_Cell_Template = _container.GetChild(i).GetComponent<Inventory_Cell>();
            if (_inventory_Cell_Template._namefield.text == name)
            {
                for (int j = 0; j < Items.Count; j++)
                {
                    if (Items[j].Name == name)
                    {
                        Items[j].count_Element++;
                        Update_Count_Item_In_Cell(i, Items[j]);
                        need_New_Cell = false;
                    }

                }

            }

        }

        if (need_New_Cell)
        {
            Create_New_Cell(name, sprite, tag, 1);
        }
    }

    public void Create_New_Cell(String name, Sprite sprite, String tag, int count)
    {
        AssetItem _assetitem = new AssetItem();
        _assetitem._name = name;
        _assetitem._UIIcon = sprite;
        _assetitem.tag = tag;
        _assetitem.count_Element = count;
        Items.Add(_assetitem);
        var cell = Instantiate(_inventory_Cell_Template, _container);
        cell.Init(_draggingparent, _container_weapons, _container_quick, this);
        cell.Render(Items[Items.Count - 1]);

    }

    public void Separate_Item(int Count_items, Inventory_Cell inv_cell)
    {
        if (inv_cell != null)
        {
            foreach (AssetItem item in Items.ToList())
            {
                if (item._name == inv_cell._namefield.text && item.count_Element == int.Parse(inv_cell._count_items.text))
                {
                    int new_count_items = int.Parse(inv_cell._count_items.text) - Count_items;
                    item.count_Element = new_count_items;
                    inv_cell._count_items.text = new_count_items.ToString();
                    Create_New_Cell(item._name, item._UIIcon, item.tag, Count_items);
                    break;
                }
            }
        }

    }

    public void Merging_Items(Inventory_Cell inv_cell, GameObject cell, GameObject merg_cell)
    {
        if (cell != null & merg_cell != null)
        {
            Inventory_Cell inv_merg_cell = merg_cell.GetComponent<Inventory_Cell>();
            foreach (AssetItem item in Items.ToList())
            {
                if (item._name == inv_merg_cell._namefield.text && item.count_Element == int.Parse(inv_merg_cell._count_items.text))
                {
                    int new_count_items = int.Parse(inv_cell._count_items.text) + int.Parse(inv_merg_cell._count_items.text);

                    foreach (AssetItem itm in Items.ToList())
                    {
                        if (itm._name == inv_cell._namefield.text && itm.count_Element == int.Parse(inv_cell._count_items.text))
                        {
                            Items.Remove(itm);
                        }
                    }

                    item.count_Element = new_count_items;
                    inv_merg_cell._count_items.text = new_count_items.ToString();
                    if (inv_cell.cur_field)
                    {
                        Destroy(inv_cell.cur_field);
                    }
                    Destroy(cell);
                    break;
                }
            }
        }
    }
}