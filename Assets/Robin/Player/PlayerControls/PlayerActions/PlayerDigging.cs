using UnityEngine;
using UnityEngine.InputSystem;

//For now just testing how digging function work
public class PlayerDigging : BasePlayerController
{
    [SerializeField] private DiggingToolType currentDiggingTool;
    private IDiggingArea iDiggingArea;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    protected override void Update() 
    {
        if (inputManager.GetInteractInput())
            Dig();
    }

    public void Dig()
    {
        var hit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!hit.collider) return;

        Debug.Log(hit.collider.gameObject.name);

        iDiggingArea = hit.collider.gameObject.GetComponentInChildren<IDiggingArea>();
        iDiggingArea?.OnDigging(hit.point, currentDiggingTool);
        iDiggingArea?.FinishDigging();
    }
}
