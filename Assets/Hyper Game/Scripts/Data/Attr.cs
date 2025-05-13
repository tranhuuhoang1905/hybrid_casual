using UnityEngine;

[System.Serializable]
public class Attr
{
    public int damage;
    public int attack;
    public float attackSpeed;
    public int swordAttack;
    public float swordSpeed;
    public float moveSpeed;
    public int armor;
    public int health;

    // Constructor có tham số
    public Attr(
        int damage = 0,
        int attack = 0,
        int swordAttack = 0,
        float attackSpeed = 0f,
        float swordSpeed = 0f,
        float moveSpeed = 0f,
        int armor = 0,
        int health = 0
    )
    {
        this.damage = damage;
        this.attack = attack;
        this.swordAttack = swordAttack;
        this.attackSpeed = attackSpeed;
        this.swordSpeed = swordSpeed;
        this.moveSpeed = moveSpeed;
        this.armor = armor;
        this.health = health;
    }

    // Cộng chỉ số từ item hoặc buff
    public static Attr operator +(Attr a, Attr b)
    {
        return new Attr(
            a.damage + b.damage,
            a.attack + b.attack,
            a.swordAttack + b.swordAttack,
            a.attackSpeed + b.attackSpeed,
            a.swordSpeed + b.swordSpeed,
            a.moveSpeed + b.moveSpeed,
            a.armor + b.armor,
            a.health + b.health
        );
    }
    // Nhân Attr với một số (levelUp)
    public static Attr operator *(Attr a, int multiplier)
    {
        return new Attr(
            a.damage * multiplier,
            a.attack * multiplier,
            a.swordAttack * multiplier,
            a.attackSpeed * multiplier,
            a.swordSpeed * multiplier,
            a.moveSpeed * multiplier,
            a.armor * multiplier,
            a.health * multiplier
        );
    }
}