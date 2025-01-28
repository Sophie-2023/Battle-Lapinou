using UnityEngine;

public class B_AlignUIWithCamera : MonoBehaviour
{
    //Aligne les barres de vie et de mana vers la caméra
    private Camera camera;
    [SerializeField] private float hauteurUI = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,hauteurUI,transform.position.z);

        transform.LookAt(camera.transform.position, Vector3.up);
    }
}
