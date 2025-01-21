using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class B_MoveUnit : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject unitShadow;
    [SerializeField] private float clickDuration = 0.2f;
    [SerializeField] private bool canBeMoved = false;
    private Coroutine longClickCoroutine; // R�f�rence � la coroutine en cours
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
 
    }

    private void OnMouseDrag()
    {
        if (!canBeMoved && longClickCoroutine == null)
        {
            longClickCoroutine = StartCoroutine(LongClick());
        }

        if (canBeMoved)
        {
            unitShadow.SetActive(true);
            MoveUnit();
        }
    }

    private void OnMouseUp()
    {
        if (longClickCoroutine != null)
        {
            StopCoroutine(longClickCoroutine);
            longClickCoroutine = null;
        }
        canBeMoved = false;
        unitShadow.SetActive(false);
    }

    private void MoveUnit()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        {
            Vector3 point = hit.point;
            point.y = 3f; // Quand on place une unit� sur la map, l'unit� est surr�lev�e par rapport � la map
            transform.position = point;
        }
    }

    private IEnumerator LongClick()
    {
        yield return new WaitForSeconds(clickDuration);
        canBeMoved = true;
        longClickCoroutine = null;
    }

}
