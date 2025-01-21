using TMPro;
using UnityEngine;

public class B_BoutiqueManager : MonoBehaviour
{
    private GameObject unitToCreate;
    [SerializeField] private Transform spawnUnit;
    [SerializeField] private int amountMoney;
    [SerializeField] private TextMeshProUGUI amountMoneyText;

    private Camera _camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask obstacleLayerMask;
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        amountMoneyText.text = amountMoney.ToString() + " coins";
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
                        CreateUnit(position);
                    }
                }
            }
        }
    }

    public void BuyUnit(GameObject unit)
    {
        int unitPrice = unit.GetComponent<BasicEntity>().GetCost();
        if (amountMoney <= unitPrice)
        {
            Debug.Log("Vous n'avez pas assez d'argent !");
        }
        else
        {
            GameObject newUnit = Instantiate(unit, spawnUnit.position, Quaternion.identity);
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

    public void ChooseUnit(GameObject unit)
    {
        unitToCreate = unit;
    }

    private void CreateUnit(Vector3 position)
    {
        int unitPrice = unitToCreate.GetComponent<BasicEntity>().GetCost();
        if (amountMoney <= unitPrice)
        {
            Debug.Log("Vous n'avez pas assez d'argent !");
        }
        else
        {
            GameObject newUnit = Instantiate(unitToCreate, position, Quaternion.identity);
            B_LevelManager.Instance.AddToPlayerArmy(newUnit);
            amountMoney -= unitPrice;
            if (amountMoney < 0) { amountMoney = 0; }
            amountMoneyText.text = amountMoney.ToString() + " coins";
        }
    }
}
