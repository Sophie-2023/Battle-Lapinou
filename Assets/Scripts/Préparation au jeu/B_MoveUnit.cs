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
    private Coroutine longClickCoroutine; // Référence à la coroutine en cours
    private BasicEntity basicEntity;

    [SerializeField] private Transform spawnTransform;
    [SerializeField] private bool isOnInetractible = false;
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        basicEntity = gameObject.GetComponent<BasicEntity>();
        spawnTransform = GameObject.Find("SpawnUnit").transform;
    }

    void Update()
    {
 
    }

    private void OnMouseDrag()
    {
        if (basicEntity.GetIsActive() == false)
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
    }

    private void OnMouseUp()
    {
        if (basicEntity.GetIsActive() == false)
        {
            if (longClickCoroutine != null)
            {
                StopCoroutine(longClickCoroutine);
                longClickCoroutine = null;
            }
            canBeMoved = false;
            unitShadow.SetActive(false);
            if (isOnInetractible) { transform.position = spawnTransform.position; }
        }
    }

    private void MoveUnit()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        {
            Vector3 point = hit.point;
            point.y = 3f; // Quand on place une unité sur la map, l'unité est surrélevée par rapport à la map
            transform.position = point;
        }
    }

    private IEnumerator LongClick()
    {
        yield return new WaitForSeconds(clickDuration);
        canBeMoved = true;
        longClickCoroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Interactible")
        {
            isOnInetractible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Interactible")
        {
            isOnInetractible = false;
        }
    }

}
