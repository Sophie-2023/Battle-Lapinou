using UnityEngine;

public class B_MissileCanon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    private Rigidbody rigi;
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rigi.linearVelocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Obstacle")
        {
            Debug.Log("Missile détruit");
            Destroy(gameObject);
        }
    }

}
