public class RarityValue
{
    public static float GetRarityValue(RarityLevel rarity)
    {
        switch (rarity)
        {
            case 0: return 100f;  //Common  
            case (RarityLevel)1: return 70f;  //Uncommon 
            case (RarityLevel)2: return 40f;  //Rare 
            case (RarityLevel)3: return 10f;  //Epic
            case (RarityLevel)4: return 2.5f;  //Legendary
            default: return 100f;
        }
    }
}
