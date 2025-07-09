using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BasePlayerController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected InputManager inputManager;

    public abstract void Update();
    public virtual void Start()
    {
        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody2D>();
    }
}
