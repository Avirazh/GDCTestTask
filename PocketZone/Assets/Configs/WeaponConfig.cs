using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfigData", menuName = "Configs/WeaponConfig", order = 51)]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] public float Cooldown;
    [SerializeField] public float DamagePerBullet;

    [field: SerializeField, Range(1, 1000)] public float BulletSpeed;
    [field: SerializeField, Range(1, 300)] public int MagazineSize;
    [field: SerializeField, Range(1, 5)] public int ReloadTime;
    [field: SerializeField, Range(1, 10)] public int BulletsPerShot;

    [SerializeField] public int BulletsPerShotCooldown;
}
