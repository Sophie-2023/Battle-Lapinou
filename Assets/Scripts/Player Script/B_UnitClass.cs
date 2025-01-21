using UnityEngine;

public enum UnitClass { Mage, Ninja, Guerrier, Healer, Tank, Archer, Alcoolique, Bob };

public interface SpecialAttack
{
    void Execute(Entity entityself);
}

[CreateAssetMenu(fileName = "B_UnitClass", menuName = "Scriptable Objects/B_UnitClass")]
public class B_UnitClass : ScriptableObject
{
    public UnitClass unitClass;
    public int maxHP;
    public int def;
    public float speed;
    public float attackSpeed;
    public int attack;
    public int maxMana;
    public int cost;
    public float range;
    public MonoBehaviour specialAttackScript;
}
