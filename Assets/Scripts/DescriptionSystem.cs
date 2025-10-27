using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DescriptionSystem : MonoBehaviour
{
    public enum Rare { common, unusual, rare, epic, legendary }
    public enum typeItem { Weapons, Armor }
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private GameObject coverImage;
    [SerializeField] private GameObject weaponsBox;
    [SerializeField] private GameObject armorBox;

    [Header("Характеристики оружия")]
    [SerializeField] private TextMeshProUGUI weaponNameTXT;
    private string weaponNameItem;
    private Color weaponColorRare;
    [SerializeField] private Image weaponRareLine;
    private Rare weaponRare;
    private string weaponRareText;
    [SerializeField] private TextMeshProUGUI weaponRareTXT;
    [SerializeField] private TextMeshProUGUI weaponDamageTXT;
    private float weaponDamageItem;
    private Sprite weaponIconAttack;
    [SerializeField] private Image weaponIconAttackUI;
    private string weaponNameAttack;
    [SerializeField] private TextMeshProUGUI weaponNameAttackTXT;
    private string weaponDescriptionItem;
    [SerializeField] private TextMeshProUGUI weaponDescripTXT;
    private string weaponAttackDescription;
    [SerializeField] private TextMeshProUGUI weaponAttackDescriptionTXT;
    [SerializeField] private Button weaponButtonActivate;
    private string weaponElemetItem;
    private StaticElementClass.Element weaponElement;
    [SerializeField] private TextMeshProUGUI weaponElemetTXT;
    [SerializeField] private Image weaponElementImage;
    [SerializeField] private Sprite[] weaponElementSprites;



    [Header("Характеристики брони")]
    [SerializeField] private TextMeshProUGUI armorNameTXT;
    private string armorNameItem;
    private Color armorColorRare;
    [SerializeField] private Image armorRareLine;
    private Rare armorRare;
    private string armorRareText;
    [SerializeField] private TextMeshProUGUI armorRareTXT;
    private string armorDescriptionItem;
    [SerializeField] private TextMeshProUGUI armorDescripTXT;
    [SerializeField] private TextMeshProUGUI additionalArmorTXT;
    [SerializeField] private TextMeshProUGUI additionalManaTXT;
    [SerializeField] private TextMeshProUGUI additionalHpTXT;
    [SerializeField] private TextMeshProUGUI additionalGemDMGTXT;
    [SerializeField] private TextMeshProUGUI additionalCaneDMGTXT;
    [SerializeField] private Button armorButtonActivate;
    private float additionalArmor;
    private float additionalMana;
    private float additionalHp;
    private float additionalGemDMG;
    private float additionalCaneDMG;
    [SerializeField] private GameObject HpTablo;
    [SerializeField] private GameObject ManaTablo;
    [SerializeField] private GameObject DMGCaneTablo;
    [SerializeField] private GameObject DMGGemTablo;
    public void Update()
    {
        UpdateInfo();

    }

    public void UpdateInfo()
    {
        if (inventoryManager.SelectedItem != null)
        {
            if (inventoryManager.SelectedItem.GetComponent<BaseItem>().TypeItem == (BaseItem.typeItem)typeItem.Weapons) TakeWeaponsInfo();
            else TakeArmorInfo();
        }
        else
        {
            ClearInfo();
            coverImage.SetActive(true);
        }

        weaponRareText = weaponRare switch
        {
            Rare.common => "Обычный",
            Rare.unusual => "Необычный",
            Rare.rare => "Редкий",
            Rare.epic => "Эпический",
            Rare.legendary => "Легенадрный",
            _ => ""
        };
        armorRareText = armorRare switch
        {
            Rare.common => "Обычный",
            Rare.unusual => "Необычный",
            Rare.rare => "Редкий",
            Rare.epic => "Эпический",
            Rare.legendary => "Легенадрный",
            _ => ""
        };



    }

    public void ClearInfo()
    {
        coverImage.SetActive(true);
        weaponNameTXT.text = "";
        weaponDescripTXT.text = "";
        weaponDamageTXT.text = "";
        weaponElemetTXT.text = "";
        weaponButtonActivate.gameObject.SetActive(false);
    }

    public void TakeWeaponsInfo()
    {
        weaponsBox.SetActive(true);
        armorBox.SetActive(false);
        BaseItem item = inventoryManager.SelectedItem.GetComponent<BaseItem>();

        weaponButtonActivate.gameObject.SetActive(true);
        coverImage.SetActive(false);

        weaponNameItem = item.Name;
        weaponNameTXT.text = weaponNameItem;

        weaponDescriptionItem = item.Description;
        weaponDescripTXT.text = weaponDescriptionItem;

        if (item.IsActive == false) weaponDamageTXT.text = "???";
        else
        {
            weaponDamageItem = item.Damage;
            weaponDamageTXT.text = weaponDamageItem.ToString("F1");
        }

        weaponElemetItem = item.ElementText;
        weaponElemetTXT.text = weaponElemetItem;

        weaponElement = item.Element;

        weaponRare = (Rare)item.RareItem;
        weaponRareTXT.text = weaponRareText;

        weaponColorRare = item.RareColor;
        weaponRareLine.color = weaponColorRare;
        weaponRareTXT.color = weaponColorRare;

        weaponIconAttack = item.Icon;
        weaponIconAttackUI.sprite = weaponIconAttack;

        if (item.AttackItem != null)
        {

            weaponNameAttack = item.AttackItem.GetComponent<BaseAttack>().GetNameAttack();
            weaponNameAttackTXT.text = weaponNameAttack;

            weaponAttackDescription = item.AttackItem.GetComponent<BaseAttack>().GetDescriptionAttack();
            weaponAttackDescriptionTXT.text = weaponAttackDescription;
        }
        else
        {
            weaponNameAttackTXT.text = "";
            weaponAttackDescriptionTXT.text = "";
        }


        weaponElementImage.sprite = weaponElement switch
        {
            StaticElementClass.Element.Fire => weaponElementSprites[0],
            StaticElementClass.Element.Frost => weaponElementSprites[1],
            StaticElementClass.Element.Wind => weaponElementSprites[2],
            StaticElementClass.Element.Earth => weaponElementSprites[3],
            StaticElementClass.Element.Water => weaponElementSprites[4],
            _ => null
        };
    }
    public void TakeArmorInfo()
    {
        weaponsBox.SetActive(false);
        armorBox.SetActive(true);

        BaseItem item = inventoryManager.SelectedItem.GetComponent<BaseItem>();

        armorButtonActivate.gameObject.SetActive(true);

        armorNameTXT.text = armorNameItem;
        armorNameItem = item.Name;

        armorDescriptionItem = item.Description;
        armorDescripTXT.text = armorDescriptionItem;

        armorRare = (Rare)item.RareItem;
        armorRareTXT.text = armorRareText;

        armorColorRare = item.RareColor;
        armorRareLine.color = armorColorRare;
        armorRareTXT.color = armorColorRare;

        additionalArmor = item.AdditionalProtection;
        if (item.IsActive == false) additionalArmorTXT.text = "???";
        else additionalArmorTXT.text = additionalArmor.ToString("F1");

        additionalGemDMG = item.AdditionalGemDMG;
        if (item.IsActive == false) additionalArmorTXT.text = "???";
        if (additionalGemDMG == 0) DMGGemTablo.SetActive(false);
        else
        {
            additionalGemDMGTXT.text = "+" + additionalGemDMG.ToString("F1");
            DMGGemTablo.SetActive(true);
        }

        additionalMana = item.AdditionalMana;
        if (item.IsActive == false) additionalManaTXT.text = "???";
        if (additionalMana == 0) ManaTablo.SetActive(false);
        else
        {
            additionalManaTXT.text = "+" + additionalMana.ToString("F1");
            ManaTablo.SetActive(true);
        }

        additionalCaneDMG = item.AdditionalCaneDMG;
        if (item.IsActive == false) additionalCaneDMGTXT.text = "???";
        if (additionalCaneDMG == 0) DMGCaneTablo.SetActive(false);
        else
        {
            additionalCaneDMGTXT.text = "+" + additionalCaneDMG.ToString("F1");
            DMGCaneTablo.SetActive(true);
        }

        additionalHp = item.AdditionalHp;
        if (item.IsActive == false) additionalHpTXT.text = "???";
        if (additionalHp == 0) HpTablo.SetActive(false);
        else
        {
            additionalHpTXT.text = "+" + additionalHp.ToString("F1");
            HpTablo.SetActive(true);
        }

        coverImage.SetActive(false);
    }
}
