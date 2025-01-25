using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class B_SpeedZone : B_InteractibleZone
{
    [SerializeField] private int speedAmount;

    [SerializeField] private float actionTimeLength;
    [SerializeField] bool isActivated = false;

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
        StartCoroutine(StartAction());
        foreach (BasicEntity entity in entitiesInZone)
        {
            if (!entity.GetIsEnemy())
            {
                entity.SetSpeed(entity.GetSpeed() + speedAmount);
            }
        }
    }

    private IEnumerator StartAction()
    {
        isActivated = true;
        yield return new WaitForSeconds(actionTimeLength);
        isActivated = false;
        StopZoneAction();
    }

    private void StopZoneAction()
    {
        foreach (BasicEntity entity in entitiesInZone)
        {
            if (!entity.GetIsEnemy())
            {
                entity.SetSpeed(entity.GetSpeed() - speedAmount);
            }
        }
        _particleSystem.Stop();
    }

    public override void StartTriggerEnterAction(BasicEntity entity)
    {
        base.StartTriggerEnterAction(entity);
        if (isActivated && !entity.GetIsEnemy()) 
        {
            entity.SetSpeed(entity.GetSpeed() + speedAmount);
        }
    }

    public override void StartTriggerExitAction(BasicEntity entity)
    {
        base.StartTriggerExitAction(entity);
        if (isActivated && !entity.GetIsEnemy())
        {
            entity.SetSpeed(entity.GetSpeed() - speedAmount);
        }
    }

}
