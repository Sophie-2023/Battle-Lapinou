using UnityEngine;

public class B_BoutiqueManager : MonoBehaviour
{
    [SerializeField] private Transform spawnUnit;
    [SerializeField] private int money;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CreateUnit(GameObject unit)
    {
        Instantiate(unit, spawnUnit.position, Quaternion.identity);
    }
}
