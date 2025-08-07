using UnityEngine;

[CreateAssetMenu(fileName = "MoveDex", menuName = "Scriptable Objects/MoveDex")]
public class MoveDex : ScriptableObject
{
    public MoveInfo[] info = new MoveInfo[15];

    public int predatorMin;
    public int predatorMax;

    public int armoredMin;
    public int armoredMax;

    public int agileMin;
    public int agileMax;

    public MoveInfo GetRandomPredator()
    {
        int temp = Random.Range(predatorMin, predatorMax);
        return info[temp];
    }

    public MoveInfo GetRandomArmored()
    {
        int temp = Random.Range(armoredMin, armoredMax);
        return info[temp];
    }

    public MoveInfo GetRandomAgile()
    {
        int temp = Random.Range(agileMin, agileMax);
        return info[agileMin];
    }
}
