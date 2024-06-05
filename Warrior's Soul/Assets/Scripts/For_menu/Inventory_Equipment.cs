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
using UnityEditor.UI;
using TMPro;

public class Inventory_Equipment: MonoBehaviour
{

    //[SerializeField] private List<AssetItem> Weapon_Items = new List<AssetItem>();
    //[SerializeField] private List<AssetItem> Quick_Items = new List<AssetItem>();

    [SerializeField] private Inventory_Cell _inventory_Cell_Template;
    [SerializeField] private Transform _container_quick_cell;
    [SerializeField] private Transform _container_weapons_cell;
    [SerializeField] private Transform _draggingparent;

    Transform Quick_Buttons;

    public Sprite _standart_image;

    //bool _menu_active = false;
    //int max_count_in_quick_cell = 4;
   // int max_count_in_weapon_cell = 2;
    //int cur_count_weapon_cells;
   // int cur_count_quick_cells;

    private void Start()
    {
        //cur_count_weapon_cells = _container_weapons_cell.childCount;
        //cur_count_quick_cells = _container_quick_cell.childCount;
        Quick_Buttons = GameObject.Find("Quick_Access_Button").transform;
        Clear_Quick_Access_Button(Quick_Buttons);
    }

    private void Update()
    {
    }

    public void Update_Count_Item_In_Cell(int index, AssetItem item, Transform _container)
    {
        Inventory_Cell cell = _container.GetChild(index).GetComponent<Inventory_Cell>();
        cell._count_items.text = item.count_Element.ToString();
    }

    public void Clear_Inventory(Transform _container)
    {
        foreach (Transform child in _container)
        {
            print(child.name);
            Destroy(child.gameObject);
        }
    }

    /*public void add_items_in_list()     //  0- добавляет в Quick_Items , 1 - Weapon_items
    {
        foreach(Inventory_Cell cell in _container_quick_cell)
        {
            AssetItem _assetitem = new AssetItem();
            _assetitem._name = cell.name;
            _assetitem._UIIcon = cell._iconField;
            _assetitem.tag = cell.obj_tag;
            _assetitem.count_Element = cell._count_items;
            Quick_Items.Add(_assetitem);
        }

        foreach (Inventory_Cell cell in _container_weapons_cell)
        {
            AssetItem _assetitem = new AssetItem();
            _assetitem._name = cell.name;
            _assetitem._UIIcon = cell._iconField;
            _assetitem.tag = cell.obj_tag;
            _assetitem.count_Element = cell._count_items;
            Weapon_Items.Add(_assetitem);
        }

    }*/


    public void Convert_Quick_Cell_To_Button(Transform quick_buttons)
    {
        Clear_Quick_Access_Button(quick_buttons);

        for (int i = 0; i < _container_quick_cell.childCount; i++)
        {
            
            quick_buttons.GetChild(i).gameObject.SetActive(true);
            Swap_Buttons_Parametrs(_container_quick_cell.GetChild(i).gameObject, quick_buttons.GetChild(i).gameObject);
        }
    }

    private void Clear_Quick_Access_Button(Transform quick_buttons)
    {
        for(int i=0; i < 4; i++)
        {
            Button button = quick_buttons.GetChild(i).GetComponent<Button>();
            button.transform.GetChild(0).GetComponent<Text>().text = "";
            button.transform.GetChild(1).GetComponent<Text>().text = "";
            button.GetComponent<Image>().color = new Vector4(255,255,255,0);
            //quick_buttons.gameObject.SetActive(false);
        }
       
    }

    private void Swap_Buttons_Parametrs(GameObject inv_cell, GameObject quick_button)
    {
        quick_button.name = inv_cell.name;
        quick_button.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
        quick_button.GetComponent<Image>().sprite = inv_cell.GetComponent<Image>().sprite;
        quick_button.transform.GetChild(1).GetComponent<Text>().text = inv_cell.transform.GetChild(1).GetComponent<Text>().text;
        quick_button.transform.GetChild(0).GetComponent<Text>().text = inv_cell.transform.GetChild(0).GetComponent<Text>().text;
    }

}