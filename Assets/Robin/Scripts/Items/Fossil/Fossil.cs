using System;
using System.Collections;
using UnityEngine;

public class Fossil : BaseItem<FossilStat>
{
    [NonSerialized] public bool isColliding = false;
    private SpriteRenderer spriteMesh;

    private void Awake()
    {
        spriteMesh = GetComponentInChildren<SpriteRenderer>();
        spriteMesh.transform.localPosition = GetComponent<BoxCollider2D>().offset;
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
