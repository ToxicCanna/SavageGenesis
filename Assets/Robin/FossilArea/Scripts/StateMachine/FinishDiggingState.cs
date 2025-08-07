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
            fossil.StatObject.rarity = GetDugOutRarity();
            Debug.Log($"Fossil rarity is {fossil.StatObject.rarity}");
            PlayerInventory_Overworld<FossilStat>.CollectItem(fossil.StatObject, 1);
        }

        PlayerInventory_Overworld<FossilStat>.DebugDisplayAllItemsInInventory();
        _miningStateMachine.StartCoroutine(BackToOverworldScene(0));
    }

    IEnumerator BackToOverworldScene(int sceneIndex)
    {
        yield return new WaitForSeconds(_miningStateMachine.waitInSecAfterFinishDigging);
        PlayerDigging.durability = _miningStateMachine.uiManager.maxDurability;
        SceneManager.LoadScene(sceneIndex);
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
