using System.Collections.Generic;
using UnityEngine;

public class B_LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerArmy = new List<GameObject>();
    [SerializeField] private GameObject selectedUnit; // L'unité séléctionnée par le joueur lors de la préparation de la partie
    [SerializeField] private GameObject king;

    // Singleton pour accéder facilement au LevelManager depuis d'autres scripts
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

    public void SetSelectedUnit(GameObject unit)
    {
        selectedUnit = unit;
    }

    public GameObject GetSelectedUnit()
    {
        return selectedUnit;
    }

    public void SetKing(GameObject unit)
    {
        king = unit;
    }

    public GameObject GetKing() 
    { 
        return king; 
    }

    // Méthode pour ajouter une unité au playerArmy
    public void AddToPlayerArmy(GameObject unit)
    {
        playerArmy.Add(unit);
    }

    // Méthode pour retirer une unité de playerArmy (l'unité à retirer est dans la variable selectedUnit (séléctionnée par le joueur avec clic gauche de la souris))
    public void RemoveFromPlayerArmy()
    {
        if (playerArmy.Contains(selectedUnit))
        {
            playerArmy.Remove(selectedUnit);
        }
        else
        {
            Debug.LogWarning($"{selectedUnit.name} n'est pas dans playerArmy !");
        }
    }

    public void SetActivePlayerArmy()
    {
        foreach (GameObject unit in playerArmy)
        {
            unit.GetComponent<BasicEntity>().SetIsActive(true);
        }
    }
}
