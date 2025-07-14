using UnityEngine;

public abstract class BasePlayerController : MonoBehaviour
{
    protected InputManager inputManager;

    protected abstract void Update();
    protected virtual void Start()
    {
        inputManager = InputManager.Instance;
    }
}
