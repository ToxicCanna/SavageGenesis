using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fossil : MonoBehaviour
{
    public FossilStat statObject;

    [SerializeField] private float boxCollisonEdgeWidth = 0.1f;
    //[SerializeField] private bool isDigOut = false;

    [NonSerialized] public bool isColliding = false;
    [NonSerialized] public FossilItem data;

    private SpriteRenderer spriteMesh;

    //[SerializeField] private Vector2[] cellPoints;
    private List<Vector2> cellPoints;

    private void Awake()
    {
        statObject.itemImage = GetComponentInChildren<SpriteRenderer>().sprite;
        data = new FossilItem(statObject);

        spriteMesh = GetComponentInChildren<SpriteRenderer>();
        spriteMesh.transform.localPosition = GetComponent<BoxCollider2D>().offset;

        cellPoints = RobinMathMethods.CellPoints((int)transform.lossyScale.x, (int)transform.lossyScale.y);
    }

    #region Collision Detection
    //Check if dig out
    public bool CheckIfDugOut(DiggingLayer[] layers)
    {
        foreach(var cellpoint in cellPoints)
        {
            foreach (var layer in layers)
            {
                if (layer.IsNotDug((Vector2)transform.position + cellpoint))
                {
                    //Debug.Log($"{gameObject.name}: {cellpoint}");
                    //isDigOut = false;
                    return false;
                }
            }
        }
        //isDigOut = true;
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
