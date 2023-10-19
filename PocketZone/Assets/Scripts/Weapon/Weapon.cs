using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform _shootPosition;

    [SerializeField] private WeaponConfig _weaponConfig;
    [SerializeField] private GameObject _bullet;

    private int _bulletsLeft;

    public LayerMask LayerMask;

    private void Awake()
    {
        Reload();
    }
    public void Shoot(Transform target)
    {
        if (_bulletsLeft <= 0)
        {
            Reload();
        }
        else
        {
            StartCoroutine(ShootWithCooldownCoroutine(_weaponConfig.BulletsPerShotCooldown, target));           
        }
        
    }
    public void Reload()
    {
        StartCoroutine(ReloadCoroutine(_weaponConfig.ReloadTime));
    }

    private IEnumerator ReloadCoroutine(int reloadTime)
    { 
        Debug.Log("RELOADING");
        
        _bulletsLeft = _weaponConfig.MagazineSize;

        yield return new WaitForSeconds(reloadTime);
    }
    
    private IEnumerator ShootWithCooldownCoroutine(float cooldown, Transform target)
    {
        for (int i = 0; i < _weaponConfig.BulletsPerShot; i++)
        {
            Vector2Extentions.RotateObjectToTarget(transform, target);
            CreateBullet(target);

            yield return new WaitForSeconds(cooldown);
        }
    }

    private void CreateBullet(Transform target)
    {
        var bulletPrefab = Instantiate(_bullet, _shootPosition.position, Quaternion.identity);
        var bullet = bulletPrefab.GetComponent<Bullet>();

        bullet.SetDependencies(target, _weaponConfig.BulletSpeed, _weaponConfig.DamagePerBullet, LayerMask);

        _bulletsLeft--;

        Debug.Log($"bullets left: {_bulletsLeft}");
    }
}
