using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class B_BoutiqueManager : MonoBehaviour
{
    private GameObject unitToCreate;
    [SerializeField] private int amountMoney;

    private Camera _camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask obstacleLayerMask;

    [Header("UI Elements")]
    [SerializeField] private B_Boutique_UI_Manager b_UI_Manager;
    [SerializeField] private Transform buttonParent; // Conteneur des boutons
    [SerializeField] private Button buttonPrefab;    // Bouton de modèle à cloner
    [SerializeField] private TextMeshProUGUI amountMoneyText;

    [Header("Unités à vendre")]
    [SerializeField] private List<GameObject> unitsToSell;   // Liste des unités disponibles à la vente

    void Start()
    {
        amountMoney = B_GameData.Instance.currentMoney;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        amountMoneyText.text = amountMoney.ToString() + " coins";
        PopulateShop();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
            {
                bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
                if (!Physics.Raycast(ray, float.MaxValue, obstacleLayerMask) && !isOverUI)
                {
                    if (unitToCreate != null)
                    {
                        Vector3 position = new Vector3(hit.point.x, 3, hit.point.z);
                        BuyUnit(position);
                    }
                }
            }
        }
    }

    private void PopulateShop()
    {
        foreach (GameObject unit in unitsToSell)
        {
            // Crée un nouveau bouton à partir du prefab
            Button newButton = Instantiate(buttonPrefab, buttonParent);

            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.fontSize = 35;
            buttonText.text = $"{unit.name} \n {unit.GetComponent<BasicEntity>().GetCost()} Coins";

            newButton.onClick.AddListener(() => ChooseUnit(unit));
            newButton.onClick.AddListener(() => b_UI_Manager.SetUnitButtonColor(newButton));
        }
    }

    private void BuyUnit(Vector3 position)
    {
        int unitPrice = unitToCreate.GetComponent<BasicEntity>().GetCost();
        if (amountMoney < unitPrice)
        {
            Debug.Log("Vous n'avez pas assez d'argent !");
        }
        else
        {
            GameObject newUnit = Instantiate(unitToCreate, position, Quaternion.identity);
            newUnit.GetComponent<Rigidbody>().isKinematic = true;
            B_LevelManager.Instance.AddToPlayerArmy(newUnit);
            amountMoney -= unitPrice;
            if (amountMoney < 0) { amountMoney = 0; }
            amountMoneyText.text = amountMoney.ToString() + " coins";
        }
    }

    public void SellUnit()
    {
        GameObject unitToRemove = B_LevelManager.Instance.GetSelectedUnit();
        if (unitToRemove != null)
        {
            amountMoney += unitToRemove.GetComponent<BasicEntity>().GetCost();
            amountMoneyText.text = amountMoney.ToString() + " coins";
            B_LevelManager.Instance.RemoveFromPlayerArmy();
            Destroy(unitToRemove);
        }
    }

    private void ChooseUnit(GameObject unit)
    {
        unitToCreate = unit;
    }

}
