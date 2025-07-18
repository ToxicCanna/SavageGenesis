using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    [SerializeField] private GameObject backgroundImage;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject enemySprite;
    [SerializeField] private GameObject actionSelector;
    [SerializeField] private GameObject actionSelectorCursor;
    [SerializeField] private GameObject skillSelector;
    [SerializeField] private GameObject skillSelectorCursor;

    [SerializeField] private GameObject dialogText;

    [SerializeField] private GameObject skillOne;
    [SerializeField] private GameObject skillTwo;
    [SerializeField] private GameObject skillThree;
    [SerializeField] private GameObject skillFour;
    [SerializeField] private GameObject skillFive;

    [SerializeField] private GameObject moveTypeText;
    [SerializeField] private GameObject movePowerText;

    [SerializeField] private GameObject playerNameText;
    [SerializeField] private GameObject playerLevelText;
    [SerializeField] private GameObject playerHealthBar;
    [SerializeField] private GameObject playerHealthText;

    [SerializeField] private GameObject playerStatusBarBackground;
    [SerializeField] private GameObject playerStatusText;

    [SerializeField] private GameObject playerExpBar;

    [SerializeField] private GameObject enemyNameText;
    [SerializeField] private GameObject enemyLevelText;
    [SerializeField] private GameObject enemyHealthBar;
    [SerializeField] private GameObject enemyHealthText;

    [SerializeField] private GameObject enemyStatusBarBackground;
    [SerializeField] private GameObject enemyStatusText;

    [SerializeField] private PlayerInventory playerDinoInventory;

    [SerializeField] private PlayerInventory enemyDinoInventory;


    public GameObject GetBacigroundImage()
    { return backgroundImage; }

    public GameObject GetPlayerSprite()
    { return playerSprite; }

    public GameObject GetEnemySprite()
    { return enemySprite; }

    public GameObject GetActionSelector()
    { return actionSelector; }

    public GameObject GetActionSelectorCursor()
    { return actionSelectorCursor; }

    public GameObject GetSkillSelector()
    { return skillSelector; }

    public GameObject GetSkillSelectorCursor()
    { return skillSelectorCursor; }

    public GameObject GetDialogText()
    { return dialogText; }

    public GameObject GetSkillOne()
    { return skillOne; }

    public GameObject GetSkillTwo()
    { return skillTwo; }

    public GameObject GetSkillThree()
    { return skillThree; }

    public GameObject GetSkillFour()
    { return skillFour; }

    public GameObject GetSkillFive()
    { return skillFive; }

    public GameObject GetMoveTypeText()
    { return moveTypeText; }

    public GameObject GetMovePowerText()
    { return movePowerText; }

    public GameObject GetPlayerNameText()
    { return playerNameText; }

    public GameObject GetPlayerLevelText()
    { return playerLevelText; }

    public GameObject GetPlayerHealthBar()
    { return playerHealthBar; }

    public GameObject GetPlayerHealthText()
    { return playerHealthText; }

    public GameObject GetPlayerStatusBarBackground()
    { return playerStatusBarBackground; }

    public GameObject GetPlayerStatusText()
    { return playerStatusText; }

    public GameObject GetPlayerExpBar()
    { return playerExpBar; }

    public GameObject GetEnemyNameText()
    { return enemyNameText; }

    public GameObject GetEnemyLevelText()
    { return enemyLevelText; }

    public GameObject GetEnemyHealthBar()
    { return enemyHealthBar; }

    public GameObject GetEnemyHealthText()
    { return enemyHealthText; }

    public GameObject GetEnemyStatusBarBackground()
    { return enemyStatusBarBackground; }

    public GameObject GetEnemyStatusText()
    { return enemyStatusText; }

    public PlayerInventory GetPlayerDinoInventory()
    {
        return playerDinoInventory;
    }

    public PlayerInventory GetEnemyDinoInventory()
    {
        return enemyDinoInventory;
    }

}
