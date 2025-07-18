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
