using UnityEngine;

public class B_LanceCanon : MonoBehaviour
{
    [SerializeField] private GameObject missile;
    [SerializeField] private Transform spawnTransform;

    private Camera _camera;
    [SerializeField] private LayerMask layerMask;
    private B_LevelManager levelManager;

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
                if (levelManager.GetIsGameStarted())
                {
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
}
