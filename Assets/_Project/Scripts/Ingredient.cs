using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private IngredientData data;

    public enum CookingState { Raw, Cooked, Burned }
    public CookingState currentState = CookingState.Raw;

    [Header("Progress")]
    public float currentCookingTime = 0f;

    private void Start()
    {
        if (data is not null)
        {
            gameObject.name = data.ingredientName;
        }
    }

    public void ApplyHeat(float heatIntensity)
    {
        if (currentState == CookingState.Burned || data == null) return;

        currentCookingTime += heatIntensity * Time.deltaTime;

        if (currentCookingTime >= data.cookingTimeRequired && currentState == CookingState.Raw)
        {
            SetState(CookingState.Cooked);
        }
    }

    private void SetState(CookingState newState)
    {
        currentState = newState;

        if (newState == CookingState.Cooked && data.cookedMaterial is not null)
        {
            GetComponent<Renderer>().material = data.cookedMaterial;
            Debug.Log($"{data.ingredientName} is now COOKED!");
        }
    }

    public IngredientData GetIngredientData() => data;
}
