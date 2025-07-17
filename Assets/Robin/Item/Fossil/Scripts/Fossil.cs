using System;
using System.Collections;
using UnityEngine;

public class Fossil : BaseItem<FossilStat>
{
    [SerializeField] private float boxCollisonEdgeWidth = 0.1f;
    
    [NonSerialized] public bool isColliding = false;
    /*[NonSerialized]*/ public bool isDigOut = true;
    private SpriteRenderer spriteMesh;
    private Vector2 boxCenter;
    private Vector2 boxSize;

    private void Awake()
    {
        spriteMesh = GetComponentInChildren<SpriteRenderer>();
        spriteMesh.transform.localPosition = GetComponent<BoxCollider2D>().offset;
    }

    #region Collision Detection
    //Check if dig out
    private void Update()
    {
        isDigOut = CheckDigOut();
    }

    private bool CheckDigOut()
    {
        // Define the box's center position
        boxCenter = new Vector2
        (
            transform.position.x + spriteMesh.transform.localPosition.x * transform.localScale.x,
            transform.position.y + spriteMesh.transform.localPosition.y * transform.localScale.y
        );

        boxSize = new Vector2
        (
            transform.localScale.x - boxCollisonEdgeWidth,
            transform.localScale.y - boxCollisonEdgeWidth
        );

        // Perform the OverlapBoxAll check
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f);

        // Process the detected colliders
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.GetComponent<DiggingLayer>())
                return false;
        }

        return true;
    }

    //Check if can be spawned
    private void OnTriggerEnter2D(Collider2D other)
    {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isColliding = false;
    }

    public IEnumerator WaitForCollisions()
    {
        yield return new WaitForFixedUpdate();
    }
    #endregion
}
