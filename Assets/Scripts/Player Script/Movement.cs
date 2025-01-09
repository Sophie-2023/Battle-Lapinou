using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 directionGoal; //Vecteur unitaire pointant vers l'objectif de l'unité en fonction de son comportement (offense->roi adverse, neutral->unité ennemi la plus proche, defense->son propre roi)
    private Vector3 direction; 
    private Entity entitySelf;
    private float spd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = transform.forward;
        entitySelf = GetComponent<BasicEntity>();
        spd = entitySelf.GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirectionGoal();
    }

    private void UpdateDirectionGoal()
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
                        directionGoal = (enemyKing.GetComponent<Transform>().position - transform.position).normalized;
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
            directionGoal = (closestEnemy.GetComponent<Transform>().position - transform.position).normalized;
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
                        directionGoal = (myKing.GetComponent<Transform>().position - transform.position).normalized;
                        break;
                    }
                }
            }

        } else
        {
            directionGoal = transform.forward;
        }
    }
}
