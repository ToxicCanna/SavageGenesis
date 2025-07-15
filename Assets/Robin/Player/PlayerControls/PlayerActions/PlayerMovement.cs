using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : BasePlayerController
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 1.0f;
    private Vector2 movement;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = inputManager.GetPlayerMovement();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
