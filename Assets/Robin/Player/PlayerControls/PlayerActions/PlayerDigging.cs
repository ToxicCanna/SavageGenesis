using UnityEngine;

//For now just testing how digging function work
public class PlayerDigging : BasePlayerController
{
    public DiggingToolType currentDiggingTool;
    private IDiggingArea iDiggingArea;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    public void Dig()
    {
        if (inputManager.GetInteractInput())
        {
            var hit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(inputManager.GetInteractPosition()));
            if (!hit.collider) return;

            Debug.Log(hit.collider.gameObject.name);

            iDiggingArea = hit.collider.gameObject.GetComponentInChildren<IDiggingArea>();
            iDiggingArea?.OnDigging(hit.point, currentDiggingTool);
        }  
    }

    #region Switch Tool
    public void SwitchToBrush()
    {
        currentDiggingTool = DiggingToolType.Brush;
    }

    public void SwitchToPickaxe()
    {
        currentDiggingTool = DiggingToolType.Pickaxe;
    }
    #endregion
}
