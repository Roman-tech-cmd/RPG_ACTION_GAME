using UnityEngine;

public class DescriptionSystem : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private GameObject coverImage;
    [SerializeField] private WeaponDescriptionUI weaponUI;
    [SerializeField] private ArmorDescriptionUI armorUI;

    void Update()
    {
        if (inventoryManager.SelectedItem == null)
        {
            ClearInfo();
            return;
        }

        BaseItem item = inventoryManager.SelectedItem.GetComponent<BaseItem>();
        if (item.TypeItem == StaticItemCharacteristicClass.typeItem.Weapons)
        {
            armorUI.Hide();
            weaponUI.Show(item);
        }
        else
        {
            weaponUI.Hide();
            armorUI.Show(item);
        }

        coverImage.SetActive(false);
    }

    public void ClearInfo()
    {
        coverImage.SetActive(true);
        weaponUI.Hide();
        armorUI.Hide();
    }
}
