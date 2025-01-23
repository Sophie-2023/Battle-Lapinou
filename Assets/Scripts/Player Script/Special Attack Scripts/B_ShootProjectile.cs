using Unity.VisualScripting;
using UnityEngine;

public class B_ShootProjectile : MonoBehaviour, SpecialAttack
{
    private Vector3 offset = new Vector3(0, 1, 0);
    [SerializeField] GameObject projectile;
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
        Debug.Log("Attaque spéciale : Sniper !");
        //L'unité tire un projectile vers le roi adverse
        Instantiate(projectile, entityself.gameObject.transform.position + offset, entityself.gameObject.transform.rotation);   
    }
}
