using UnityEngine;

public class Pan : MonoBehaviour
{
    [SerializeField] private float heatPower = 1.0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Ingredient>(out Ingredient ingredient))
        {
            ingredient.ApplyHeat(heatPower);
        }
    }
}
