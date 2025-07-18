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
    public void Dig(CircleCollider2D collision)
    {
        if (InteractHitCollider() == null) return;

        if (inputManager.GetInteractInput())
        {
            //Debug.Log(hit.collider.gameObject.name);
            iDiggingArea = InteractHitCollider().gameObject.GetComponentInChildren<IDiggingArea>();
            iDiggingArea?.OnDigging(collision, currentDiggingTool);
        }  
    }

    private Collider2D InteractHitCollider()
    {
        var hit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(inputManager.GetInteractPosition()));
        return hit.collider;
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
