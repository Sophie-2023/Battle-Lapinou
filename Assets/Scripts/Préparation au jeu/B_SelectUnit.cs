using Unity.VisualScripting;
using UnityEngine;

public class B_SelectUnit : MonoBehaviour
{
    private B_ChooseBehavior chooseBehavior;

    void Start()
    {
        chooseBehavior = GameObject.Find("Choose Behavior Manager").GetComponent<B_ChooseBehavior>();
    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        GameObject previousSelectedUnit = B_LevelManager.Instance.GetSelectedUnit();
        if (previousSelectedUnit != null && previousSelectedUnit.TryGetComponent<Outline>(out Outline oldOutline))
        {
            oldOutline.enabled = false;
        }
        B_LevelManager.Instance.SetSelectedUnit(gameObject);
        if (gameObject.TryGetComponent<Outline>(out Outline outline)) 
        {
            outline.enabled = true;
        }
        chooseBehavior.unit = gameObject;
        chooseBehavior.ActivatePanel();
    }
}
