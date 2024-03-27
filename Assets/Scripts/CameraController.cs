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

    Vector3 targetPos;
    Vector3 smoothed;
    Vector3 followVelocity;
    Vector3 lastFollowPos;

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
        followVelocity = follow.position - lastFollowPos;
        targetPos = follow.position + Vector3.ClampMagnitude(mouseWorldPos - follow.position, maxDistance) + (Vector3.back * 10f);
        smoothed = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime );
        transform.position = followVelocity + smoothed;
    }
    private void LateUpdate()
    {
        lastFollowPos = follow.position;
    }
}
