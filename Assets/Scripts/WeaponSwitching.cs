using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    void Start()
    {
        SelectWeapon(); // Ensure the correct weapon is selected at the start.
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i == selectedWeapon);
            i++;
        }
    }

    public void SetSelectedWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < transform.childCount)
        {
            selectedWeapon = weaponIndex;
            SelectWeapon();
        }
    }
}

