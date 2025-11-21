using UnityEngine;

[CreateAssetMenu(fileName = "NewGem", menuName = "Gems/Gem Data")]
public class GemsData : ScriptableObject
{
    public StaticItemCharacteristicClass.Rare rareItem;
    public string gemName;
    public string gemDiscription;
    public int addHealth;
    public GameObject gemAtack;
}
