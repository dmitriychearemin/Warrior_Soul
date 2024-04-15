using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(menuName = "Item")]
public class AssetItem: ScriptableObject
{
    public string Name => _name;
    public Sprite UIICON => _UIIcon;

    public string Tag => tag;
    // string IItem.Name { get => _name; set => _name}
    // Sprite IItem.UIICON { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public int count_Element = 1;

    public string _name;
    public string tag;
    public Sprite _UIIcon;


}

