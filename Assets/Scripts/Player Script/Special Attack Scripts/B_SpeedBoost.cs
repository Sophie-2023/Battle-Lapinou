using UnityEngine;
using System.Collections;

public class B_SpeedBoost : MonoBehaviour, SpecialAttack
{
    private float BoostDuration = 5f;
    private bool isQuick = false;
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
        //Augmente temporairement la vitesse
        Debug.Log("Attaque spéciale : L'éclaire de Konoha !");
        //L'unité augmente temporairement sa vitesse
        StartCoroutine(quickCoroutine(entityself));
    }

    IEnumerator quickCoroutine(Entity entityself)
    {
        if (!isQuick)
        {
            isQuick = true;
            float spd = entityself.GetSpeed();
            entityself.SetSpeed(1.5f*spd);
            yield return new WaitForSeconds(BoostDuration);
            entityself.SetSpeed(spd);
            isQuick = false;
        }
    }
}
