using Unity.VisualScripting;
using UnityEngine;

public class B_MoveUnit : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject unitShadow;
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
 
    }

    private void OnMouseDrag()
    {
        unitShadow.SetActive(true);
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        {
            Vector3 point = hit.point;
            point.y = 3f; // Quand on place une unit� sur la map, l'unit� est surr�lev�e par rapport � la map
            transform.position = point;
        }
    }


    private void OnMouseExit()
    {
        unitShadow.SetActive(false);
    }
}
