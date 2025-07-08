using UnityEngine;

//For now just testing how digging function work
public class Digging : MonoBehaviour
{
    [SerializeField] private DiggingToolType currentDiggingTool;
    private IDiggingArea iDiggingArea;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
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
