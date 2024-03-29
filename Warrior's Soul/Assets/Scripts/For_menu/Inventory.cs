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

public class Inventory: MonoBehaviour
{

    [SerializeField] private List<AssetItem> Items = new List<AssetItem>();
    [SerializeField] private Inventory_Cell _inventory_Cell_Template;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingparent;

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

    public void Add_Element_In_Cell(String name, Sprite sprite){

        bool need_New_Cell = true;
        for (int i = 0; i < _container.childCount; i++)
        {

            _inventory_Cell_Template = _container.GetChild(i).GetComponent<Inventory_Cell>();
            if (_inventory_Cell_Template._namefield.text == name)  
            {
                for(int j=0; j< Items.Count; j++)
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
            Create_New_Cell(name, sprite);
        }
    }

    public void Create_New_Cell(String name, Sprite sprite)
    {
        AssetItem _assetitem = new AssetItem();
        _assetitem._name = name;
        _assetitem._UIIcon = sprite;
        Items.Add(_assetitem); 
        var cell = Instantiate(_inventory_Cell_Template, _container);
        cell.Init(_draggingparent);
        cell.Render(Items[Items.Count - 1]);
        
    }

    


}