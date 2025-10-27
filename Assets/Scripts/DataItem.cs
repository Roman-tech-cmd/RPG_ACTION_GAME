using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item Data")]
public class DataItem : ScriptableObject
{
    public enum Rare { common, unusual, rare, epic, legendary };
    public Rare rareItem;
    public string ItemName;
    public string ItemDiscription;
}

