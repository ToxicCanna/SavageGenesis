using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class DiggingArea : MonoBehaviour, IDigging
{
    [SerializeField] private Fossil[] fossilList;

    private List<Fossil> _fossilList = new List<Fossil>();   //Keep track of fossils that can be possibly dug out

    public void OnDigging(DiggingToolType tool)
    {
        HandleDigging();

        if (_fossilList != null)
        {
            Debug.Log($"Hey, I found a {GetDigOutFossil()}!");
        }
        else
            Debug.Log("WTF?! Nothing is there...");
    }

    private Fossil GetDigOutFossil()
    {
        return _fossilList[Random.Range(0, _fossilList.Count)];
    }

    private void HandleDigging()
    {
        foreach (Fossil fossil in fossilList)
        {
            _fossilList.Add(fossil);
        }
    }
}
