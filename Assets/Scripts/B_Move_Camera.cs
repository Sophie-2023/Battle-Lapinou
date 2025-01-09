using UnityEngine;

public class B_Move_Camera : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Translate(Vector3.forward*cameraSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * cameraSpeed);
        }
    }
}
