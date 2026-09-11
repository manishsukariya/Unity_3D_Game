using UnityEngine;

public class Fire : MonoBehaviour
{
    public float fall = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0f, -fall, 0f);
        transform.position += movement * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject, 2f);
        }
    }
}
