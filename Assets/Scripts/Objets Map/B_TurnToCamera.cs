using UnityEngine;

public class B_TurnToCamera : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        // Faire en sorte que l'objet regarde toujours la caméra principale
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0); // Inverser l'axe pour que la barre soit visible correctement
        }
    }
}
