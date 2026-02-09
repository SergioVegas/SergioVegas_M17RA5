using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    public Transform hand;
    public Transform back;
    public GameObject weaponPrefab;
    
    private GameObject currentWeapon;
    private bool isEquipped = false;
    public bool IsEquipped => isEquipped;
    public bool HasWeapon => currentWeapon != null;

    public void ToggleEquip()
    {
        if (isEquipped) Unequip();
        else Equip();
    }

    public void SetEquipped(bool state)
    {
        if (state) Equip();
        else Unequip();
    }

    public void Equip()
    {
        if (hand != null && currentWeapon != null)
        {
            currentWeapon.transform.SetParent(hand);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
            isEquipped = true;
        }
    }
    public void Unequip()
    {
        if (back != null && currentWeapon != null)
        {
            currentWeapon.transform.SetParent(back);
            //Rotation needed to look good on the back
            currentWeapon.transform.localPosition = new  Vector3(0,-0.19f,-0.09f); 
            currentWeapon.transform.localRotation = Quaternion.Euler(-2.42f,5.08f, 29.74f);
            isEquipped = false;
        }
    }
    public void CreateWeapon()
    {
        if (weaponPrefab != null && currentWeapon == null)
        {
            currentWeapon = Instantiate(weaponPrefab);
            Unequip();
        }
    }
    private void OnEnable() { ObtainWeapon.InstateWeapon += CreateWeapon; }
    private void OnDisable() { ObtainWeapon.InstateWeapon -= CreateWeapon; }
}