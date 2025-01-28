using UnityEngine;

public class B_MovementProjectile : MonoBehaviour
{
    // Le projectile monte jusqu'� �tre hors cam�ra, puis se t�l�porte au-dessus de sa cible et devient un missile � t�te chercheuse. Ce comportement minimise le risque de collision avec d'autres unit�s que sa cible
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
