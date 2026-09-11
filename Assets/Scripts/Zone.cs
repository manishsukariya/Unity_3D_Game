using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public bool left;
    public bool right;
    public bool forward;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        void OnTriggerEnter(Collider other)
        {
        if (!other.CompareTag("Car"))
        {
            List<int> dir = new List<int>();

            if(left)
                dir.Add(-1);
            if(right)
                dir.Add(1);
            if(forward)
                dir.Add(0);

            Rigidbody rb = other.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.MoveRotation(Quaternion.Euler(0, dir[Random.Range(0, dir.Count)]*90, 0));
            }
        }
        }
}
