using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public WeaponSwitching weaponSwitchingController;
    public WeaponSwitching weaponSwitchingHand;

    private WeaponSwitching activeWeaponSwitching;

    void Update()
    {
        DetermineActiveWeaponSwitching();

        // Example input to switch weapons on the active weapon holder
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (activeWeaponSwitching != null)
            {
                activeWeaponSwitching.SetSelectedWeapon(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (activeWeaponSwitching != null)
            {
                activeWeaponSwitching.SetSelectedWeapon(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (activeWeaponSwitching != null)
            {
                activeWeaponSwitching.SetSelectedWeapon(2);
            }
        }
    }

    void DetermineActiveWeaponSwitching()
    {
        // Check if the controller weapon holder is active, and assign it as the active weapon switching component
        if (weaponSwitchingController != null && weaponSwitchingController.gameObject.activeInHierarchy)
        {
            activeWeaponSwitching = weaponSwitchingController;
        }
        // Otherwise, check if the hand weapon holder is active
        else if (weaponSwitchingHand != null && weaponSwitchingHand.gameObject.activeInHierarchy)
        {
            activeWeaponSwitching = weaponSwitchingHand;
        }
        else
        {
            activeWeaponSwitching = null; // No active weapon holder found
        }
    }
}

