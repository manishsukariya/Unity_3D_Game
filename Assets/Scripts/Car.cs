using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 0.5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(
            rb.position + transform.forward * speed * Time.fixedDeltaTime
        );
    }
}