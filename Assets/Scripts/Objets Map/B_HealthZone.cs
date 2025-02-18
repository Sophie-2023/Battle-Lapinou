using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_HealthZone : B_InteractibleZone
{
    [SerializeField] private int healAmount;

    private void Start()
    {
        StartBehavior();
    }

    private void Update()
    {
        UpdateBehavior();
    }

    public override void StartBehavior()
    {
        base.StartBehavior();
    }

    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
    }

    // Gu�rit toutes les entit�s (alli�es ou ennemies) de la zone
    public override void StartZoneAction()
    {
        foreach (BasicEntity entity in entitiesInZone)
        {
            entity.SetHP(entity.GetHP() + healAmount);
            if (entity.GetHP()> entity.GetMaxHP())
            {
                entity.SetHP(entity.GetMaxHP());
            }
        }
    }
}
