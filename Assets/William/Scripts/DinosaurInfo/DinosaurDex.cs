using UnityEngine;

[CreateAssetMenu(fileName = "DinosaurDex", menuName = "Scriptable Objects/DinosaurDex")]
public class DinosaurDex : ScriptableObject
{
    public DinosaurInfo[] info = new DinosaurInfo[5];

    public int predatorMin;
    public int predatorMax;

    public int armoredMin;
    public int armoredMax;

    public int agileMin;
    public int agileMax;

    public int predatorArmoredMin;
    public int predatorArmoredMax;

    public int predatorAgileMin;
    public int predatorAgileMax;

    public int armoredAgileMin;
    public int armoredAgileMax;

    public DinosaurInfo GetRandomPredator()
    {
        int temp = Random.Range(predatorMin, predatorMax);
        return info[temp];
    }

    public DinosaurInfo GetRandomArmored()
    {
        int temp = Random.Range(armoredMin, armoredMax);
        return info[temp];
    }

    public DinosaurInfo GetRandomAgile()
    {
        int temp = Random.Range(agileMin, agileMax);
        return info[temp];
    }

    public DinosaurInfo GetRandomPredatorArmored()
    {
        int temp = Random.Range(predatorArmoredMin, predatorArmoredMax);
        return info[temp];
    }

    public DinosaurInfo GetRandomPredatorAgile()
    {
        int temp = Random.Range(predatorAgileMin, predatorAgileMax);
        return info[temp];
    }

    public DinosaurInfo GetRandomArmoredAgile()
    {
        int temp = Random.Range(armoredAgileMin, armoredAgileMax);
        return info[temp];
    }
}
