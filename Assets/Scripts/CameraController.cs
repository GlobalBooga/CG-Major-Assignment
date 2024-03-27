using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform follow;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float maxDistance = 3f;

    Inputs inputs;
    Vector3 mouseWorldPos = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    Vector3 followPos;
    Vector3 targetPos;
    Vector3 smoothed;

    private void Awake()
    {
        inputs = new Inputs();

        inputs.Camera.Look.performed += ctx =>
        {
            mouseWorldPos = Camera.main.ScreenToWorldPoint((Vector3)ctx.ReadValue<Vector2>());
        };

        inputs.Camera.Enable();
    }

    private void OnDestroy()
    {
        inputs.Dispose();
    }

    private void Update()
    {
        followPos = follow.transform.position + Vector3.back * 10f;
        targetPos = Vector3.ClampMagnitude(mouseWorldPos - followPos, maxDistance);
        smoothed = Vector3.SmoothDamp(followPos, targetPos, ref velocity, smoothTime);
        transform.position = smoothed;
    }
}
