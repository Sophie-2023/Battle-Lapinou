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
        chooseBehavior.unit = gameObject;
        chooseBehavior.ActivatePanel();
    }
}
