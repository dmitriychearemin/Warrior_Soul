
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
   
  


    private Transform _dragingParrent;
    private Transform _originalparent;

    public void Init(Transform draggingparent)
    {
        _dragingParrent = draggingparent;
        _originalparent = transform.parent;
        
    }

    public void Render(AssetItem item)
    {
      
        _namefield.text = item.Name;
        _iconField.sprite = item.UIICON;
        _count_items.text = item.count_Element.ToString();

    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = _dragingParrent;
        
    }




    public void OnDrag(PointerEventData eventData)
    {
       
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousepos.x, mousepos.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        int closestindex = 0;

        for(int i =0; i< _originalparent.transform.childCount; i++)
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
}

