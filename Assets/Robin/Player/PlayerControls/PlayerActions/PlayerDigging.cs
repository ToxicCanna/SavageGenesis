using UnityEngine;

//For now just testing how digging function work
public class PlayerDigging : BasePlayerController
{
    [SerializeField] private DiggingToolType currentDiggingTool;
    private IDiggingArea iDiggingArea;

    public override void Update()
    {
        if (inputManager.GetInteraction())
        {
            if (iDiggingArea != null)
            {
                iDiggingArea.OnDigging(currentDiggingTool);
                iDiggingArea.FinishDigging();
            } 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        iDiggingArea = collision.GetComponent<IDiggingArea>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        iDiggingArea = null;
    }
}
