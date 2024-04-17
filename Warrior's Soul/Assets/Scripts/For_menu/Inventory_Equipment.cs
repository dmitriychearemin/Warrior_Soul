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

public class Inventory_Equipment: MonoBehaviour
{

    [SerializeField] private List<AssetItem> Weapon_Items = new List<AssetItem>();
    [SerializeField] private List<AssetItem> Quick_Items = new List<AssetItem>();

    [SerializeField] private Inventory_Cell _inventory_Cell_Template;
    [SerializeField] private GameObject default_quick_cell;
    [SerializeField] private GameObject default_Weapon_cell;
    [SerializeField] private Transform _container_quick_cell;
    [SerializeField] private Transform _container_weapons_cell;
    [SerializeField] private Transform _draggingparent;

    bool _menu_active = false;
    int max_count_in_quick_cell = 4;
    int max_count_in_weapon_cell = 2;

    int cur_count_weapon_cells;
    int cur_count_quick_cells;
    private void Start()
    {
        cur_count_weapon_cells = _container_weapons_cell.childCount;
        cur_count_quick_cells = _container_quick_cell.childCount;
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

    

}