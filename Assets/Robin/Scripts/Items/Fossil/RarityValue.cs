public class RarityValue
{
    public static float[] rarityValues = { 50f, 30f, 15f, 4f, 1f};

    public static RarityLevel GetRarityLevelFromValue(int index)
    {
        switch (index)
        {
            case 0: return 0;  //Common  
            case 1: return (RarityLevel)1;  //Uncommon 
            case 2: return (RarityLevel)2;  //Rare 
            case 3: return (RarityLevel)3;  //Epic
            case 4: return (RarityLevel)4;  //Legendary
            default: return 0;
        }
    }

    /*public static float GetRarityValue(RarityLevel rarity)
    {
        switch (rarity)
        {
            case 0: return rarityValues[0];  //Common  
            case (RarityLevel)1: return rarityValues[1];  //Uncommon 
            case (RarityLevel)2: return rarityValues[2];  //Rare 
            case (RarityLevel)3: return rarityValues[3];  //Epic
            case (RarityLevel)4: return rarityValues[4];  //Legendary
            default: return 100f;
        }
    }*/
}
