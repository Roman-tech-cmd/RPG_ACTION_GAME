using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item Data")]
public class DataItem : ScriptableObject
{
    public StaticItemCharacteristicClass.Rare rareItem;
    public string ItemName;
    public string ItemDiscription;
}

