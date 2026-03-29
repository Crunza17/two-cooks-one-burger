using UnityEngine;
using System.Collections.Generic;

public class IngredientStack : MonoBehaviour
{
    [Header("Stack Settings")]
    [SerializeField] private Transform attachPoint; 
    [SerializeField] private float verticalStep = 0.1f; 

    public List<IngredientData> stackedContents = new List<IngredientData>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ingredient>(out Ingredient incomingIngredient))
        {
            Rigidbody rb = incomingIngredient.GetComponent<Rigidbody>();

            if (rb is not null && !rb.isKinematic)
            {
                TrySnapToStack(incomingIngredient);
            }
        }
    }

    private void TrySnapToStack(Ingredient incoming)
    {
        stackedContents.Add(incoming.GetIngredientData());

        Rigidbody rb = incoming.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        incoming.GetComponent<Collider>().enabled = false;

        float yOffset = (stackedContents.Count - 1) * verticalStep;
        incoming.transform.position = attachPoint.position + new Vector3(0, yOffset, 0);
        incoming.transform.rotation = attachPoint.rotation;

        incoming.transform.SetParent(this.transform);

        Debug.Log($"Stacked {incoming.GetIngredientData().ingredientName} on {gameObject.name}");
    }

    public void RemoveIngredient(Ingredient incoming)
    {
        IngredientData dataToRemove = incoming.GetIngredientData();

        if (stackedContents.Contains(dataToRemove))
        {
            stackedContents.Remove(dataToRemove);
            Debug.Log($"Removed {dataToRemove.ingredientName} from stack");
        }

        incoming.transform.SetParent(null); 

        Rigidbody rb = incoming.GetComponent<Rigidbody>();
        if (rb is null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}