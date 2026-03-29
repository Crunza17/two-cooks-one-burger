using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Kitchen/Ingredient Data")]
public class IngredientData : ScriptableObject
{
    public string ingredientName;
    public GameObject prefab;
    public float cookingTimeRequired;
    public Material cookedMaterial;
}
