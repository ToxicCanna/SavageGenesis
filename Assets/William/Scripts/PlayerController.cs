using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Code.Scripts.Managers.Singleton<PlayerController>
{
    [SerializeField] private CombatStateMachine combatSM;
    private CombatStateMachine _stateMachine;
    public UnityEvent confirmPressed;
    public UnityEvent cancelPressed;
    public UnityEvent upPressed;
    public UnityEvent downPressed;
    public UnityEvent leftPressed;
    public UnityEvent rightPressed;

    [SerializeField] private ActionSelection actionSelection;
    [SerializeField] private SkillSelection skillSelection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        confirmPressed.AddListener(combatSM.PlayerPressedConfirm);
    }

    public void ActionSelectionAddListener()
    {
        upPressed.AddListener(actionSelection.PlayerPressedUp);
        downPressed.AddListener(actionSelection.PlayerPressedDown);
        confirmPressed.AddListener(actionSelection.PlayerPressedConfirm);
    }

    public void ActionSelectionRemoveListener()
    {
        upPressed.RemoveListener(actionSelection.PlayerPressedUp);
        downPressed.RemoveListener(actionSelection.PlayerPressedDown);
        confirmPressed.RemoveListener(actionSelection.PlayerPressedConfirm);
    }

    public void SkillSelectionAddListener()
    {
        upPressed.AddListener(skillSelection.PlayerPressedUp);
        downPressed.AddListener(skillSelection.PlayerPressedDown);
        leftPressed.AddListener(skillSelection.PlayerPressedLeft);
        rightPressed.AddListener(skillSelection.PlayerPressedRight);
        confirmPressed.AddListener(skillSelection.PlayerPressedConfirm);
        cancelPressed.AddListener(skillSelection.PlayerPressedCancel);
    }

    public void SkillSelectionRemoveListener() 
    {
        upPressed.RemoveListener(skillSelection.PlayerPressedUp);
        downPressed.RemoveListener(skillSelection.PlayerPressedDown);
        leftPressed.RemoveListener(skillSelection.PlayerPressedLeft);
        rightPressed.RemoveListener(skillSelection.PlayerPressedRight);
        confirmPressed.RemoveListener(skillSelection.PlayerPressedConfirm);
        cancelPressed.RemoveListener(skillSelection.PlayerPressedCancel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUp()
    {
        upPressed.Invoke();
    }

    public void OnDown()
    { 
        downPressed.Invoke();
    }

    public void OnLeft()
    {
        leftPressed.Invoke();
    }

    public void OnRight()
    {
        rightPressed.Invoke();
    }

    public void OnConfirm()
    {
        confirmPressed.Invoke();
    }

    public void OnCancel()
    {
        cancelPressed.Invoke();
    }
}
