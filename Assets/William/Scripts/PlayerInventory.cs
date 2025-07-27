using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InventoryDinosaur slotOneDino; //starting slots, this is the setup before we enter combat. saved for when exiting combat.
    [SerializeField] private InventoryDinosaur slotTwoDino;
    [SerializeField] private InventoryDinosaur slotThreeDino;
    [SerializeField] private InventoryDinosaur slotFourDino;
    [SerializeField] private InventoryDinosaur tempSlotDino;

    [SerializeField] private InventoryDinosaur combatSlotOne;   //how it looks during combat, slot one( and maybe two) are active on the screen and three and four are seeable inside switch
    [SerializeField] private InventoryDinosaur combatSlotTwo;
    [SerializeField] private InventoryDinosaur combatSlotThree;
    [SerializeField] private InventoryDinosaur combatSlotFour;


    void Start()
    {
        combatSlotOne = slotOneDino;
        combatSlotTwo = slotTwoDino;
        combatSlotThree = slotThreeDino;
        combatSlotFour = slotFourDino;
        GameManager.Instance.loadingCount++;
    }

    private void SwapSlots(InventoryDinosaur slotFrom, InventoryDinosaur slotTo)    // only between regular slots off of combat. 
    {
        if (!slotFrom.IsEmpty())
        {
            tempSlotDino = slotTo;
            slotTo = slotFrom;
            slotFrom = tempSlotDino;
        }
        
    }

    private void SwapCombatSlots(DinosaurSlot slotFrom, DinosaurSlot slotTo) //only be done between combatSlots during combat
    {
        if (slotFrom == slotTo )
        {
            return;
        }
        if (slotFrom == DinosaurSlot.One && slotTo == DinosaurSlot.Two)
        {
            if ((GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVOne) || (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVTwo))
            {
                return;
            }
            tempSlotDino = combatSlotOne;
            combatSlotOne = combatSlotTwo;
            combatSlotTwo = tempSlotDino;
        }
        else if (slotFrom == DinosaurSlot.One && slotTo == DinosaurSlot.Three)
        {
            tempSlotDino = combatSlotOne;
            combatSlotOne = combatSlotThree;
            combatSlotThree = tempSlotDino;
        }
        else if (slotFrom == DinosaurSlot.One && slotTo == DinosaurSlot.Four)
        {
            tempSlotDino = combatSlotOne;
            combatSlotOne = combatSlotFour;
            combatSlotFour = tempSlotDino;
        }
        else if (slotFrom == DinosaurSlot.Two)
        {
            if ((GameManager.Instance.GetCurrentGameMode() == GameMode.OneVOne) || (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVTwo))
            {
                return;
            }

            if (slotTo == DinosaurSlot.Three)
            {
                tempSlotDino = combatSlotTwo;
                combatSlotTwo = combatSlotThree;
                combatSlotThree = tempSlotDino;
            }
            else if (slotTo == DinosaurSlot.Four)
            {
                tempSlotDino = combatSlotTwo;
                combatSlotTwo = combatSlotFour;
                combatSlotFour = tempSlotDino;
            }
        }
    }

    private InventoryDinosaur LoadSlotOne()
    { 
        return slotOneDino;
    }

    private InventoryDinosaur LoadSlotTwo()
    {
        return slotTwoDino;
    }

    private InventoryDinosaur LoadSlotThree()
    {
        return slotThreeDino;
    }

    private InventoryDinosaur LoadSlotFour()
    {
        return slotFourDino;
    }

    public InventoryDinosaur LoadCombatSlotOne()
    {
        return combatSlotOne;
    }

    public InventoryDinosaur LoadCombatSlotTwo()
    {
        return combatSlotTwo;
    }
}
