using System.Collections.Generic;
using UnityEngine;

public class GetChildrenObjects
{
    public static List<GameObject> GetAllChildren(GameObject parent)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in parent.transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }
}

public class RobinMathMethods
{
    public static List<Vector2> CellPoints(int x, int y)
    {
        var vector2 = new List<Vector2>();
        
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                vector2.Add(new Vector2(i + 0.5f, j + 0.5f));
            }
        }

        return vector2;
    } 
}
