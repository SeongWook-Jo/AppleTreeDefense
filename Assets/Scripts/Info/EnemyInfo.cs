public class EnemyInfo
{
    public int Id { get; private set; }
    public float Health { get; private set; }
    public float Speed { get; private set; }
    public AttackType AttackType { get; private set; }
    public float AttackTime { get; private set; }
    public float Damage { get; private set; }
    public int DropGold { get; private set; }

    public EnemyInfo(int id, float health, float speed, AttackType attackType, float attackTime, float damage, int dropGold)
    {
        Id = id;
        Health = health;
        Speed = speed;
        AttackType = attackType;
        AttackTime = attackTime;
        Damage = damage;
        DropGold = dropGold;
    }
}