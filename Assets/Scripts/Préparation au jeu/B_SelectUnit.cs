using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class B_SelectUnit : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject choosePropertiesPanel;

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
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask)) // Les unités alliées et ennemies ont le layer "Character"
            {
                GameObject unit = hit.collider.gameObject;
                if (!unit.GetComponent<BasicEntity>().GetIsEnemy())
                {
                    SelectUnit(hit.collider.gameObject);
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
        if (unit.TryGetComponent<Outline>(out Outline outline))
        {
            outline.enabled = true;
        }
        choosePropertiesPanel.SetActive(true);
    }
}
