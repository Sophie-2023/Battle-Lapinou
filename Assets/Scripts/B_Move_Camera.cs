using UnityEngine;

public class B_Move_Camera : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 0.1f;
    [SerializeField] private float rotationSpeed = 400f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * cameraSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * cameraSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * cameraSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.forward * cameraSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.back * cameraSpeed);
        }

        if (Input.GetMouseButton(1))
        {
            // Rotation avec la souris
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;


            // Appliquer la rotation
            transform.Rotate(0, mouseX, 0); // Rotation horizontale
            transform.Rotate(-mouseY, 0, 0); // Rotation verticale

            // Forcer la composante Z de la rotation à 0
            Vector3 eulerRotation = transform.eulerAngles;
            eulerRotation.z = 0; // Remet l'angle Z à zéro
            transform.eulerAngles = eulerRotation;
        }
    }


}
