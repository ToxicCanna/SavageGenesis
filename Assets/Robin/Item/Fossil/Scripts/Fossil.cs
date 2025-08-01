using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fossil : MonoBehaviour
{
    public FossilStat statObject;

    [SerializeField] private float boxCollisonEdgeWidth = 0.1f;
    
    [NonSerialized] public bool isColliding = false;
    //public bool isDigOut = false;
    [NonSerialized] public FossilItem data;

    private SpriteRenderer spriteMesh;

    [SerializeField] private Vector2[] cellPoints;

    private void Awake()
    {
        statObject.itemImage = GetComponentInChildren<SpriteRenderer>().sprite;
        data = new FossilItem(statObject);

        spriteMesh = GetComponentInChildren<SpriteRenderer>();
        spriteMesh.transform.localPosition = GetComponent<BoxCollider2D>().offset;
    }

    private void Start()
    {
        //cellPoints = new Vector2[(int)(transform.lossyScale.x * transform.lossyScale.y)];

        //Think a way to code the cellPoints array, math knowledge will define the rule 
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
                    //Debug.Log((Vector2)transform.position + cellpoint);
                    
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
