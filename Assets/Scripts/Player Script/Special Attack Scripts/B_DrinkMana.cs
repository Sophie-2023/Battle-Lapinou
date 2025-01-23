using Unity.VisualScripting;
using UnityEngine;

public class B_DrinkMana : MonoBehaviour, SpecialAttack
{
    private float drinkrange = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute(Entity entityself)
    {
        //L'unité baisse le mana des ennemis à porté
        Debug.Log("Attaque spéciale : Cuite de Mana !");
        var entityObjectsList = FindObjectsOfType(typeof(BasicEntity));
        foreach (var entityobj in entityObjectsList)
        {
            BasicEntity entityother = entityobj.GetComponent<BasicEntity>();
            B_Mana manaOther = entityobj.GetComponent<B_Mana>();
            if (entityother.GetIsEnemy() != entityself.GetIsEnemy())
            {
                float dist = (entityother.transform.position - entityself.transform.position).magnitude;
                if ((dist < drinkrange) && (dist > 0))
                {
                    manaOther.manaDrained();
                }
            }
        }
    }
}
