using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item Data")]
public class DataItem : ScriptableObject
{
    public StaticItemCharacteristicClass.Rare rareItem;
    public StaticItemCharacteristicClass.typeItem ItemType;
    public string ItemName;
    public string ItemDiscription;
    
}

