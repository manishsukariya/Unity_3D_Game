using System.ComponentModel.Design;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float fall = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}
