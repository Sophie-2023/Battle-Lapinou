using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class B_LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerArmy = new List<GameObject>();
    [SerializeField] public List<GameObject> enemyArmy = new List<GameObject>();
    [SerializeField] private List<GameObject> canonList = new List<GameObject>();
    [SerializeField] private GameObject selectedUnit; // L'unit� s�l�ctionn�e par le joueur lors de la pr�paration de la partie
    [SerializeField] private GameObject king;
    [SerializeField] private GameObject crownOfSelectedUnit; // La couronne en enfant de l'unit� s�l�ctionn�
    [SerializeField] private GameObject kingCrown;
    [SerializeField] private GameObject enemyKing;

    [SerializeField] private GameObject boutiqueManager;

    [SerializeField] private bool isGameStarted = false;
    [SerializeField] private bool isGameOver = false;

    public int enemyCount;
    [SerializeField] private B_Boutique_UI_Manager UI_Manager;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private int winCoins;

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

        if (isGameStarted && !isGameOver)
        {
            CountEnemy();

            if (king == null)
            {
                GameOver();
            }

            else if (enemyKing == null) 
            {
                UI_Manager.UpdateUIinGame();
                Win();
            }
        }
    }

    private void CountEnemy()
    {
        int count = 0;
        foreach (GameObject enemy in enemyArmy)
        {
            if (enemy != null)
            { count++; }
        }
        enemyCount = count;
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

    private void SetActivePlayerArmy()
    {
        selectedUnit = null;
        crownOfSelectedUnit = null;
        foreach (GameObject unit in playerArmy)
        {
            unit.GetComponent<BasicEntity>().SetIsActive(true);
            unit.GetComponent<B_MoveUnit>().enabled = false;
            unit.GetComponent<Outline>().enabled = false;
        }
    }

    private void SetActiveEnemyArmy()
    {
        foreach (GameObject unit in enemyArmy)
        {
            unit.GetComponent<BasicEntity>().SetIsActive(true);
            unit.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    private void DesactivePlayerArmy()
    {
        foreach (GameObject unit in playerArmy)
        {
            if (unit != null)
            {
                unit.GetComponent<BasicEntity>().SetIsActive(false);
            }
        }
    }

    private void DesactiveEnemyArmy()
    {
        foreach (GameObject unit in enemyArmy)
        {
            if (unit != null)
            {
                unit.GetComponent<BasicEntity>().SetIsActive(false);
            }
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
        isGameStarted = false;
        DesactivePlayerArmy();
        DesactiveEnemyArmy();
        gameOverPanel.SetActive(true);
    }

    private void Win()
    {
        Debug.Log("You win !");
        isGameStarted = false;
        DesactivePlayerArmy();
        DesactiveEnemyArmy();
        winPanel.SetActive(true);
        B_GameData.Instance.currentMoney += winCoins;
    }
}
