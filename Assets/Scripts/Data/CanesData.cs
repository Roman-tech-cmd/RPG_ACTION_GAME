using UnityEngine;

[CreateAssetMenu(fileName = "NewCane", menuName = "Canes/Cane Data")]
public class CanesData : ScriptableObject
{
    public StaticItemCharacteristicClass.Rare rareItem;
    public string caneName;
    public string caneDiscription;
    public int addHealth;
    public GameObject caneAtack;
}
