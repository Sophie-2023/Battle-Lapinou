using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B_Boutique_UI_Manager : MonoBehaviour
{
    [SerializeField] private Button offenseButton;
    [SerializeField] private Button defenseButton;
    [SerializeField] private Button neutralButton;
    [SerializeField] private Button isKingButton;
    [SerializeField] private GameObject boutique;

    private B_LevelManager levelManager;

    [SerializeField] private List<Button> unitBoutique = new List<Button>();
    private Button previousUnitButton;

    void Start()
    {
        levelManager = B_LevelManager.Instance;
    }

    void Update()
    {
        
    }

    public void SetButtonColor()
    {
        GameObject selectedUnit = levelManager.GetSelectedUnit();
        BasicEntity basicEntity = selectedUnit.GetComponent<BasicEntity>();

        if (basicEntity.GetIsKing() == true)
        {
            isKingButton.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            isKingButton.GetComponent<Image>().color = Color.white;
        }

        if (basicEntity.GetBehavior() == Entity.Behavior.Offense) 
        {
            offenseButton.GetComponent<Image>().color = Color.red;
            defenseButton.GetComponent<Image>().color = Color.white;
            neutralButton.GetComponent<Image>().color = Color.white;
        }
        else if (basicEntity.GetBehavior() == Entity.Behavior.Defense)
        {
            offenseButton.GetComponent<Image>().color = Color.white;
            defenseButton.GetComponent<Image>().color = Color.blue;
            neutralButton.GetComponent<Image>().color = Color.white;
        }
        else if (basicEntity.GetBehavior() == Entity.Behavior.Neutral)
        {
            offenseButton.GetComponent<Image>().color = Color.white;
            defenseButton.GetComponent<Image>().color = Color.white;
            neutralButton.GetComponent<Image>().color = Color.green;
        }

    }

    public void SetUnitButtonColor(Button unitButton)
    {
        if (previousUnitButton != null) 
        {
            previousUnitButton.GetComponent<Image>().color = Color.white;
        }
        unitButton.GetComponent<Image>().color = Color.yellow;
        previousUnitButton = unitButton;

    }

    public void CloseBoutique()
    {
        if (levelManager.GetIsGameStarted()) 
        {
            boutique.SetActive(false);
        }
    }


}
