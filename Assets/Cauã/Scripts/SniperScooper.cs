using UnityEngine;
using UnityEngine.InputSystem;

public class SniperScooper : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float zoomFOV = 25f;
    [SerializeField] private float zoomSpeed = 10f;

    private Camera playerCamera;
    private bool isAiming;

    void Start()
    {
        playerCamera = Camera.main;

        if (playerCamera != null)
            normalFOV = playerCamera.fieldOfView;
    }

    void Update()
    {
        if (playerCamera == null) return;

        isAiming = Mouse.current.rightButton.isPressed;

        float targetFOV = isAiming ? zoomFOV : normalFOV;

        playerCamera.fieldOfView = Mathf.Lerp(
            playerCamera.fieldOfView,
            targetFOV,
            Time.deltaTime * zoomSpeed
        );
    }

    private void OnDisable()
    {
        if (playerCamera != null)
            playerCamera.fieldOfView = normalFOV;
    }
}
