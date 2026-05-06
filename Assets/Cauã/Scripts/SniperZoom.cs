using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SniperZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
    [Header("Zoom")]
    [SerializeField] private float normalFOV = 40f;
    [SerializeField] private float zoomFOV = 18f;
    [SerializeField] private float zoomSpeed = 10f;

    private bool canZoom = false;

    void Update()
    {
        if (playerFollowCamera == null) return;

        float targetFOV = normalFOV;

        if (canZoom && Mouse.current.rightButton.isPressed)
        {
            targetFOV = zoomFOV;
        }

        var lens = playerFollowCamera.m_Lens;

        lens.FieldOfView = Mathf.Lerp(lens.FieldOfView, targetFOV, Time.deltaTime * zoomSpeed);

        playerFollowCamera.m_Lens = lens;
    }

    public void SetCanZoom(bool value)
    {
        canZoom = value;
    }
}
