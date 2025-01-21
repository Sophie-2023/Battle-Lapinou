using UnityEngine;

public class B_ChooseBehavior : MonoBehaviour
{

    void Start()
    {
       
    }

    void Update()
    {

    }

    public void SetKing()
    {
        GameObject selectedUnit = B_LevelManager.Instance.GetSelectedUnit();
        GameObject previousKing = B_LevelManager.Instance.GetKing();
        if (previousKing != null) 
        {
            previousKing.GetComponent<BasicEntity>().SetIsKing(false);
            B_LevelManager.Instance.GetKingCrown().SetActive(false);
        }
        B_LevelManager.Instance.SetKing(selectedUnit);
        B_LevelManager.Instance.SetKingCrown(B_LevelManager.Instance.GetCrown());
        B_LevelManager.Instance.GetKingCrown().SetActive(true);  
        selectedUnit.GetComponent<BasicEntity>().SetIsKing(true);
    }

    public void SetNeutral()
    {
        B_LevelManager.Instance.GetSelectedUnit().GetComponent<BasicEntity>().SetBehavior(Entity.Behavior.Neutral);
    }

    public void SetOffense()
    {
        B_LevelManager.Instance.GetSelectedUnit().GetComponent<BasicEntity>().SetBehavior(Entity.Behavior.Offense);
    }

    public void SetDefense()
    {
        B_LevelManager.Instance.GetSelectedUnit().GetComponent<BasicEntity>().SetBehavior(Entity.Behavior.Defense);
    }

}
