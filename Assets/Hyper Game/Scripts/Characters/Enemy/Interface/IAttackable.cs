public interface IAttackable
{
    void Attack();
    void StopAttack();
    bool IsAttacking { get; }
}