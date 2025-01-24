using UnityEngine;

public class B_FireZone : B_InteractibleZone
{
    [SerializeField] private int degat;

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

    // inflige des dégâts aux entités (alliées ou ennemies) de la zone
    public override void StartZoneAction()
    {
        foreach (BasicEntity entity in entitiesInZone)
        {
            entity.SetHP(entity.GetHP() - degat);
            if (entity.GetHP() < 0)
            {
                entity.SetHP(0);
            }
        }
    }
}
