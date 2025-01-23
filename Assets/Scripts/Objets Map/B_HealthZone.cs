using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_HealthZone : MonoBehaviour
{
    [SerializeField] private ParticleSystem healthParticleSystem;

    private Camera _camera;
    [SerializeField] private LayerMask layerMask;
    private B_LevelManager levelManager;

    private bool canHeal = true;
    private float rechargeTime = 5;
    [SerializeField] private Outline outline;

    [SerializeField] private List<BasicEntity> entitiesInHealthZone = new List<BasicEntity>();
    [SerializeField] private int healAmount;

    void Start()
    {
        _camera = Camera.main;
        levelManager = B_LevelManager.Instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
            {
                if (levelManager.GetIsGameStarted() && hit.collider.gameObject == gameObject && canHeal)
                {
                    healthParticleSystem.Play();
                    StartCoroutine(RechargeTime());
                    HealEntities();
                }
            }
        }
    }

    private IEnumerator RechargeTime()
    {
        canHeal = false;
        outline.enabled = false;
        yield return new WaitForSeconds(rechargeTime);
        canHeal = true;
        outline.enabled = true;
    }

    // Guérit toutes les entités (alliées ou ennemies) de la zone
    private void HealEntities()
    {
        foreach (BasicEntity entity in entitiesInHealthZone)
        {
            entity.SetHP(entity.GetHP() + healAmount);
            if (entity.GetHP()> entity.GetMaxHP())
            {
                entity.SetHP(entity.GetMaxHP());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BasicEntity>(out BasicEntity entity)) 
        { 
            entitiesInHealthZone.Add(entity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (entitiesInHealthZone.Contains(other.GetComponent<BasicEntity>()))
        {
            entitiesInHealthZone.Remove(other.GetComponent<BasicEntity>());
        }
    }
}
