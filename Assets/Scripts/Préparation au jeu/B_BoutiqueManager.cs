using TMPro;
using UnityEngine;

public class B_BoutiqueManager : MonoBehaviour
{
    [SerializeField] private Transform spawnUnit;
    [SerializeField] private int amountMoney;
    [SerializeField] private TextMeshProUGUI amountMoneyText;
    void Start()
    {
        amountMoneyText.text = amountMoney.ToString() + " coins";
    }

    void Update()
    {
        
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
}
