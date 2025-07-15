using System;
using System.Collections;
using UnityEngine;

public class Fossil : BaseItem<FossilStat>
{
    [NonSerialized] public bool isColliding = false;
    
    public void GetFossil()
    {
        Debug.Log($"Hey, I got {statObject.itemName}!");
    }

    private void OnTriggerEnter2D()
    {
        isColliding = true;
    }

    private void OnTriggerExit2D()
    {
        isColliding = false;
    }

    public IEnumerator WaitForCollisions()
    {
        yield return new WaitForFixedUpdate();
    }
}
