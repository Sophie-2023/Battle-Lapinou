using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class B_Makarena : MonoBehaviour, SpecialAttack
{
    private float makarenaDuration = 5f;
    private float makarenaRange = 3f;
    private bool DancingMakarena = false;
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
        Debug.Log("Attaque spéciale : Makarena !");
        //L'unité et les enemis à porté dansent temporairement la makarena 
        StartCoroutine(makarenaCoroutine(entityself));
    }

    IEnumerator makarenaCoroutine(Entity entityself)
    {
        DancingMakarena = true;
        var entityObjectsList = FindObjectsOfType(typeof(BasicEntity));

        foreach (var entityobj in entityObjectsList)
        {
            BasicEntity entityother = entityobj.GetComponent<BasicEntity>();
            if (entityother.GetIsEnemy() != entityself.GetIsEnemy())
            {
                float dist = (entityother.transform.position - entityself.transform.position).magnitude;
                if (dist < makarenaRange)
                {
                    entityother.SetIsActive(false);
                }
            }
        }
        entityself.SetIsActive(false);

        yield return new WaitForSeconds(makarenaDuration);

        entityObjectsList = FindObjectsOfType(typeof(BasicEntity));
        foreach (var entityobj in entityObjectsList)
        {
            BasicEntity entityother = entityobj.GetComponent<BasicEntity>();
            if (entityother.GetIsEnemy() != entityself.GetIsEnemy())
            {
                float dist = (entityother.transform.position - entityself.transform.position).magnitude;
                if (dist < makarenaRange)
                {
                    entityother.SetIsActive(true);
                }
            }
        }
        entityself.SetIsActive(true);
        DancingMakarena = false;
    }

    public bool isDancingMakarena()
    {
        return DancingMakarena;
    }
}
