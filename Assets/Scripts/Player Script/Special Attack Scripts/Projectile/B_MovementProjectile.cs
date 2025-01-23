using UnityEngine;

public class B_MovementProjectile : MonoBehaviour
{
    // Le projectile monte jusqu'à être hors caméra, puis se téléporte au-dessus de sa cible et devient un missile à tête chercheuse. Ce comportement minimise le risque de collision avec d'autres unités que sa cible
    private GameObject target;
    private bool isRising = true;
    private float speed = 5f;
    private Rigidbody rb;
    private Renderer rd;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void setTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    private void Move()
    {
        rb.linearVelocity = speed * transform.forward;
        if (isRising)
        {
            transform.rotation *= Quaternion.FromToRotation(transform.forward, Vector3.up);
            if (!rd.isVisible)
            {
                isRising = false;
                transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            } else
            {
                transform.LookAt(target.transform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject==target)
        {
            target.GetComponent<B_Health>().halfHP();
            Destroy(this);
        }
    }
}
