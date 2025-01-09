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

    public void CreateUnit(GameObject unit)
    {
        if (amountMoney <= 0)
        {
            Debug.Log("Vous n'avez pas assez d'argent !");
        }
        else
        {
            Instantiate(unit, spawnUnit.position, Quaternion.identity);
            amountMoney -= unit.GetComponent<BasicEntity>().GetCost();
            if (amountMoney < 0) { amountMoney = 0; }
            amountMoneyText.text = amountMoney.ToString() + " coins";
        }
    }
}
