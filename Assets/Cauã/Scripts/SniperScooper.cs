using UnityEngine;
using UnityEngine.InputSystem;

public class SniperScooper : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float zoomFOV = 25f;
    [SerializeField] private float zoomSpeed = 10f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (cam == null)
            Debug.LogError("Não achei a MainCamera!");
    }

    void LateUpdate()
    {
        if (cam == null) return;

        bool mirando = Mouse.current.rightButton.isPressed;

        float alvo = mirando ? zoomFOV : normalFOV;

        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView,
            alvo,
            Time.deltaTime * zoomSpeed
        );
    }

    private void OnDisable()
    {
        if (cam != null)
            cam.fieldOfView = normalFOV;
    }

    private void OnDestroy()
    {
        if (cam != null)
            cam.fieldOfView = normalFOV;
    }
}