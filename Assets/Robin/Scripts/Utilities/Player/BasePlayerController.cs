using UnityEngine;

public abstract class BasePlayerController : MonoBehaviour
{
    protected InputManager inputManager;

    protected virtual void Start()
    {
        inputManager = InputManager.Instance;
    }
}
