using UnityEngine;

public class GameplayUITestInput : MonoBehaviour
{
    [SerializeField] private GameplayUIManager uiManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            // Reduce durability by 10
            float newDurability = Mathf.Max(0f, GetCurrentDurability() - 10f);
            uiManager.SetDurability(newDurability);
            Debug.Log($"Durability set ot {newDurability}");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            // Set random bone count between 3 and 10
            int randomTotal = Random.Range(3, 11);
            uiManager.SetTotalBones(randomTotal);
            Debug.Log($"Total bones set to {randomTotal}");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            // Simulate collecting a bone
            uiManager.DecreaseRemainingBones();
            Debug.Log("Decreased remaining bones by 1");
        }
    }

    // Helper to get the current durability
    private float GetCurrentDurability()
    {
        var durabilityField = typeof(GameplayUIManager)
            .GetField("currentDurability", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        return durabilityField != null
            ? (float)durabilityField.GetValue(uiManager)
            : 0f;
    }
}
