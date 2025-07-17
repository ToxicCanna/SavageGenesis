using System;
using System.Collections;
using UnityEngine;

public class Fossil : BaseItem<FossilStat>
{
    [NonSerialized] public bool isColliding = false;
    /*[NonSerialized]*/ public bool isDigOut = false;
    private SpriteRenderer spriteMesh;

    private void Awake()
    {
        spriteMesh = GetComponentInChildren<SpriteRenderer>();
        spriteMesh.transform.localPosition = GetComponent<BoxCollider2D>().offset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isColliding = true;
        if (other.gameObject.CompareTag("DiggingLayer"))
            isDigOut = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isColliding = false;
        if (other.gameObject.CompareTag("DiggingLayer"))
            isDigOut = true;
    }

    public IEnumerator WaitForCollisions()
    {
        yield return new WaitForFixedUpdate();
    }
}
