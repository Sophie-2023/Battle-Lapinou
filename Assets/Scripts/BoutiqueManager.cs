using UnityEngine;

public class BoutiqueManager : MonoBehaviour
{
    [SerializeField] private Transform spawnUnit;
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
