using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BasicEntity))]
public class B_Movement : MonoBehaviour
{
    private Vector3 positionGoal; //Position de l'objectif de l'unité en fonction de son comportement (offense->roi adverse, neutral->unité ennemi la plus proche, defense->son propre roi)
    private Entity entitySelf;
    private float spd;
    NavMeshAgent agent;
    [SerializeField] private Vector3 defaultGoal = new Vector3(0,0,0);
    [SerializeField] private float distChange = 1f; //Distance de changement, si la cible de l'unité bouge d'au moins cette distance par rapport à sa position initiale, on met à jour le pathfinding

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entitySelf = GetComponent<BasicEntity>();
        spd = entitySelf.GetSpeed();
        agent = GetComponent<NavMeshAgent>();

        agent.speed = spd;
        UpdatePositionGoal();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositionGoal();
        float distPositionGoalTarget = (agent.destination - positionGoal).magnitude;
        if (distPositionGoalTarget >= distChange)
        {
            if (agent.hasPath)
            {
                agent.ResetPath();
            }
            agent.SetDestination(positionGoal);
        }
    }

    private void UpdatePositionGoal()
    {
        var entityObjectsList = FindObjectsOfType(typeof(BasicEntity));
        if (entitySelf.GetBehavior()==Entity.Behavior.Offense)
        {
            foreach (var entity in entityObjectsList)
            {
                var entityOther = entity.GetComponent<BasicEntity>();
                if (entityOther.GetIsKing())
                {
                    if (entitySelf.GetIsEnemy()!=entityOther.GetIsEnemy())
                    {
                        var enemyKing = entity;
                        positionGoal = enemyKing.GetComponent<Transform>().position;
                        break;
                    }
                }
            }
        } else if (entitySelf.GetBehavior() == Entity.Behavior.Neutral)
        {
            var closestEnemy = entityObjectsList[0];
            float minDistance = Mathf.Infinity;
            foreach (var entity in entityObjectsList)
            {
                var entityOther = entity.GetComponent<BasicEntity>();
                if (entitySelf.GetIsEnemy() != entityOther.GetIsEnemy())
                {
                    var enemy = entity;
                    float dist = (enemy.GetComponent<Transform>().position - transform.position).magnitude;
                    if ((dist>0)&&(dist < minDistance))
                    {
                        closestEnemy = enemy;
                        minDistance = dist;
                    }
                }
            }
            positionGoal = closestEnemy.GetComponent<Transform>().position;
        } else if (entitySelf.GetBehavior() == Entity.Behavior.Defense)
        {
            foreach (var entity in entityObjectsList)
            {
                var entityOther = entity.GetComponent<BasicEntity>();
                if (entityOther.GetIsKing())
                {
                    if (entitySelf.GetIsEnemy() == entityOther.GetIsEnemy())
                    {
                        var myKing = entity;
                        positionGoal = myKing.GetComponent<Transform>().position;
                        break;
                    }
                }
            }

        } else
        {
            positionGoal = defaultGoal;
        }
    }
}
