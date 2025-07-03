using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState { Start, ActionSelect, SkillSelection, RunningTurn, Busy, BattleOver}

public enum BattleAction { Skill, UseItem, Run }


public class BattleSystem : MonoBehaviour
{
    [Header("Battle Data")]
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleDialogBox dialogBox;

    //Serialize the player unit
    [SerializeField]
    [CustomLabel("Player")]
    Enemy player;


    public event Action<bool> OnBattleOver;

    BattleState state;
    int currentAction;
    int currentSkill;

    Enemy localEnemy;

    bool isBossBattle = false; // this is for boss fight implementation

    int escapeAttempts;

    void Start()
    {
        var localEnemy = FindObjectOfType<AreaEnemies>().GetComponent<AreaEnemies>().GetRandomLocalEnemy();

        StartBattle(player, localEnemy);

        GameObject bossMarker = GameObject.FindWithTag("BossBattle");
        if (bossMarker != null)
        {
            isBossBattle = true;
        }
    }

    private void StartBattle(Enemy player, Enemy localEnemy)
    {
        this.player = player;
        player.Init();
        this.localEnemy = localEnemy;
        StartCoroutine(SetupBattle());
        OnBattleOver += EndBattle;
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup(player);
        enemyUnit.Setup(localEnemy);
        
        dialogBox.SetSkillNames(playerUnit.Enemy.Skills);


        yield return dialogBox.TypeDialog($"A dangerous {enemyUnit.Enemy.Base.Name} appeared!");

        escapeAttempts = 0;

        ActionSelect();
    }

    void BattleOver(bool won)
    {
        state = BattleState.BattleOver;
        localEnemy.OnBattleOver();
        OnBattleOver(won);
    }

    void ActionSelect()
    {
        state = BattleState.ActionSelect;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
    }

    void SkillSelect()
    {
        state = BattleState.SkillSelection;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableSkillSelector(true);
    }

    IEnumerator RunTurns(BattleAction playerAction)
    {
        state = BattleState.RunningTurn;

        if (playerAction == BattleAction.Skill)
        {
            playerUnit.Enemy.CurrentSkill = playerUnit.Enemy.Skills[currentSkill];
            enemyUnit.Enemy.CurrentSkill = enemyUnit.Enemy.GetRandomSkill();

            int playerSkillPriority = playerUnit.Enemy.CurrentSkill.Base.Priority;
            int enemySkillPriority = enemyUnit.Enemy.CurrentSkill.Base.Priority;

            //who moves first
            bool playerGoesFirst = true;
            if (enemySkillPriority > playerSkillPriority)
            {
                playerGoesFirst = false;
            }
            else if (enemySkillPriority == playerSkillPriority)
            {
                playerGoesFirst = playerUnit.Enemy.Dexterity >= enemyUnit.Enemy.Dexterity;
            }

            var firstUnit = (playerGoesFirst) ? playerUnit : enemyUnit;
            var secondUnit = (playerGoesFirst) ? enemyUnit : playerUnit;

            var secondEnemy = secondUnit.Enemy;

            //First turn
            yield return RunSkill(firstUnit, secondUnit, firstUnit.Enemy.CurrentSkill);
            yield return RunAfterTurn(firstUnit);
            if (state == BattleState.BattleOver) yield break;

            if (secondEnemy.HP > 0)
            {
                //second turn
                yield return RunSkill(secondUnit, firstUnit, secondUnit.Enemy.CurrentSkill);
                yield return RunAfterTurn(secondUnit);
                if (state == BattleState.BattleOver) yield break;
            }

            if (state != BattleState.BattleOver)
            {
                ActionSelect();
            }

        }
        else
        {
            if (playerAction == BattleAction.UseItem)
            {
                yield return HealPlayerUnit();
                yield return ShowStatusChanges(playerUnit.Enemy);
                yield return playerUnit.Hud.UpdateHP();

            }

            else if (playerAction == BattleAction.Run)
            {
                yield return TryToRun();
            }

            var enemySkill = enemyUnit.Enemy.GetRandomSkill();
            yield return RunSkill(enemyUnit, playerUnit, enemySkill);
            yield return RunAfterTurn(enemyUnit);
            if (state == BattleState.BattleOver) yield break;

            ActionSelect();
        }
    }

    IEnumerator HealPlayerUnit()
    {
        int healAmount = playerUnit.Enemy.MaxHp / 2;
        playerUnit.Enemy.HealHP(healAmount);
        yield return dialogBox.TypeDialog($"{playerUnit.Enemy.Base.Name} healed for {healAmount} HP.");
    }

