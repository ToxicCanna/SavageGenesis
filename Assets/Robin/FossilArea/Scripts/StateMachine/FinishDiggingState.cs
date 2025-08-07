using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDiggingState : BaseState
{
    private MiningStateMachine _miningStateMachine;

    public FinishDiggingState(MiningStateMachine stateMachine) 
    {
        _miningStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        _miningStateMachine.diggingIcon.SetActive(false);

        foreach (var fossil in _miningStateMachine.fossilDigOutList)
        {
            fossil.statObject.rarity = GetDugOutRarity();
            fossil.data = new FossilItem(fossil.statObject);

            PlayerInventory_Overworld.CollectItem(fossil.data.stat, 1);
            //Debug.Log($"Fossil rarity is {fossil.data.stat.rarity}");
        }

        PlayerInventory_Overworld.DebugDisplayAllItemsInInventory();
        _miningStateMachine.StartCoroutine(LeaveScene(1));
    }

    IEnumerator LeaveScene(int sceneID)
    {
        yield return new WaitForSeconds(_miningStateMachine.waitInSecAfterFinishDigging);
        PlayerDigging.durability = _miningStateMachine.uiManager.maxDurability;
        SceneManager.LoadScene(sceneID);
    }

    private RarityLevel GetDugOutRarity()
    {
        var lootChance = Random.Range(0, RarityValue.rarityValues.Sum());

        float cumulative = 0f;

        for (int i = 0; i < RarityValue.rarityValues.Length; i++)
        {
            cumulative += RarityValue.rarityValues[i];
            if (lootChance < cumulative)
            {
                return (RarityLevel)i;
            }
        }

        return 0;
    }

    #region Unused
    public override void UpdateState() { }
    public override void ExitState() { }
    #endregion
}
