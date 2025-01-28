using UnityEngine;

public class B_ChangeColor : MonoBehaviour
{
    //Change la couleur de l'unité en fonction de si elle est alliée ou ennemie
    [SerializeField] private Material matAllies;
    [SerializeField] private Material matEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GetComponent<BasicEntity>().GetIsEnemy())
        {
            GetComponent<MeshRenderer>().material = matEnemy;
        } else
        {
            GetComponent<MeshRenderer>().material = matAllies;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
