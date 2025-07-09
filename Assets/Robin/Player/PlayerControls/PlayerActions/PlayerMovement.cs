using UnityEngine;

public class PlayerMovement : BasePlayerController
{
    [SerializeField] private float moveSpeed = 1.0f;
    private Vector2 movement;

    public override void Update()
    {
        movement = inputManager.GetPlayerMovement();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