    IEnumerator RunSkill(BattleUnit sourceUnit, BattleUnit targetUnit, Skill skill)
    {
        bool canRunSkill = sourceUnit.Enemy.OnBeforeSkill();

        if (!canRunSkill)
        {
            yield return ShowStatusChanges(sourceUnit.Enemy);
            yield return sourceUnit.Hud.UpdateHP();
            yield break;
        }
        yield return ShowStatusChanges(sourceUnit.Enemy);

        skill.Mana--;
        yield return dialogBox.TypeDialog($"{sourceUnit.Enemy.Base.Name} used {skill.Base.Name}!");

        if (CheckIfSkillHits(skill, sourceUnit.Enemy, targetUnit.Enemy))
        {

            if (skill.Base.Category == SkillCategory.Status)
            {
                yield return RunSkillEffects(skill.Base.Effects, sourceUnit.Enemy, targetUnit.Enemy, skill.Base.Target);
            }
            else
            {
                var damageRecap = targetUnit.Enemy.TakeDamage(skill, sourceUnit.Enemy);
                yield return targetUnit.Hud.UpdateHP();
                yield return ShowDamageRecap(damageRecap);
            }

            if (skill.Base.Secondaries != null && skill.Base.Secondaries.Count > 0 && targetUnit.Enemy.HP > 0)
            {
                foreach (var secondary in skill.Base.Secondaries)
                {
                    var rnd = UnityEngine.Random.Range(1, 101);
                    if (rnd <= secondary.Chance)
                    {
                        yield return RunSkillEffects(secondary, sourceUnit.Enemy, targetUnit.Enemy, secondary.Target);
                    }
                }
            }

            if (targetUnit.Enemy.HP <= 0)
            {
                yield return HandleEnemyDead(targetUnit);
            }
        }
        else
        {
            yield return dialogBox.TypeDialog($"The {sourceUnit.Enemy.Base.Name} missed!");
        }
    }
    IEnumerator RunSkillEffects(SkillEffects effects, Enemy source, Enemy target, SkillTarget skillTarget)
    {
        //stat boost
        if (effects.Boosts != null)
        {
            if (skillTarget == SkillTarget.Self)
            {
                source.ApplyBoosts(effects.Boosts);
            }
            else
            {
                target.ApplyBoosts(effects.Boosts);
            }
        }

        //Status effect
        if (effects.Status != ConditionID.none)
        {
            target.SetStatus(effects.Status);
        }

        //Volatile Status effect
        if (effects.VolatileStatus != ConditionID.none)
        {
            target.SetVolatileStatus(effects.VolatileStatus);
        }

        yield return ShowStatusChanges(source);
        yield return ShowStatusChanges(target);
    }

    IEnumerator RunAfterTurn(BattleUnit sourceUnit)
    {
        if (state == BattleState.BattleOver) yield break;
        yield return new WaitUntil(() => state == BattleState.RunningTurn);

        //Some statuses hurt the target after the turn
        sourceUnit.Enemy.OnAfterTurn();
        yield return ShowStatusChanges(sourceUnit.Enemy);
        yield return sourceUnit.Hud.UpdateHP();
        if (sourceUnit.Enemy.HP <= 0)
        {
            yield return HandleEnemyDead(sourceUnit);
            yield return new WaitUntil(() => state == BattleState.RunningTurn);
        }
    }

    bool CheckIfSkillHits(Skill skill, Enemy source, Enemy target)
    {
        if (skill.Base.AlwaysHits)
        {
            return true;
        }

        float skillAccuracy = skill.Base.Accuracy;

        int accuracy = source.StatBoosts[Stat.Accuracy];
        int evasion = target.StatBoosts[Stat.Evasion];

        var boostValues = new float[] { 1f, 4f / 3f, 5f / 3f, 2f, 7f / 3f, 8f / 3f, 3f };

        if (accuracy > 0)
        {
            skillAccuracy *= boostValues[accuracy];
        }
        else
        {
            skillAccuracy /= boostValues[-accuracy];
        }

        if (evasion > 0)
        {
            skillAccuracy /= boostValues[evasion];
        }
        else
        {
            skillAccuracy *= boostValues[-evasion];
        }

        return UnityEngine.Random.Range(1, 101) <= skillAccuracy;
    }

    IEnumerator ShowStatusChanges(Enemy enemy)
    {
        while (enemy.StatusChanges.Count > 0)
        {
            var message = enemy.StatusChanges.Dequeue();
            yield return dialogBox.TypeDialog(message);
        }
    }

    IEnumerator HandleEnemyDead(BattleUnit deadUnit)
    {
        yield return dialogBox.TypeDialog($"{deadUnit.Enemy.Base.Name} Died!");
        yield return new WaitForSeconds(2f);

        if (!deadUnit.IsPlayer)
        {
            //exp gain
            int expYield = deadUnit.Enemy.Base.ExpYield;
            int enemyLevel = deadUnit.Enemy.Level;
            float bossBonus = (isBossBattle) ? 2f : 1f;

            int expGain = Mathf.FloorToInt((expYield * enemyLevel * bossBonus) / 7);
            playerUnit.Enemy.Exp += expGain;
            yield return dialogBox.TypeDialog($"{playerUnit.Enemy.Base.Name} gained {expGain} exp!");
            yield return playerUnit.Hud.SetExpSmooth();


            //check level up
            while (playerUnit.Enemy.CheckForLevelUp())
            {
                playerUnit.Hud.SetLevel();
                yield return dialogBox.TypeDialog($"{playerUnit.Enemy.Base.Name} grew to level {playerUnit.Enemy.Level}!");

                yield return playerUnit.Hud.SetExpSmooth(true);
            }

            yield return new WaitForSeconds(1f);
        }

        CheckBattleOver(deadUnit);
    }

