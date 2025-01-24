using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_InteractibleZone : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private Camera _camera;
    [SerializeField] private LayerMask layerMask;
    private B_LevelManager levelManager;

    private bool canBeActivated = true;
    [SerializeField] private float rechargeTime = 5;
    [SerializeField] private Outline outline;

    [SerializeField] protected List<BasicEntity> entitiesInZone = new List<BasicEntity>();

    void Start()
    {
        StartBehavior();
    }

    void Update()
    {
        UpdateBehavior();
    }

    public virtual void StartBehavior()
    {
        _camera = Camera.main;
        levelManager = B_LevelManager.Instance;
    }

    public virtual void UpdateBehavior()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
            {
                if (levelManager.GetIsGameStarted() && hit.collider.gameObject == gameObject && canBeActivated)
                {
                    _particleSystem.Play();
                    StartCoroutine(RechargeTime());
                    StartZoneAction();
                }
            }
        }
    }

    public virtual void StartZoneAction()
    {
        Debug.Log("Start zone action");
    }

    private IEnumerator RechargeTime()
    {
        canBeActivated = false;
        outline.enabled = false;
        yield return new WaitForSeconds(rechargeTime);
        canBeActivated = true;
        outline.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BasicEntity>(out BasicEntity entity))
        {
            entitiesInZone.Add(entity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (entitiesInZone.Contains(other.GetComponent<BasicEntity>()))
        {
            entitiesInZone.Remove(other.GetComponent<BasicEntity>());
        }
    }
}
