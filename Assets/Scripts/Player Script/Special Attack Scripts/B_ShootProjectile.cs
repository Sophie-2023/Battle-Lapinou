using Unity.VisualScripting;
using UnityEngine;

public class B_ShootProjectile : MonoBehaviour, SpecialAttack
{
    private Vector3 offset = new Vector3(0, 2, 0);
    private GameObject projectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute(Entity entityself)
    //Tire un projectile vers le roi adverse, le projectile divise les points de vie par 2
    {
        Debug.Log("Attaque spéciale : Sniper !");
        //L'unité tire un projectile vers le roi adverse
        

        var entityObjectsList = FindObjectsOfType(typeof(BasicEntity));
        foreach (var entity in entityObjectsList)
        {
            var entityOther = entity.GetComponent<BasicEntity>();
            if (entityOther.GetIsKing())
            {
                if (entityself.GetIsEnemy() != entityOther.GetIsEnemy())
                {
                    GameObject newprojectile = Instantiate(projectile, entityself.gameObject.transform.position + offset, entityself.gameObject.transform.rotation);
                    newprojectile.GetComponent<B_MovementProjectile>().setTarget(entityOther.gameObject);
                    break;
                }
            }
        }
    }

    public void setProjectile(GameObject projectile)
    {
        this.projectile = projectile;
    }
}
