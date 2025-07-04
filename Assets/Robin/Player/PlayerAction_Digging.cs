using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

//For now just testing how digging function work
public class Digging : MonoBehaviour
{
    [SerializeField] private DiggingArea diggingArea;
    [SerializeField] private DiggingToolType currentDiggingTool;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            diggingArea.OnDigging(currentDiggingTool);
        }
    }
}
