using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(menuName = "Item")]
public class AssetItem: ScriptableObject, IItem
{
    public string Name => _name;
    public Sprite UIICON => _UIIcon;


    [SerializeField] private string _name;
    [SerializeField] private Sprite _UIIcon;


}

