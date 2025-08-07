using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fossil : MonoBehaviour
{
    public FossilStat statObject;

    [Min(0.01f)]
    [SerializeField] private float boxCollisonEdgeWidth = 0.01f;
    
    //[SerializeField] private bool isDigOut = false;
    [NonSerialized] public bool isColliding = false;
    [NonSerialized] public FossilItem data;

    private SpriteRenderer spriteMesh;

    //[SerializeField] private Vector2[] cellPoints;
    private BoxCollider2D _fossilCollider;
    private List<Vector2> cellPoints;
    private Vector2 _colliderOrigin;

    private void Awake()
    {
        statObject.itemImage = GetComponentInChildren<SpriteRenderer>().sprite;

        spriteMesh = GetComponentInChildren<SpriteRenderer>();
        _fossilCollider = GetComponent<BoxCollider2D>();

        if (statObject.itemImage == null)
            statObject.itemImage = spriteMesh.sprite;

        //data = new FossilItem(statObject);

        _colliderOrigin = _fossilCollider.offset - _fossilCollider.size * 0.5f;
        cellPoints = RobinMathMethods.CellPoints((int)_fossilCollider.size.x, (int)_fossilCollider.size.y, _colliderOrigin);

        _fossilCollider.size = new Vector2(_fossilCollider.size.x - boxCollisonEdgeWidth, _fossilCollider.size.y - boxCollisonEdgeWidth);
    }

    private void Start()
    {
        /*Debug.Log($"{gameObject.name}'s offset: {_fossilOffset}");
        
        foreach (var cellpoint in cellPoints)
        {
            Debug.Log($"{gameObject.name}: {cellpoint}");
        }*/

        //OnBuying();
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
