using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] ContactFilter2D movementFilter;
    [SerializeField] float collisionOffset = 2f;

    Vector2 movementInput;
    private Rigidbody2D _rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private Vector2 lastPosition; // Track last position to detect whole steps

    //track last scene
    public int sceneBuildIndex;

    private void Awake()
    {
        ConditionsDB.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        lastPosition = _rb.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(movementInput != Vector2.zero)
        {

            //handle multi direction input and allow player to "slide" on colliders when able
            bool success = TryMove(movementInput);

            if(!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            if (IsWholeStepMove())
            {

            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = _rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime * collisionOffset);


        //if raycast returns false (0) allow movement
        if (count == 0)
        {
            _rb.MovePosition(_rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsWholeStepMove()
    {
        // Check if both X and Y movement are whole numbers
        return Mathf.Abs(_rb.position.x - lastPosition.x) >= 3.5f || Mathf.Abs(_rb.position.y - lastPosition.y) >= 3.5f;
    }



    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
