using UnityEngine;

interface IDiggingArea
{
    void DigTile(float x, float y, DiggingToolType tool, out bool isDugOut);
}
