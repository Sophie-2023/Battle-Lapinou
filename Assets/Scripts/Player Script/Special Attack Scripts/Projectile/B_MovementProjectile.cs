using UnityEngine;

public class B_MovementProjectile : MonoBehaviour
{
    // Le projectile monte jusqu'à être hors caméra, puis se téléporte au-dessus de sa cible et devient un missile à tête chercheuse. Ce comportement minimise le risque de collision avec d'autres unités que sa cible
    private GameObject target=null;
    private bool isRising = true;
    private float speed = 5f;
    private Rigidbody rb;
    private Collider col;
    Camera cam;
    private float maxheight = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        cam = Camera.main;
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
            transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.up);
            if ( (!seenByCamera()) && (transform.position.y >= maxheight) )
            {
                isRising = false;
                if (target != null)
                {
                    transform.position = new Vector3(target.transform.position.x, maxheight, target.transform.position.z);
                }
                else
                {
                    Destroy(this.gameObject);
                }
            } 
        }
        else
        {
            if (target!=null)
            {
                Vector3 toTarget = target.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(toTarget, Vector3.up);
            } else
            {
                Destroy(this.gameObject);
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject==target)
        {
            
            target.GetComponent<B_Health>().halfHP();
            Destroy(this.gameObject);
        }
    }

    private bool seenByCamera()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, col.bounds);
    }
}
