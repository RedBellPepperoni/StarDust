using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Weapon,
    Relic
    

}

public enum Attributes
{
    Agility,
    Intellect,
    Stamina,
    Strength
}
public abstract class ItemObject : ScriptableObject
{
    public int Id;
  //  public Sprite uiDisplay;
    public ItemType type;
    public GameObject prefab;

    [TextArea (15, 20)]
    public string description;
    public ItemBuff[] buffs;

    public Item CreateItem () {
        Item newItem = new Item (this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    
    public Item (ItemObject item) {
        Name = item.name;
        Id = item.Id;
       
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff (int _min, int _max) {
        min = _min;
        max = _max;
        GenerateValue ();
    }
    public void GenerateValue () {
        value = UnityEngine.Random.Range (min, max);
    }
}

