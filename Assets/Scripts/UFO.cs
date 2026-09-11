using UnityEngine;
using UnityEngine.InputSystem;

public class UFO : MonoBehaviour
{
    public float move_speed = 5f;
    public GameObject fire;
    public float nextFire = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move_x = 0f;
        float move_z = 0;
        float fly = 0f;

        if(Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            move_x = 1f;
        if(Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            move_x = -1f;
        if(Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            move_z = -1f;
        if(Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            move_z = 1f;

        if(Keyboard.current.spaceKey.isPressed)
        {
            if(Time.time >= nextFire){
                Instantiate(fire, transform.position, Quaternion.identity);
                nextFire = Time.time + 2f;
                }
            fly = 3f;
        }

        Vector3 movement = new Vector3(move_x * move_speed, fly, move_z * move_speed);
        transform.position += movement * Time.deltaTime;

    }
}
