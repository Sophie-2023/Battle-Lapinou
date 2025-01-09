using UnityEngine;

public class B_ChooseBehavior : MonoBehaviour
{
    [SerializeField] private GameObject chooseBehaviorPanel;
    //private Camera _camera;
    //[SerializeField] private LayerMask layerMask;

    public GameObject unit = null;

    void Start()
    {
       //_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousePosition = Input.mousePosition;
        //    Ray ray = _camera.ScreenPointToRay(mousePosition);
        //    if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        //    {
        //        unit = hit.collider.gameObject;
        //        chooseBehaviorPanel.SetActive(true);
        //    }
        //    else
        //    {
        //        chooseBehaviorPanel.SetActive(false);
        //    }
        //}
    }

    public void ActivatePanel()
    {
        chooseBehaviorPanel.SetActive(true);
    }


    public void SetNeutral()
    {
        unit.GetComponent<BasicEntity>().SetBehavior(Entity.Behavior.Neutral);
    }

    public void SetOffense()
    {
        unit.GetComponent<BasicEntity>().SetBehavior(Entity.Behavior.Offense);
    }

    public void SetDefense()
    {
        unit.GetComponent<BasicEntity>().SetBehavior(Entity.Behavior.Defense);
    }

}
