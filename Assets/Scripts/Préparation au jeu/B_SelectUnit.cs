using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class B_SelectUnit : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject choosePropertiesPanel;
    [SerializeField] private B_Boutique_UI_Manager b_UI_manager;

    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask)) // Les unit�s alli�es et ennemies ont le layer "Character"
            {
                GameObject unit = hit.collider.gameObject;
                BasicEntity basicEntity = unit.GetComponent<BasicEntity>();
                if (!basicEntity.GetIsEnemy() && basicEntity.GetIsActive() == false)
                {
                    SelectUnit(unit);
                }
            }
        }
    }

    private void SelectUnit(GameObject unit)
    {
        GameObject previousSelectedUnit = B_LevelManager.Instance.GetSelectedUnit();
        if (previousSelectedUnit != null && previousSelectedUnit.TryGetComponent<Outline>(out Outline oldOutline))
        {
            oldOutline.enabled = false;
        }
        B_LevelManager.Instance.SetSelectedUnit(unit);
        B_LevelManager.Instance.SetCrown(unit.transform.Find("Couronne").gameObject);
        if (unit.TryGetComponent<Outline>(out Outline outline))
        {
            outline.enabled = true;
        }
        b_UI_manager.SetButtonColor();
        choosePropertiesPanel.SetActive(true);
    }
}
