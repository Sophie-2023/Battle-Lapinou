using UnityEngine;

public class B_ManaZone : B_InteractibleZone
{
    [SerializeField] private int manaAmount;

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

    // Ajoute de la mana aux entités (alliées ou ennemies) de la zone
    public override void StartZoneAction()
    {
        foreach (BasicEntity entity in entitiesInZone)
        {
            entity.SetMana(entity.GetMana() + manaAmount);
            if (entity.GetMana() > entity.GetMaxMana())
            {
                entity.SetMana(entity.GetMaxMana());
            }
        }
    }
}
