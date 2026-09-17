using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 0.5f;
    public float detectionDistance = 2f;

    public float leftDX = 4f;
    public float leftDZ = 3f;

    public float rightDX = 6.33f;
    public float rightDZ = 5.33f;

    public float turnDuration = 2f;

    private Rigidbody rb;

    private bool carAheadClear = true;
    private bool trafficLightCanMove = true;
    private bool isTurning = false;

    private HashSet<int> usedTurnPoints = new HashSet<int>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isTurning)
            return;

        CheckVehicleAhead();

        if (!carAheadClear || !trafficLightCanMove)
            return;

        rb.MovePosition(
            rb.position + transform.forward * speed * Time.fixedDeltaTime
        );
    }

    void CheckVehicleAhead()
    {
        RaycastHit hit;

        Vector3 rayOrigin = transform.position + Vector3.up * 0.3f;

        if (Physics.Raycast(
            rayOrigin,
            transform.forward,
            out hit,
            detectionDistance
        ))
        {
            CubeMovement car = hit.collider.GetComponentInParent<CubeMovement>();

            if (car != null && car != this)
            {
                carAheadClear = false;
                return;
            }
        }

        carAheadClear = true;
    }

    public void SetTrafficLight(bool value)
    {
        if (!isTurning)
            trafficLightCanMove = value;
    }

    public void TurnAtPoint(TurnPoint turnPoint)
    {
        if (isTurning)
            return;

        int pointID = turnPoint.GetInstanceID();

        if (usedTurnPoints.Contains(pointID))
            return;

        usedTurnPoints.Add(pointID);

        int direction = Random.Range(0, 3);

        if (direction == 1)
            return;

        StartCoroutine(SmoothTurn(direction, turnPoint));
    }

    IEnumerator SmoothTurn(int direction, TurnPoint turnPoint)
    {
        isTurning = true;
        trafficLightCanMove = true;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        float yRotation = NormalizeAngle(transform.eulerAngles.y);

        Vector3 offset = Vector3.zero;
        float targetYRotation = yRotation;

        if (direction == 0)
        {
            if (Mathf.Abs(yRotation - 0f) < 1f)
            {
                offset = new Vector3(-leftDX, 0f, leftDZ);
                targetYRotation = 270f;
            }
            else if (Mathf.Abs(yRotation - 90f) < 1f)
            {
                offset = new Vector3(leftDX, 0f, leftDZ);
                targetYRotation = 0f;
            }
            else if (Mathf.Abs(yRotation - 180f) < 1f)
            {
                offset = new Vector3(leftDX, 0f, -leftDZ);
                targetYRotation = 90f;
            }
            else
            {
                offset = new Vector3(-leftDX, 0f, -leftDZ);
                targetYRotation = 180f;
            }
        }
        else
        {
            if (Mathf.Abs(yRotation - 0f) < 1f)
            {
                offset = new Vector3(rightDX, 0f, rightDZ);
                targetYRotation = 90f;
            }
            else if (Mathf.Abs(yRotation - 90f) < 1f)
            {
                offset = new Vector3(rightDX, 0f, -rightDZ);
                targetYRotation = 180f;
            }
            else if (Mathf.Abs(yRotation - 180f) < 1f)
            {
                offset = new Vector3(-rightDX, 0f, -rightDZ);
                targetYRotation = 270f;
            }
            else
            {
                offset = new Vector3(-rightDX, 0f, rightDZ);
                targetYRotation = 0f;
            }
        }

        Vector3 targetPosition = startPosition + offset;

        Vector3 controlPoint =
            startPosition +
            transform.forward * 2f +
            (targetPosition - startPosition) * 0.5f;

        float elapsed = 0f;

        while (elapsed < turnDuration)
        {
            elapsed += Time.fixedDeltaTime;

            float t = Mathf.Clamp01(elapsed / turnDuration);

            float smoothT = t * t * (3f - 2f * t);

            Vector3 point = CalculateBezier(
                startPosition,
                controlPoint,
                targetPosition,
                smoothT
            );

            Vector3 nextPoint = CalculateBezier(
                startPosition,
                controlPoint,
                targetPosition,
                Mathf.Clamp01(smoothT + 0.01f)
            );

            Vector3 directionVector = nextPoint - point;

            if (directionVector.sqrMagnitude > 0.001f)
            {
                Quaternion rotation = Quaternion.LookRotation(
                    directionVector.normalized,
                    Vector3.up
                );

                rb.MovePosition(point);
                rb.MoveRotation(rotation);
            }
            else
            {
                rb.MovePosition(point);
            }

            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(targetPosition);
        rb.MoveRotation(Quaternion.Euler(0f, targetYRotation, 0f));

        isTurning = false;
        carAheadClear = true;
        trafficLightCanMove = true;
    }

    Vector3 CalculateBezier(
        Vector3 start,
        Vector3 control,
        Vector3 end,
        float t
    )
    {
        float oneMinusT = 1f - t;

        return
            oneMinusT * oneMinusT * start +
            2f * oneMinusT * t * control +
            t * t * end;
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360f;

        if (angle < 0f)
            angle += 360f;

        return angle;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 rayOrigin =
            transform.position + Vector3.up * 0.3f;

        Gizmos.DrawLine(
            rayOrigin,
            rayOrigin + transform.forward * detectionDistance
        );
    }
}