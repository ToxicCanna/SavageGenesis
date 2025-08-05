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
    [SerializeField] private SwitchSelection switchSelection;
    [SerializeField] private TargetSelection targetSelection;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (confirmPressed == null)
            confirmPressed = new UnityEvent();
        if (cancelPressed == null)
            cancelPressed = new UnityEvent();
        if (upPressed == null)
            upPressed = new UnityEvent();
        if (downPressed == null)
            downPressed = new UnityEvent();
        if (leftPressed == null)
            leftPressed = new UnityEvent();
        if (rightPressed == null)
            rightPressed = new UnityEvent();

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
        if(upPressed != null)
            upPressed.RemoveListener(actionSelection.PlayerPressedUp);
        if (downPressed != null)
            downPressed.RemoveListener(actionSelection.PlayerPressedDown);
        if (confirmPressed != null)
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
        if (upPressed != null)
            upPressed.RemoveListener(skillSelection.PlayerPressedUp);
        if (downPressed != null)
            downPressed.RemoveListener(skillSelection.PlayerPressedDown);
        if (leftPressed != null)
            leftPressed.RemoveListener(skillSelection.PlayerPressedLeft);
        if (rightPressed != null)
            rightPressed.RemoveListener(skillSelection.PlayerPressedRight);
        if (confirmPressed != null)
            confirmPressed.RemoveListener(skillSelection.PlayerPressedConfirm);
        if (cancelPressed != null)
            cancelPressed.RemoveListener(skillSelection.PlayerPressedCancel);
    }

    public void SwitchSelectionAddListener()
    {
        upPressed.AddListener(switchSelection.PlayerPressedUp);
        downPressed.AddListener(switchSelection.PlayerPressedDown);
        leftPressed.AddListener(switchSelection.PlayerPressedLeft);
        rightPressed.AddListener(switchSelection.PlayerPressedRight);
        confirmPressed.AddListener(switchSelection.PlayerPressedConfirm);
        cancelPressed.AddListener(switchSelection.PlayerPressedCancel);
    }

    public void SwitchSelectionRemoveListener()
    {
        if (upPressed != null)
            upPressed.RemoveListener(switchSelection.PlayerPressedUp);
        if (downPressed != null)
            downPressed.RemoveListener(switchSelection.PlayerPressedDown);
        if (leftPressed != null)
            leftPressed.RemoveListener(switchSelection.PlayerPressedLeft);
        if (rightPressed != null)
            rightPressed.RemoveListener(switchSelection.PlayerPressedRight);
        if (confirmPressed != null)
            confirmPressed.RemoveListener(switchSelection.PlayerPressedConfirm);
        if (cancelPressed != null)
            cancelPressed.RemoveListener(switchSelection.PlayerPressedCancel);
    }

    public void TargetSelectionAddListener()
    {
        leftPressed.AddListener(targetSelection.PlayerPressedLeft);
        rightPressed.AddListener(targetSelection.PlayerPressedRight);
        confirmPressed.AddListener(targetSelection.PlayerPressedConfirm);
        cancelPressed.AddListener(targetSelection.PlayerPressedCancel);
    }

    public void TargetSelectionRemoveListener()
    {
        if (leftPressed != null)
            leftPressed.RemoveListener(targetSelection.PlayerPressedLeft);
        if (rightPressed != null)
            rightPressed.RemoveListener(targetSelection.PlayerPressedRight);
        if (confirmPressed != null)
            confirmPressed.RemoveListener(targetSelection.PlayerPressedConfirm);
        if (cancelPressed != null)
            cancelPressed.RemoveListener(targetSelection.PlayerPressedCancel);
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
