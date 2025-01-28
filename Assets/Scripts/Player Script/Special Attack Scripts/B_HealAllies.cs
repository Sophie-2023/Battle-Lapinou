using Unity.VisualScripting;
using UnityEngine;

public class B_HealAllies : MonoBehaviour, SpecialAttack
{
    private float healrange = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute(Entity entityself)
    //Heal 50% des pv aux alliés à porté
    {
        Debug.Log("Attaque spéciale : Soin !");
        //L'unité soigne les alliés à porté
        var entityObjectsList = FindObjectsOfType(typeof(BasicEntity));
        foreach (var entityobj in entityObjectsList)
        {
            BasicEntity entityother = entityobj.GetComponent<BasicEntity>();
            B_Health healthOther = entityobj.GetComponent<B_Health>();
            if (entityother.GetIsEnemy()==entityself.GetIsEnemy())
            {
                float dist = (entityother.transform.position - entityself.transform.position).magnitude;
                if ((dist < healrange)&&(dist>0)) 
                {
                    healthOther.heal();
                }
            }
        }
    }
}
