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

    public int armoredPredatorMin;
    public int armoredPredatorMax;

    public int armoredAgileMin;
    public int armoredAgileMax;

    public int agilePredatorMin;
    public int agilePredatorMax;

    public int agileArmoredMin;
    public int agileArmoredMax;

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

    public DinosaurInfo GetRandomArmoredPredator()
    {
        int temp = Random.Range(armoredPredatorMin, armoredPredatorMax);
        return info[temp];
    }
    public DinosaurInfo GetRandomArmoredAgile()
    {
        int temp = Random.Range(armoredAgileMin, armoredAgileMax);
        return info[temp];
    }

    public DinosaurInfo GetRandomAgilePredator()
    {
        int temp = Random.Range(agilePredatorMin, agilePredatorMax);
        return info[temp];
    }

    public DinosaurInfo GetRandomAgileArmored()
    {
        int temp = Random.Range(agileArmoredMin, agileArmoredMax);
        return info[temp];
    }

}
