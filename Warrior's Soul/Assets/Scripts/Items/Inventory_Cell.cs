
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_Cell: MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] public Text _namefield;
    [SerializeField] private Image _iconField;
    [SerializeField] public Text _count_items;
    String obj_tag;
    private Transform _dragingParrent;
    private Transform inventory_container;
    private Transform _weapon_container_Parrent;
    private Transform _quick_container_Parrent;
    private Transform _originalparent;
    private OnContainer onContainer = OnContainer.Inventory;

    enum OnContainer
    {
        Inventory,
        Quick_Access,
        Weapon
    }

    public void Init(Transform draggingparent, Transform weapon_container, Transform quick_container)
    {
        _dragingParrent = draggingparent;
        _originalparent = transform.parent;
        inventory_container = _originalparent;
        _quick_container_Parrent = quick_container;
        _weapon_container_Parrent = weapon_container;
    }

    public void Render(AssetItem item)
    {
      
        _namefield.text = item.Name;
        _iconField.sprite = item.UIICON;
        _count_items.text = item.count_Element.ToString();
        obj_tag = item.Tag;

    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = _dragingParrent;
        
    }

    public Text getName()
    {
        return _namefield;
    }


    public void OnDrag(PointerEventData eventData)
    {
       
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousepos.x, mousepos.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        int closestindex = 0;

        switch (onContainer) {
            case OnContainer.Inventory:
                _originalparent = inventory_container;
                break;

            case OnContainer.Quick_Access:
                _originalparent = _quick_container_Parrent;
                break;

            case OnContainer.Weapon:
                _originalparent = _weapon_container_Parrent;
                break;

            default:
                print("Данный контейнер отсутствует");
                break;
        }

        for (int i =0; i< _originalparent.transform.childCount; i++)
        {
         

            if(Vector3.Distance(transform.position,_originalparent.transform.GetChild(i).transform.position) < 
                Vector3.Distance(transform.position, _originalparent.transform.GetChild(closestindex).transform.position))
            {
                closestindex = i;
            }
        }

        transform.parent = _originalparent;
        transform.SetSiblingIndex(closestindex);
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "For_Weapons")
        {
            if (obj_tag == "Weapon")
            {
                onContainer = OnContainer.Weapon;
            }
        }
        else if (collision.name == "For_quick_access_cells")
        {
            if (obj_tag == "Quick_access_items")
            {
                onContainer = OnContainer.Quick_Access;
            }
        }

        else
        {
            onContainer = OnContainer.Inventory;
        }
    }

}

