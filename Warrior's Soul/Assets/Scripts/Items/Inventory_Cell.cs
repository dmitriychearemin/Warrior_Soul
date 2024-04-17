
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
    [SerializeField] GameObject _input_field;
    String obj_tag;
    private Transform _dragingParrent;
    private Transform inventory_container;
    private Transform _weapon_container_Parrent;
    private Transform _quick_container_Parrent;
    private Transform _originalparent;
    private OnContainer onContainer = OnContainer.Inventory;
    bool _doubleclick = false;
    float timer_before_second_click=0;
    int count_separate_elements = 0;

    enum OnContainer
    {
        Inventory,
        Quick_Access,
        Weapon
    }

    private void Update()
    {
        if (timer_before_second_click > 0)
        {
            timer_before_second_click += 22 *Time.deltaTime;
            if(timer_before_second_click > 20)
            {
                timer_before_second_click = 0;
                _doubleclick = false;
            }
        }
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

    public Text getName()
    {
        return _namefield;
    }
    public void Click()
    {
        if (timer_before_second_click <= 20 & timer_before_second_click > 0)
        {
            _doubleclick = true;
            Instantiate_Input_field();
        }
        timer_before_second_click++;
        
    }

    void Instantiate_Input_field()
    {
        var field = Instantiate(_input_field, transform);
        field.transform.parent = _dragingParrent;
        For_Input_Fields script_field = field.GetComponent<For_Input_Fields>();
        script_field.Set_Cell_For_Separate(gameObject);
    }

    public void Get_Count_Separate_Item(int count)
    {
        count_separate_elements = count;
        
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