    void CheckBattleOver(BattleUnit deadUnit)
    {
        if (deadUnit.IsPlayer)
        {
            BattleOver(false);
        }
        else
        {
            BattleOver(true);
        }
    }

    IEnumerator ShowDamageRecap(DamageRecap damageRecap)
    {
        if (damageRecap.Critical > 1f)
        {
            yield return dialogBox.TypeDialog("That hit a vital!");
        }

        if (damageRecap.TypeEffect > 1f && damageRecap.TypeEffect < 2f)
        {
            yield return dialogBox.TypeDialog("That hit a weak point!");
        }
        else if (damageRecap.TypeEffect > 1f && damageRecap.TypeEffect  > 2f)
        {
            yield return dialogBox.TypeDialog("That hit a major weak point!");
        }
        else if (damageRecap.TypeEffect < 1f && damageRecap.TypeEffect  > 0.5f)
        {
            yield return dialogBox.TypeDialog("That didn't do much...");
        }
        else if (damageRecap.TypeEffect < 1f && damageRecap.TypeEffect  < 0.75f)
        {
            yield return dialogBox.TypeDialog("That really didn't do much...");
        }
    }

    private void Update()
    {
        if (state == BattleState.ActionSelect)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.SkillSelection)
        {
            HandleSkillSelection();
        }
        
    }

    void HandleActionSelection()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
                ++currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A))
        {
                --currentAction;
        }

        currentAction = Mathf.Clamp(currentAction, 0, 3);

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (currentAction == 0)
            {
                //Attack
                SkillSelect();
            }
            else if (currentAction == 1)
            {
                //Items
                StartCoroutine(RunTurns(BattleAction.UseItem));
            }
            else if (currentAction == 2)
            {
                //???
            }
            else if (currentAction == 3)
            {
                //Run
                StartCoroutine(RunTurns(BattleAction.Run));
            }
        }
    }

    void HandleSkillSelection()
    {  
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
                ++currentSkill;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
                currentSkill += 5;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
                --currentSkill;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
                currentSkill -= 5;
        }

        currentSkill = Mathf.Clamp(currentSkill, 0 , playerUnit.Enemy.Skills.Count - 1);

        dialogBox.UpdateSkillSelection(currentSkill, playerUnit.Enemy.Skills[currentSkill]);

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            var skill = playerUnit.Enemy.Skills[currentSkill];
            if (skill.Mana == 0) return;

            dialogBox.EnableSkillSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(RunTurns(BattleAction.Skill));
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Backspace))
        {
            var skill = playerUnit.Enemy.Skills[currentSkill];
            if (skill.Mana == 0) return;

            dialogBox.EnableSkillSelector(false);
            dialogBox.EnableDialogText(true);
            ActionSelect();
        }
    }

    void EndBattle(bool won)
    {
       if (!won)
        {
            // Clear all player prefs if the player loses
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs cleared.");
            SceneManager.LoadScene(0); // Load a specific scene for losing, such as a game over screen
        }
        else
        {
            if (isBossBattle == true)
            {
                // It's a boss battle; proceed to the win screen
                PlayerPrefs.SetString("sceneName", "WinScreen");
                PlayerPrefs.Save(); // Ensure the data is saved
                SceneManager.LoadScene("WinScreen");
            }
            else
            {
                SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
                sceneLoader.LoadData();
            }
        }
    }

    IEnumerator TryToRun()
    {
        state = BattleState.Busy;

        if(isBossBattle)
        {
            yield return dialogBox.TypeDialog($"You can't run from this!");
            state = BattleState.RunningTurn;
            yield break;
        }

        ++escapeAttempts;

        int playerDexterity = playerUnit.Enemy.Dexterity;
        int enemyDexterity = enemyUnit.Enemy.Dexterity;

        if (enemyDexterity < playerDexterity)
        {
            yield return dialogBox.TypeDialog($"You ran away!");
            BattleOver(true);
        }
        else
        {
            float f = (playerDexterity * 128) / enemyDexterity + 30 * escapeAttempts;
            f = f % 256;

            if (UnityEngine.Random.Range(0, 256) < f)
            {
                yield return dialogBox.TypeDialog($"You ran away!");
                BattleOver(true);
            }
            else
            {
                yield return dialogBox.TypeDialog($"You got cut off!");
                state = BattleState.RunningTurn;
            }
        }
    }
}
