using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class B_Berserk : MonoBehaviour, SpecialAttack
{
    private float BerserkDuration = 5f;
    private bool isBerserk = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Execute(Entity entityself)
    //Augment temporairement l'attaque
    {
        Debug.Log("Attaque spéciale : Berserk mode !");
        //L'unité augmente temporairement son attaque
        StartCoroutine(berserkCoroutine(entityself));
    }

    IEnumerator berserkCoroutine(Entity entityself)
    {
        if (!isBerserk)
        {
            isBerserk = true;
            int atk = entityself.GetAttack();
            entityself.SetAttack(Mathf.FloorToInt(1.5f * atk));
            yield return new WaitForSeconds(BerserkDuration);
            entityself.SetAttack(atk);
            isBerserk = false;
        }
    }
}
