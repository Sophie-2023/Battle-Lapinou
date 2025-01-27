using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class B_Boutique_UI_Manager : MonoBehaviour
{
    [SerializeField] private Button offenseButton;
    [SerializeField] private Button defenseButton;
    [SerializeField] private Button neutralButton;
    [SerializeField] private Button isKingButton;
    [SerializeField] private GameObject boutique;

    [SerializeField] private GameObject gameUI;
    [SerializeField] private TextMeshProUGUI nbEnnemis;
    [SerializeField] private TextMeshProUGUI timer;
    private float startTime;

    private B_LevelManager levelManager;

    [SerializeField] private List<Button> unitBoutique = new List<Button>();
    private Button previousUnitButton;

    void Start()
    {
        levelManager = B_LevelManager.Instance;
    }

    void Update()
    {
        if (levelManager.GetIsGameStarted()) 
        {
            float time = Time.time - startTime;
            int minutes = (int)(time / 60f);
            int seconds = (int)(time % 60f);
            timer.text = minutes + "m " + seconds + "s";

            nbEnnemis.text = $"{levelManager.enemyArmy.Count()} ennemis restants";
        }
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

    public void StartGame()
    {
        if (levelManager.GetIsGameStarted()) 
        {
            boutique.SetActive(false);
            gameUI.SetActive(true);
            startTime = Time.time;
        }
    }


}
