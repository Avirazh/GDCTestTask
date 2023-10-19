using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] Transform weaponHolderTransform;

    private Weapon _weapon;

    private Quaternion _weaponOriginalRotation;
    public void SetDependencies(Weapon weapon, LayerMask layerMask)
    {
        _weapon = weapon;
        _weapon.LayerMask = layerMask;

        _weapon.transform.position = weaponHolderTransform.position;
        _weaponOriginalRotation = _weapon.transform.rotation;
    }

    public void Shoot(Transform target)
    {
         _weapon.Shoot(target);       
    }

    public void ChangeWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }
    public void ResetWeaponRotation()
    {
        _weapon.transform.rotation = _weaponOriginalRotation;
    }
}
