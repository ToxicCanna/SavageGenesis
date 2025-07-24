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
        if (InteractHits().Length <= 0) return;

        if (inputManager.GetInteractInput())
        {
            foreach (var hit in InteractHits())
            {
                //Debug.Log(hit.collider.gameObject.name);
                iDiggingArea = hit.collider.gameObject.GetComponentInChildren<IDiggingArea>();
                if (iDiggingArea != null)
                {
                    iDiggingArea.OnDigging(collision, currentDiggingTool);
                    //break;
                }
            }
        }  
    }

    private RaycastHit2D[] InteractHits()
    {
        return Physics2D.GetRayIntersectionAll(_camera.ScreenPointToRay(inputManager.GetInteractPosition()));;
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
