using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item Data")]
public class DataItem : ScriptableObject
{
    public StaticItemCharacteristicClass.Rare RareItem;
    public StaticItemCharacteristicClass.typeItem ItemType;
    public StaticItemCharacteristicClass.CategoryItem CategoryItem;
    public string ItemName;
    public string ItemDiscription;
    
}

