using UnityEngine;

//For now just testing how digging function work
public class Digging : MonoBehaviour
{
    [SerializeField] private DiggingToolType currentDiggingTool;
    private IDigging _iDigging;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (_iDigging != null)
                _iDigging.OnDigging(currentDiggingTool);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _iDigging = collision.GetComponent<IDigging>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _iDigging = null;
    }
}
