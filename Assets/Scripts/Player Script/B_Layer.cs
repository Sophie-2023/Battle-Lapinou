using UnityEngine;

public class B_Layer : MonoBehaviour
{
    //Change le layer des unités à Character
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
