using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GameplayUIManager : MonoBehaviour
{
    [Header("Bone Counter")]
    [SerializeField] private TMP_Text boneCounterVauleText;
    private int totalBones = 0;
    private int remainingBones = 0;

    [Header("Durability Bar")]
    [SerializeField] private Image durabilityFill;
    [SerializeField] private float maxDurability = 100f;
    private float currentDurability;

    [Header("Tool Buttons")]
    [SerializeField] private Button pickaxeButton;
    [SerializeField] private Button brushButton;

    [Tooltip("Use these to assign external tool scripts via the inspector.")]
    public UnityEvent onPickaxeSelected;
    public UnityEvent onBrushSelected;

    private void Start()
    {
        // Default: Hide UI until manually shown
        //gameObject.SetActive(false);

        // Assign button events
        pickaxeButton.onClick.AddListener(() => onPickaxeSelected.Invoke());
        brushButton.onClick.AddListener(() => onBrushSelected.Invoke());

        // Initialize durability
        currentDurability = maxDurability;
        UpdateDurabilityBar();
    }

    // Called externally when scene starts
    public void SetTotalBones(int total)
    {
        totalBones = Mathf.Max(0, total);
        remainingBones = totalBones;
        UpdateBoneCounter();
    }

    // Called when a bone is collected
    public void DecreaseRemainingBones()
    {
        remainingBones = Mathf.Max(0, remainingBones -1);
        UpdateBoneCounter();
    }

    private void UpdateBoneCounter()
    {
        boneCounterVauleText.text = $"{remainingBones}/{totalBones}";
    }

    // Called when tool takes damage or changes durability
    public void SetDurability(float newDurability)
    {
        currentDurability = Mathf.Clamp(newDurability, 0f, maxDurability);
        UpdateDurabilityBar();
    }

    public void SetMaxDurability(float newMax)
    {
        maxDurability = Mathf.Max(1f, newMax);
        currentDurability = maxDurability;
        UpdateDurabilityBar();
    }

    private void UpdateDurabilityBar()
    {
        int totalSteps = 25; // 200px tall bar / 8px per step
        float stepHeight = 1f / totalSteps;

        float normalized = currentDurability / maxDurability;

        int filledSteps = Mathf.RoundToInt(normalized * totalSteps);
        float snappedScale = filledSteps * stepHeight;

        // Pivot Y = 1
        durabilityFill.rectTransform.localScale = new Vector3(1f, snappedScale, 1f);
    }

    // Hides or shows the entire UI
    public void ShowUI(bool state)
    {
        gameObject.SetActive(state);
    }
}
