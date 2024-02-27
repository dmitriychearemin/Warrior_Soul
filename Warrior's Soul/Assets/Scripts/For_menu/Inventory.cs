using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Unity.VisualScripting.ReorderableList;
using Unity.VisualScripting;

/*public class Inventory : MonoBehaviour
{
  public Data_Base data;

    public List<Item_Inventory> items = new List<Item_Inventory> ();
    public GameObject Object_Show;
    public GameObject Main_Object;
    public int Max_Count;

    public Camera cam;
    public EventSystem eventSystem;

    public int currentID;
    public Item_Inventory currentItem;

    public RectTransform moveObject;
    public Vector3 offset;

    void Start()
    {
        if(items.Count == 0)
        {
            Add_Graphics();
        }

        for (int i =0; i< Max_Count; i++) // заполнение €чеек
        {
            AddItem(i, data.item[Random.Range(0, data.item.Count)], Random.Range(1, 99));
        }
        Update_Inventory();
    }


    public void Update()
    {
        if(currentID != -1)
        {
            MoveObject();
        }
    }

    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.identificator;
        items[id].count_elements = count;
        items[id].itemGameObject.GetComponent<Image>().sprite = item.image;

        if(count > 1 && item.identificator != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id, Item_Inventory item_Inventory)
    {
        items[id].id = item_Inventory.id;
        items[id].count_elements = item_Inventory.count_elements;
        items[id].itemGameObject.GetComponent<Image>().sprite = data.item[item_Inventory.id].image;

        if (item_Inventory.count_elements > 1 && item_Inventory.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = item_Inventory.count_elements.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void Add_Graphics()
    {
        for (int i = 0; i < Max_Count; i++)
        {
            GameObject newItem = Instantiate(Object_Show, Main_Object.transform) as GameObject;
            newItem.name = i.ToString();

            Item_Inventory ii = new Item_Inventory();
            ii.itemGameObject = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);
            Button tempButton = newItem.GetComponent<Button>();
            tempButton.onClick.AddListener(delegate { SelectObject(); });
            items.Add(ii);
        }

    }

    public void Update_Inventory()
    {
        for(int i=0; i< Max_Count; i++)
        {
            if (items[i].id != 0 && items[i].count_elements > 0)
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = items[i].count_elements.ToString();
            }
            else
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }
            items[i].itemGameObject.GetComponent<Image>().sprite = data.item[items[i].id].image;

        }
    }

    public void SelectObject()
    {
        if(currentID== -1)
        {
            currentID = int.Parse(eventSystem.currentSelectedGameObject.name);
            currentItem = Copy_Inventory(items[currentID]);
            moveObject.gameObject.SetActive(true);
            moveObject.GetComponent<Image>().sprite = data.item[currentItem.id].image;

            AddItem(currentID, data.item[0], 0);
        }

        else{
            AddInventoryItem(currentID, items[int.Parse(eventSystem.currentSelectedGameObject.name)]);
            AddInventoryItem(int.Parse(eventSystem.currentSelectedGameObject.name), currentItem);
            currentID = -1;
            moveObject.gameObject.SetActive(false);
        }
    }

    public Item_Inventory Copy_Inventory(Item_Inventory old)
    {
        Item_Inventory New = new Item_Inventory();

        New.id = old.id;
        New.count_elements = old.count_elements;
        New.itemGameObject = old.itemGameObject;
        return New;
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = Main_Object.GetComponent<RectTransform>().position.z;
        moveObject.position = cam.ScreenToWorldPoint(pos);
    }

}

[System.Serializable]

public class Item_Inventory
{

    public int id;
    public int count_elements;
    public GameObject itemGameObject;
}*/


public class Inventory: MonoBehaviour
{

    [SerializeField] private List<AssetItem> Items;
    [SerializeField] private Inventory_Cell _inventory_Cell_Template;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingparent;

    public void OnEnable()
    {
        Render(Items);
    }

    public void Render(List<AssetItem> Items)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }


        Items.ForEach(item =>
        {
            var cell = Instantiate(_inventory_Cell_Template, _container);
            cell.Init(_draggingparent);
            cell.Render(item);
        });
    }

}