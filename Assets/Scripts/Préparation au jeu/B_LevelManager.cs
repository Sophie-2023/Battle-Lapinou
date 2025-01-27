using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class B_LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerArmy = new List<GameObject>();
    [SerializeField] public List<GameObject> enemyArmy = new List<GameObject>();
    [SerializeField] private List<GameObject> canonList = new List<GameObject>();
    [SerializeField] private GameObject selectedUnit; // L'unité séléctionnée par le joueur lors de la préparation de la partie
    [SerializeField] private GameObject king;
    [SerializeField] private GameObject crownOfSelectedUnit; // La couronne en enfant de l'unité séléctionné
    [SerializeField] private GameObject kingCrown;

    [SerializeField] private GameObject boutiqueManager;

    [SerializeField] private bool isGameStarted = false;
    [SerializeField] private bool isGameOver = false;

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
        if (isGameStarted && !isGameOver && king == null )
        {
            GameOver();
        }
    }

    public bool GetIsGameStarted()
    {
        return isGameStarted;
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

    private void SetActivePlayerArmy()
    {
        selectedUnit = null;
        crownOfSelectedUnit = null;
        foreach (GameObject unit in playerArmy)
        {
            unit.GetComponent<BasicEntity>().SetIsActive(true);
            unit.GetComponent<B_MoveUnit>().enabled = false;
            unit.GetComponent<Outline>().enabled = false;
            unit.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void SetActiveEnemyArmy()
    {
        foreach (GameObject unit in enemyArmy)
        {
            unit.GetComponent<BasicEntity>().SetIsActive(true);
            unit.GetComponent<Rigidbody>().isKinematic = false;
            unit.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    private void DesactivateCanonTrigger()
    {
        foreach (GameObject canon in canonList)
        {
            canon.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void Play()
    {
        if (king == null)
        {
            Debug.Log("Vous n'avez pas encore choisi le roi !");
        }
        else
        {
            boutiqueManager.SetActive(false);
            isGameStarted = true;
            DesactivateCanonTrigger();
            SetActivePlayerArmy();
            SetActiveEnemyArmy();
            Debug.Log("Play !");
        }
    }

    private void GameOver()
    {
        Debug.Log("Vous avez perdu !");
        isGameOver = true;
    }
}
