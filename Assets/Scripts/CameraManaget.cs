using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public Camera normalCamera;
    public Camera ufoCamera;

    public float normalFOV = 60f;
    public float zoomFOV = 10f;
    public float zoomSpeed = 5f;

    void Start()
    {
        normalCamera.gameObject.SetActive(true);
        ufoCamera.gameObject.SetActive(false);

        normalCamera.fieldOfView = normalFOV;
        ufoCamera.fieldOfView = normalFOV;
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            normalCamera.gameObject.SetActive(true);
            ufoCamera.gameObject.SetActive(false);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            normalCamera.gameObject.SetActive(false);
            ufoCamera.gameObject.SetActive(true);
        }

        if (ufoCamera.gameObject.activeSelf)
        {
            float targetFOV = Keyboard.current.shiftKey.isPressed
                ? zoomFOV
                : normalFOV;

            ufoCamera.fieldOfView = Mathf.Lerp(
                ufoCamera.fieldOfView,
                targetFOV,
                zoomSpeed * Time.deltaTime
            );
        }
    }
}