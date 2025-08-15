using UnityEngine;

public class FossilToDino : Code.Scripts.Managers.Singleton<FossilToDino>
{
    [SerializeField] MoveDex moveDex;
    [SerializeField] DinosaurDex dinoDex;

    DinosaurInfo newDinoInfo;
    MoveInfo[] newMoveInfo = new MoveInfo[5];

    public InventoryDinosaur result;
    public InventoryDinosaur MakeDino(FossilStat[] fossil) {
        int baseStrength = 0;
        int baseDefense = 0;
        int baseAgility = 0;

        int predatorCount = 0;
        int armoredCount = 0;
        int agileCount = 0;

        for (int i = 0; i < 5; i++)
        {


            switch (fossil[i].fossilType)
            {
                case DinosaurType.Predator:
                    predatorCount++;
                    switch (fossil[i].rarity)
                    {
                        case RarityLevel.Common:
                            baseStrength += 3;
                            baseDefense += 1;
                            baseAgility += 2;
                            break;
                        case RarityLevel.Uncommon:
                            baseStrength += 4;
                            baseDefense += 1;
                            baseAgility += 3;
                            break;
                        case RarityLevel.Rare:
                            baseStrength += 5;
                            baseDefense += 2;
                            baseAgility += 3;
                            break;
                        case RarityLevel.Epic:
                            baseStrength += 6;
                            baseDefense += 2;
                            baseAgility += 4;
                            break;
                        case RarityLevel.Legendary:
                            baseStrength += 7;
                            baseDefense += 3;
                            baseAgility += 5;
                            break;
                    }
                    break;
                case DinosaurType.Armored:
                    armoredCount++;
                    switch (fossil[i].rarity)
                    {
                        case RarityLevel.Common:
                            baseStrength += 2;
                            baseDefense += 3;
                            baseAgility += 1;
                            break;
                        case RarityLevel.Uncommon:
                            baseStrength += 3;
                            baseDefense += 4;
                            baseAgility += 1;
                            break;
                        case RarityLevel.Rare:
                            baseStrength += 3;
                            baseDefense += 5;
                            baseAgility += 2;
                            break;
                        case RarityLevel.Epic:
                            baseStrength += 4;
                            baseDefense += 6;
                            baseAgility += 2;
                            break;
                        case RarityLevel.Legendary:
                            baseStrength += 5;
                            baseDefense += 7;
                            baseAgility += 3;
                            break;
                    }
                    break;
                case DinosaurType.Agile:
                    agileCount++;
                    switch (fossil[i].rarity)
                    {
                        case RarityLevel.Common:
                            baseStrength += 1;
                            baseDefense += 2;
                            baseAgility += 3;
                            break;
                        case RarityLevel.Uncommon:
                            baseStrength += 1;
                            baseDefense += 3;
                            baseAgility += 4;
                            break;
                        case RarityLevel.Rare:
                            baseStrength += 2;
                            baseDefense += 3;
                            baseAgility += 5;
                            break;
                        case RarityLevel.Epic:
                            baseStrength += 2;
                            baseDefense += 4;
                            baseAgility += 6;
                            break;
                        case RarityLevel.Legendary:
                            baseStrength += 3;
                            baseDefense += 5;
                            baseAgility += 7;
                            break;
                    }
                    break;

            }


        }

        if (predatorCount > 2)
        {
            newDinoInfo = dinoDex.GetRandomPredator();
        } else if (armoredCount > 2)
        {
            newDinoInfo = dinoDex.GetRandomArmored();
        } else if (agileCount > 2)
        {
            newDinoInfo = dinoDex.GetRandomAgile();
        } else if (predatorCount == armoredCount)
        {
            newDinoInfo = dinoDex.GetRandomPredatorArmored();
        } else if (predatorCount == agileCount)
        {
            newDinoInfo = dinoDex.GetRandomPredatorAgile();
        } else if (armoredCount == agileCount)
        { 
            newDinoInfo = dinoDex.GetRandomArmoredAgile();
        }

        


        int currentMove = 0;

        while (predatorCount > 0) {
            newMoveInfo[currentMove] = moveDex.GetRandomPredator();
            currentMove++;
            predatorCount--;
        }
        while (armoredCount > 0)
        {
            newMoveInfo[currentMove] = moveDex.GetRandomArmored();
            currentMove++;
            armoredCount--;
        }
        while (agileCount > 0)
        {
            newMoveInfo[currentMove] = moveDex.GetRandomAgile();
            currentMove++;
            agileCount--;
        }
        InventoryDinosaur resultCopy = Instantiate<InventoryDinosaur>(result, DinoHolder.Instance.transform);
        resultCopy.Initiate(baseStrength, baseDefense, baseAgility, newDinoInfo, newMoveInfo, 5, 0);
        return resultCopy;
    }
}
