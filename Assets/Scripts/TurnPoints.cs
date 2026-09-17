using UnityEngine;

public class TurnPoint : MonoBehaviour
{
    public bool isSide1;
    public bool isSide2;
    public bool isSide3;
    public bool isSide4;

    public float greenTime = 10f;

    private static int greenSide = 1;
    private static float timer = 0f;
    private static TurnPoint controller;

    void Awake()
    {
        if (controller == null)
        {
            controller = this;
            greenSide = 1;
            timer = 0f;
        }
    }

    void Update()
    {
        if (controller != this)
            return;

        timer += Time.deltaTime;

        if (timer >= greenTime)
        {
            greenSide++;

            if (greenSide > 4)
                greenSide = 1;

            timer = 0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        CubeMovement car =
            other.GetComponentInParent<CubeMovement>();

        if (car == null)
            return;

        bool isGreen = IsThisSideGreen();

        car.SetTrafficLight(isGreen);

        if (isGreen)
        {
            car.TurnAtPoint(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CubeMovement car =
            other.GetComponentInParent<CubeMovement>();

        if (car == null)
            return;

        car.SetTrafficLight(true);
    }

    bool IsThisSideGreen()
    {
        if (isSide1 && greenSide == 1)
            return true;

        if (isSide2 && greenSide == 2)
            return true;

        if (isSide3 && greenSide == 3)
            return true;

        if (isSide4 && greenSide == 4)
            return true;

        return false;
    }
}