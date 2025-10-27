using UnityEngine;

[CreateAssetMenu(fileName = "NewGem", menuName = "Gems/Gem Data")]
public class GemsData : ScriptableObject
{
    public enum Rare { common, unusual, rare, epic, legendary };
    public Rare rareItem;
    public string gemName;
    public string gemDiscription;
    public int addHealth;
    public GameObject gemAtack;
}
