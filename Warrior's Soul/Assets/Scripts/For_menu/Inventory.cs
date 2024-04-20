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
    [SerializeField] private GameObject _AssetItem;
    [SerializeField] private Transform player;
    bool _menu_active = false;

    private void Start()
    {

    }

    private void Update()
    {
    }



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
            AssetItem item = Search_Item_In_List(Items, inv_cell);
            int new_count_items = int.Parse(inv_cell._count_items.text) - Count_items;
            item.count_Element = new_count_items;
            inv_cell._count_items.text = new_count_items.ToString();
            Create_New_Cell(item._name, item._UIIcon, item.tag, Count_items);               
        }

    }

    private AssetItem Search_Item_In_List(List<AssetItem> item, Inventory_Cell inv_cell)
    {
        foreach (AssetItem itm in item.ToList())
        {
            if (itm._name == inv_cell._namefield.text && itm.count_Element == int.Parse(inv_cell._count_items.text))
            {
               
                return itm;
            }
        }

        return null;

    }

    public void Merging_Items(Inventory_Cell inv_cell, GameObject cell, GameObject merg_cell)
    {
        if (cell != null & merg_cell != null)
        {
            Inventory_Cell inv_merg_cell = merg_cell.GetComponent<Inventory_Cell>();
            var item = Search_Item_In_List(Items, inv_merg_cell);
            int new_count_items = int.Parse(inv_cell._count_items.text) + int.Parse(inv_merg_cell._count_items.text);
            item.count_Element = new_count_items;
            inv_merg_cell._count_items.text = new_count_items.ToString();


            Items.Remove(Search_Item_In_List(Items, inv_cell));
            if (inv_cell.cur_field)
            {
                Destroy(inv_cell.cur_field);
            }
            Destroy(cell);         
        }
    }

    public void Remove_Item_In_List(Inventory_Cell cell, GameObject cell_in_container, int opredelitel) {

        AssetItem item = Search_Item_In_List(Items, cell);

        Items.Remove(item);

        switch (opredelitel){

            case 0:  // Для дропа предмета
                Drop_Item(cell);
                break;

            case 1:  // Для полного удаления  

                break;

            default:
                break;
        }
        Destroy(cell_in_container);

    }

    void Drop_Item(Inventory_Cell cell)
    {
        _AssetItem.name = cell._namefield.text;
        _AssetItem.tag = cell.obj_tag;
        SpriteRenderer spriteRenderer = _AssetItem.GetComponent<SpriteRenderer>();
        _AssetItem.GetComponent<SpriteRenderer>().sprite = cell._iconField.sprite;
        _AssetItem.GetComponent<Take_Item>()._name_Item = cell._namefield.text;
        Vector2 pos_for_drop = new Vector2(player.position.x, player.position.y-4);
        
        for(int i=0; i< int.Parse(cell._count_items.text); i++)
        {
            Instantiate(_AssetItem, pos_for_drop, transform.rotation);
        }

    }

    
    void Check_On_True_Destroying()  // Проверка, точно ли человек хочет удалить элемент безвозвратно
    {

    }



}