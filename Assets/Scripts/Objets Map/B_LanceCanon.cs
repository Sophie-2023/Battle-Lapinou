using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class B_LanceCanon : MonoBehaviour
{
    [SerializeField] private GameObject missile;
    [SerializeField] private Transform spawnTransform;

    [SerializeField] private float rechargeTime;
    private bool canFire = true;

    private Camera _camera;
    [SerializeField] private LayerMask layerMask;
    private B_LevelManager levelManager;

    [Header("UI Elements")]
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI timeLeftText;

    [SerializeField] private Outline outline;

    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
                if (levelManager.GetIsGameStarted() && hit.collider.gameObject.transform.parent.gameObject == gameObject && canFire)
                {
                    StartCoroutine(RechargeTime());
                    Debug.Log("Canon touché !");
                    SpawnMissile();
                }
            }
        }
    }

    private void SpawnMissile()
    {
        GameObject newMissile = Instantiate(missile);
        newMissile.transform.position = spawnTransform.position;
        newMissile.transform.forward = spawnTransform.forward;
    }

    private IEnumerator RechargeTime()
    {
        canFire = false;
        outline.enabled = false;
        float timer = rechargeTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            if (fillImage != null)
            {
                fillImage.fillAmount = 1 - timer / rechargeTime;
            }

            if (timeLeftText != null)
            {
                timeLeftText.text = Mathf.Ceil(timer).ToString();
            }

            yield return null;
        }
        canFire = true;
        outline.enabled = true;
    }
}
