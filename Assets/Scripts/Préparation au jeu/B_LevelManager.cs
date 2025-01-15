using System.Collections.Generic;
using UnityEngine;

public class B_LevelManager : MonoBehaviour
{
    private List<GameObject> playerArmy = new List<GameObject>();

    // Singleton pour acc�der facilement au LevelManager depuis d'autres scripts
    public static B_LevelManager Instance;

    private void Awake()
    {
        // Assurer qu'il n'y a qu'une seule instance du LevelManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // M�thode pour ajouter une unit� au playerArmy
    public void AddToPlayerArmy(GameObject unit)
    {
        playerArmy.Add(unit);
    }

    // M�thode pour retirer un GameObject de la liste playerArmy
    public void RemoveFromPlayerArmy(GameObject unit)
    {
        if (playerArmy.Contains(unit))
        {
            playerArmy.Remove(unit);
        }
        else
        {
            Debug.LogWarning($"{unit.name} n'est pas dans playerArmy !");
        }
    }

    public void SetActivePlayerArmy()
    {
        foreach (GameObject unit in playerArmy)
        {
            unit.GetComponent<BasicEntity>().SetIsActive(true);
        }
        Debug.Log("Les unit�s sont activ�es !");
    }
}
