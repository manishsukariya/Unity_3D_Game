using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 0.5f;
    public float detectionDistance = 2f;

    private Rigidbody rb;
    private bool carAheadClear = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckVehicleAhead();

        if (!carAheadClear)
            return;

        rb.MovePosition(
            rb.position +
            transform.forward * speed * Time.fixedDeltaTime
        );
    }

    void CheckVehicleAhead()
    {
        RaycastHit hit;

        Vector3 origin =
            transform.position + Vector3.up * 0.3f;

        if (Physics.Raycast(
            origin,
            transform.forward,
            out hit,
            detectionDistance
        ))
        {
            CubeMovement other =
                hit.collider.GetComponentInParent<CubeMovement>();

            if (other != null && other != this)
            {
                carAheadClear = false;
                return;
            }
        }

        carAheadClear = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 origin =
            transform.position + Vector3.up * 0.3f;

        Gizmos.DrawLine(
            origin,
            origin + transform.forward * detectionDistance
        );
    }
}