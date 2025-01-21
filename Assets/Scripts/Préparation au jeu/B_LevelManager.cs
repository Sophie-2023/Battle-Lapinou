using System.Collections.Generic;
using UnityEngine;

public class B_LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerArmy = new List<GameObject>();
    [SerializeField] private GameObject selectedUnit; // L'unit� s�l�ctionn�e par le joueur lors de la pr�paration de la partie
    [SerializeField] private GameObject king;
    [SerializeField] private GameObject crownOfSelectedUnit; // La couronne en enfant de l'unit� s�l�ctionn�
    [SerializeField] private GameObject kingCrown;

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

    public GameObject GetCrown()
    {
        return crownOfSelectedUnit;
    }

    public void SetCrown(GameObject unit) 
    {
        crownOfSelectedUnit = unit;
    }

    public void SetKingCrown(GameObject unit)
    {
        kingCrown = unit;
    }

    public GameObject GetKingCrown()
    {
        return kingCrown;
    }

    // M�thode pour ajouter une unit� au playerArmy
    public void AddToPlayerArmy(GameObject unit)
    {
        playerArmy.Add(unit);
    }

    // M�thode pour retirer une unit� de playerArmy (l'unit� � retirer est dans la variable selectedUnit (s�l�ctionn�e par le joueur avec clic gauche de la souris))
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
