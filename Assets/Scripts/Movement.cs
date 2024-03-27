using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 1;

    Inputs inputs;
    Vector3 moveDir;


    private void Awake()
    {
        inputs = new Inputs();

        inputs.General.Move.performed += ctx =>
        {
            moveDir = (Vector3)ctx.ReadValue<Vector2>();
        };
        inputs.General.Move.canceled += ctx => 
        {
            moveDir = Vector3.zero;
        };

        inputs.General.Enable();
    }

    private void OnDestroy()
    {
        inputs.Dispose();
    }

    private void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime * moveDir;
    }
}
