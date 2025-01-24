using Unity.VisualScripting;
using UnityEngine;

public class B_SpeedZone : B_InteractibleZone
{
    [SerializeField] private int speedAmount;

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

    public override void StartZoneAction()
    {
        foreach (BasicEntity entity in entitiesInZone)
        {
            entity.SetSpeed(entity.GetSpeed() + speedAmount);
        }
    }

}
