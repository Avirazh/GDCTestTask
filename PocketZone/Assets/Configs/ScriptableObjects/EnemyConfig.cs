using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfigData", menuName = "Configs/EnemyConfig", order = 51)]
public class EnemyConfig : ScriptableObject
{
    public GameObject Prefab;
    public float Damage;
    public EnemyDifficulty Difficulty;

    public float AggroRadius;
    public float Speed;

    public float AttackRange;
    public float AttackCooldown;
}
public enum EnemyDifficulty
{
    Easy,
    Medium,
    Hard
}
