using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponDescriptionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private Image rareLine;
    [SerializeField] private TextMeshProUGUI rareTxt;
    [SerializeField] private TextMeshProUGUI damageTxt;
    [SerializeField] private Image iconAttackUI;
    [SerializeField] private TextMeshProUGUI attackNameTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    [SerializeField] private TextMeshProUGUI attackDescriptionTxt;
    [SerializeField] private Button activateButton;
    [SerializeField] private TextMeshProUGUI elementTxt;
    [SerializeField] private Image elementImage;
    [SerializeField] private ElementLibrary library;

    public void Show(BaseItem item)
    {
        gameObject.SetActive(true);
        activateButton.gameObject.SetActive(true);

        nameTxt.text = item.Name;
        descriptionTxt.text = item.Description;

        damageTxt.text = item.IsActive ? item.Damage.ToString("F1") : "???";

        elementTxt.text = item.ElementText;
        rareTxt.text = item.RareItem.ToString();
        rareLine.color = item.RareColor;
        rareTxt.color = item.RareColor;

        iconAttackUI.sprite = item.Icon;

        if (item.AttackItem != null)
        {
            var attack = item.AttackItem.GetComponent<BaseAttack>();
            attackNameTxt.text = attack.NameAttack;
            attackDescriptionTxt.text = attack.DescriptionAttack;
        }
        else
        {
            attackNameTxt.text = "";
            attackDescriptionTxt.text = "";
        }

        elementImage.sprite = item.Element switch
        {
            StaticItemCharacteristicClass.Element.Fire => library.fire,
            StaticItemCharacteristicClass.Element.Frost => library.frost,
            StaticItemCharacteristicClass.Element.Wind => library.wind,
            StaticItemCharacteristicClass.Element.Earth => library.earth,
            StaticItemCharacteristicClass.Element.Water => library.water,
            _ => null
        };
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
