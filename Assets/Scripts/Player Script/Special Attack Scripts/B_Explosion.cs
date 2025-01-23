using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class B_Explosion : MonoBehaviour, SpecialAttack
{
    float explosionrange = 3f;
    float power = 10f;
    float explosionEffectDuration = 3f;
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
        Debug.Log("Attaque spéciale : Souffle de la calamité !");
        //Les enemis et alliés à porté subissent une onde de choc
        StartCoroutine(ExplosionCoroutine(entityself));
    }

    IEnumerator ExplosionCoroutine(Entity entityself)
    {
        var entityObjectsList = FindObjectsOfType(typeof(BasicEntity));
        Rigidbody rb = entityself.gameObject.GetComponent<Rigidbody>();
        List<Object> lst = new List<Object>();
        foreach (Object entityobj in entityObjectsList)
        {
            BasicEntity entityother = entityobj.GetComponent<BasicEntity>();
            if (entityother != entityself)
            {
                float dist = (entityother.transform.position - entityself.transform.position).magnitude;
                if (dist < explosionrange)
                {
                    entityother.SetIsActive(false);
                    lst.Add(entityobj);
                }
            }
        }
        rb.AddExplosionForce(power, entityself.gameObject.transform.position, explosionrange);
        yield return new WaitForSeconds(explosionEffectDuration);
        foreach(Object entityobj in lst)
        {
            if (entityobj!=null)
            {
                BasicEntity entityother = entityobj.GetComponent<BasicEntity>();
                entityother.SetIsActive(true);
            }
        }
    }
}
