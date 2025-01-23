using System;
using UnityEngine;

public class B_SetClass : MonoBehaviour
{
    [SerializeField] private B_UnitClass[] ClassList;
    [SerializeField] private UnitClass unitClass;
    [SerializeField] private GameObject projectile;
    private Entity entity;
    private SpecialAttack specialAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entity = GetComponent<BasicEntity>();
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetStats()
    {
        B_UnitClass ActualClass = ClassList[0];
        foreach(B_UnitClass Class in ClassList) 
        { 
            if (Class.unitClass == unitClass)
            {
                ActualClass = Class;
            }
        }
        entity.SetMaxHP(ActualClass.maxHP);
        entity.SetHP(ActualClass.maxHP);
        entity.SetSpeed(ActualClass.speed);
        entity.SetDef(ActualClass.def);
        entity.SetAttack(ActualClass.attack);
        entity.SetAttackSpeed(ActualClass.attackSpeed);
        entity.SetMaxMana(ActualClass.maxMana);
        entity.SetMana(0);
        entity.SetCost(ActualClass.cost);
        entity.SetRange(ActualClass.range);
        SetSpecialAttack();
    }

    private void SetSpecialAttack()
    {
        switch (unitClass)
        {
            case UnitClass.Mage:
                specialAttack = gameObject.AddComponent(typeof(B_Explosion)) as B_Explosion;
                break;
            case UnitClass.Ninja:
                specialAttack = gameObject.AddComponent(typeof(B_SpeedBoost)) as B_SpeedBoost;
                break;
            case UnitClass.Guerrier:
                specialAttack = gameObject.AddComponent(typeof(B_Berserk)) as B_Berserk;
                break;
            case UnitClass.Healer:
                specialAttack = gameObject.AddComponent(typeof(B_HealAllies)) as B_HealAllies;
                break;
            case UnitClass.Tank:
                specialAttack = gameObject.AddComponent(typeof(B_Invulnerability)) as B_Invulnerability;
                break;
            case UnitClass.Archer:
                specialAttack = gameObject.AddComponent(typeof(B_ShootProjectile)) as B_ShootProjectile;
                if (specialAttack is B_ShootProjectile specialAttackProjectile)
                {
                    specialAttackProjectile.setProjectile(projectile);
                }
                break;
            case UnitClass.Alcoolique:
                specialAttack = gameObject.AddComponent(typeof(B_DrinkMana)) as B_DrinkMana;
                break;
            case UnitClass.Bob:
                specialAttack = gameObject.AddComponent(typeof(B_Makarena)) as B_Makarena;
                break;
        }
    }

    public SpecialAttack GetSpecialAttack()
    {
        return specialAttack;
    }

    public UnitClass GetClass()
    {
        return unitClass;
    }
}
