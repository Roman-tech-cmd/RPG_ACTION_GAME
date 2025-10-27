using UnityEngine;

[CreateAssetMenu(fileName = "NewCane", menuName = "Canes/Cane Data")]
public class CanesData : ScriptableObject
{
    public enum Rare { common, unusual, rare, epic, legendary };
    public Rare rareItem;
    public string caneName;
    public string caneDiscription;
    public int addHealth;
    public GameObject caneAtack;
}
