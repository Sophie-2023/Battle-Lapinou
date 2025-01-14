using UnityEngine;

public class B_Layer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Character");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
