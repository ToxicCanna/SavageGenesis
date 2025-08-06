using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    [SerializeField] private GameObject backgroundImage;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject enemySprite;

    [SerializeField] private GameObject playerTwoSprite;
    [SerializeField] private GameObject enemyTwoSprite;

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

    [SerializeField] private GameObject switchOne;
    [SerializeField] private GameObject switchTwo;
    [SerializeField] private GameObject switchThree;
    [SerializeField] private GameObject switchFour;
    [SerializeField] private GameObject switchOneSprite;
    [SerializeField] private GameObject switchTwoSprite;
    [SerializeField] private GameObject switchThreeSprite;
    [SerializeField] private GameObject switchFourSprite;

    [SerializeField] private GameObject switchOneFaintSprite;
    [SerializeField] private GameObject switchTwoFaintSprite;
    [SerializeField] private GameObject switchThreeFaintSprite;
    [SerializeField] private GameObject switchFourFaintSprite;

    [SerializeField] private GameObject moveTypeText;
    [SerializeField] private GameObject movePowerText;

    [SerializeField] private GameObject playerNameText;
    [SerializeField] private GameObject playerLevelText;
    [SerializeField] private GameObject playerHealthBar;
    [SerializeField] private GameObject playerHealthText;

    [SerializeField] private GameObject playerStatusBarBackground;
    [SerializeField] private GameObject playerStatusText;

    [SerializeField] private GameObject playerExpBar;

    [SerializeField] private GameObject playerTwoNameText;
    [SerializeField] private GameObject playerTwoLevelText;
    [SerializeField] private GameObject playerTwoHealthBar;
    [SerializeField] private GameObject playerTwoHealthText;

    [SerializeField] private GameObject playerTwoStatusBarBackground;
    [SerializeField] private GameObject playerTwoStatusText;

    [SerializeField] private GameObject enemyNameText;
    [SerializeField] private GameObject enemyLevelText;
    [SerializeField] private GameObject enemyHealthBar;
    [SerializeField] private GameObject enemyHealthText;

    [SerializeField] private GameObject enemyStatusBarBackground;
    [SerializeField] private GameObject enemyStatusText;

    [SerializeField] private GameObject enemyTwoNameText;
    [SerializeField] private GameObject enemyTwoLevelText;
    [SerializeField] private GameObject enemyTwoHealthBar;
    [SerializeField] private GameObject enemyTwoHealthText;

    [SerializeField] private GameObject enemyTwoStatusBarBackground;
    [SerializeField] private GameObject enemyTwoStatusText;

    [SerializeField] private PlayerInventory playerDinoInventory;

    [SerializeField] private PlayerInventory enemyDinoInventory;

    [SerializeField] private GameObject switchSelector;
    [SerializeField] private GameObject switchSelectorCursor;



    public GameObject GetBacigroundImage()
    { return backgroundImage; }

    public GameObject GetPlayerSprite()
    { return playerSprite; }

    public GameObject GetPlayerTwoSprite()
    { return playerTwoSprite; }

    public GameObject GetEnemySprite()
    { return enemySprite; }

    public GameObject GetEnemyTwoSprite()
    { return enemyTwoSprite; }

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

    public GameObject GetSwitchOne()
    { return switchOne; }

    public GameObject GetSwitchTwo()
    { return switchTwo; }

    public GameObject GetSwitchThree()
    { return switchThree; }

    public GameObject GetSwitchFour()
    { return switchFour; }

    public GameObject GetSwitchOneSprite()
    { return switchOneSprite; }

    public GameObject GetSwitchTwoSprite()
    { return switchTwoSprite; }

    public GameObject GetSwitchThreeSprite()
    { return switchThreeSprite; }

    public GameObject GetSwitchFourSprite()
    { return switchFourSprite; }

    public GameObject GetSwitchOneFaintSprite()
    { return switchOneFaintSprite; }

    public GameObject GetSwitchTwoFaintSprite()
    { return switchTwoFaintSprite; }

    public GameObject GetSwitchThreeFaintSprite()
    { return switchThreeFaintSprite; }

    public GameObject GetSwitchFourFaintSprite()
    { return switchFourFaintSprite; }

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

    public GameObject GetPlayerTwoNameText()
    { return playerTwoNameText; }

    public GameObject GetPlayerTwoLevelText()
    { return playerTwoLevelText; }

    public GameObject GetPlayerTwoHealthBar()
    { return playerTwoHealthBar; }

    public GameObject GetPlayerTwoHealthText()
    { return playerTwoHealthText; }

    public GameObject GetPlayerTwoStatusBarBackground()
    { return playerTwoStatusBarBackground; }

    public GameObject GetPlayerTwoStatusText()
    { return playerTwoStatusText; }

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

    public GameObject GetEnemyTwoNameText()
    { return enemyTwoNameText; }

    public GameObject GetEnemyTwoLevelText()
    { return enemyTwoLevelText; }

    public GameObject GetEnemyTwoHealthBar()
    { return enemyTwoHealthBar; }

    public GameObject GetEnemyTwoHealthText()
    { return enemyTwoHealthText; }

    public GameObject GetEnemyTwoStatusBarBackground()
    { return enemyTwoStatusBarBackground; }

    public GameObject GetEnemyTwoStatusText()
    { return enemyTwoStatusText; }

    public PlayerInventory GetPlayerDinoInventory()
    {
        return playerDinoInventory;
    }

    public PlayerInventory GetEnemyDinoInventory()
    {
        return enemyDinoInventory;
    }

    public GameObject GetSwitchSelector()
    { 
        return switchSelector;
    }

    public GameObject GetSwitchSelectorCursor()
    {
        return switchSelectorCursor;
    }

}
