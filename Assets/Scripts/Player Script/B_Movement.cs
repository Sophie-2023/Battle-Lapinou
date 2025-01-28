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
    [SerializeField] private float distCrit = 3; //C'est une distance qui créé un changement de comportement. Si la distance entre l'unité et son objectif est supérieur à distCrit, elle cherche à se rapprocher, sinon, elle attaque l'unité ennemie la plus proche 
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
        float distPositionGoalTarget = (agent.destination - positionGoal).magnitude; //distance entre la destination du navMesh agent et la véritable position de son objectif
        agent.speed = entitySelf.GetSpeed();
        if (distPositionGoalTarget >= distChange) //On ne change la destination que si l'unité visée s'en éloigne trop
        {
            if (agent.hasPath)
            {
                agent.ResetPath();
            }
            if (agent.enabled)
            {
                agent.SetDestination(positionGoal);
            }
        }
    }

    private void UpdatePositionGoal()
    {
        var entityObjectsList = FindObjectsOfType(typeof(BasicEntity));
        if (entitySelf.GetBehavior()==Entity.Behavior.Offense) //Si le comportement est offense, l'unité se rapproche du roi adverse, puis attaque l'unité ennemi la plus proche une fois qu'elle est suffisement proche du roi adverse
        {
            foreach (var entity in entityObjectsList)
            {
                var entityOther = entity.GetComponent<BasicEntity>();
                if (entityOther.GetIsKing())
                {
                    if (entitySelf.GetIsEnemy()!=entityOther.GetIsEnemy())
                    {
                        var enemyKing = entity;
                        float distBetweenSelfAndGoal = (transform.position - enemyKing.GetComponent<Transform>().position).magnitude;
                        if (distBetweenSelfAndGoal > distCrit) //Cas où on se rapproche du roi adverse
                        {
                            positionGoal = enemyKing.GetComponent<Transform>().position;
                        } else //Cas où on se rapproche de l'unité enemi la plus proche
                        {
                            Vector3 closestEnemyPosition = getClosestenemy().GetComponent<Transform>().position;
                            float distBetweenSelfAndClosestEnemy = (transform.position - closestEnemyPosition).magnitude;
                            if (distBetweenSelfAndClosestEnemy < distCrit)
                            {
                                positionGoal = closestEnemyPosition;
                                agent.updateRotation = false;
                                transform.LookAt(closestEnemyPosition);
                            } else
                            {
                                agent.updateRotation = true;
                                positionGoal = transform.position;
                            }
                        }
                        break;
                    }
                }
            }
        } else if (entitySelf.GetBehavior() == Entity.Behavior.Neutral) //Si le comportement est sur neutral, on cherche à se rapprocher de l'unité adverse la plus proche
        {
            positionGoal = getClosestenemy().GetComponent<Transform>().position;
            float distBetweenSelfAndClosestEnemy = (transform.position - positionGoal).magnitude;
            if (distBetweenSelfAndClosestEnemy < distCrit)
            {
                agent.updateRotation = false;
                transform.LookAt(positionGoal);
            } else
            {
                agent.updateRotation = true;
            }
        } else if (entitySelf.GetBehavior() == Entity.Behavior.Defense) //Si le comportement est defensee, l'unité se rapproche de son roi, puis attaque l'unité ennemi la plus proche une fois qu'elle est suffisement proche de son roi
        {
            foreach (var entity in entityObjectsList)
            {
                var entityOther = entity.GetComponent<BasicEntity>();
                if (entityOther.GetIsKing())
                {
                    if (entitySelf.GetIsEnemy() == entityOther.GetIsEnemy())
                    {
                        var myKing = entity;
                        float distBetweenSelfAndGoal = (transform.position - myKing.GetComponent<Transform>().position).magnitude;
                        if (distBetweenSelfAndGoal > distCrit) //Cas où on se rapproche de son roi
                        {
                            positionGoal = myKing.GetComponent<Transform>().position;
                        }
                        else //Cas où on se rapproche de l'unité enemi la plus proche
                        {
                            Vector3 closestEnemyPosition = getClosestenemy().GetComponent<Transform>().position;
                            float distBetweenSelfAndClosestEnemy = (transform.position - closestEnemyPosition).magnitude;
                            if (distBetweenSelfAndClosestEnemy < distCrit)
                            {
                                positionGoal = closestEnemyPosition;
                                agent.updateRotation = false;
                                transform.LookAt(closestEnemyPosition);
                            }
                            else
                            {
                                agent.updateRotation = true;
                                positionGoal = transform.position;
                            }
                        }
                        break;
                    }
                }
            }

        } else
        {
            positionGoal = defaultGoal;
        }
        if (!entitySelf.GetIsActive()) //L'unité ne bouge pas si elle est inactive
        {
            positionGoal = transform.position;
        }
    }

    private Object getClosestenemy() //renvoie l'enemi le plus proche
    {
        var entityObjectsList = FindObjectsOfType(typeof(BasicEntity));
        var closestEnemy = entityObjectsList[0];
        float minDistance = Mathf.Infinity;
        foreach (Entity entity in entityObjectsList)
        {
            var entityOther = entity.GetComponent<BasicEntity>();
            if (entitySelf.GetIsEnemy() != entityOther.GetIsEnemy())
            {
                var enemy = entity;
                float dist = (enemy.GetComponent<Transform>().position - transform.position).magnitude;
                if ((dist > 0) && (dist < minDistance))
                {
                    closestEnemy = enemy;
                    minDistance = dist;
                }
            }
        }
        return closestEnemy;
    }
}
