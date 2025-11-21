using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArmorDescriptionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private Image rareLine;
    [SerializeField] private TextMeshProUGUI rareTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    [SerializeField] private TextMeshProUGUI protectionTxt;
    [SerializeField] private TextMeshProUGUI manaTxt;
    [SerializeField] private TextMeshProUGUI hpTxt;
    [SerializeField] private TextMeshProUGUI gemDmgTxt;
    [SerializeField] private TextMeshProUGUI caneDmgTxt;
    [SerializeField] private Button activateButton;
    [SerializeField] private GameObject hpTablo;
    [SerializeField] private GameObject manaTablo;
    [SerializeField] private GameObject caneDmgTablo;
    [SerializeField] private GameObject gemDmgTablo;

    public void Show(BaseItem item)
    {
        gameObject.SetActive(true);
        activateButton.gameObject.SetActive(true);

        nameTxt.text = item.Name;
        descriptionTxt.text = item.Description;

        rareTxt.text = item.RareItem.ToString();
        rareLine.color = item.RareColor;
        rareTxt.color = item.RareColor;

        protectionTxt.text = item.IsActive ? item.AdditionalProtection.ToString("F1") : "???";

        if (item.AdditionalGemDMG == 0) gemDmgTablo.SetActive(false);
        else { gemDmgTxt.text = "+" + item.AdditionalGemDMG.ToString("F1"); gemDmgTablo.SetActive(true); }

        if (item.AdditionalMana == 0) manaTablo.SetActive(false);
        else { manaTxt.text = "+" + item.AdditionalMana.ToString("F1"); manaTablo.SetActive(true); }

        if (item.AdditionalCaneDMG == 0) caneDmgTablo.SetActive(false);
        else { caneDmgTxt.text = "+" + item.AdditionalCaneDMG.ToString("F1"); caneDmgTablo.SetActive(true); }

        if (item.AdditionalHp == 0) hpTablo.SetActive(false);
        else { hpTxt.text = "+" + item.AdditionalHp.ToString("F1"); hpTablo.SetActive(true); }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
