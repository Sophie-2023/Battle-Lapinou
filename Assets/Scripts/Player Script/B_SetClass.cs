using System;
using UnityEngine;

public class B_SetClass : MonoBehaviour
{
    [SerializeField] private B_UnitClass[] ClassList;
    [SerializeField] private UnitClass unitClass;
    private Entity entity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entity = GetComponent<BasicEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        SetStats();
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
        entity.SetSpeed(ActualClass.speed);
        entity.SetDef(ActualClass.def);
        entity.SetAttack(ActualClass.attack);
        entity.SetAttackSpeed(ActualClass.attackSpeed);
        entity.SetMaxMana(ActualClass.maxMana);
        entity.SetCost(ActualClass.cost);
        entity.SetRange(ActualClass.range);
    }

    public B_UnitClass GetClass()
    {
        B_UnitClass ActualClass = ClassList[0];
        foreach (B_UnitClass Class in ClassList)
        {
            if (Class.unitClass == unitClass)
            {
                ActualClass = Class;
            }
        }
        return ActualClass;
    }
}
