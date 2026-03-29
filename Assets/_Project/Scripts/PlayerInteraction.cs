using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactDistance = 10f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform holdPosition;

    [SerializeField] private float grabSpeed = 20f;
    private Ingredient grabbedIngredient;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (grabbedIngredient is null)
            {
                TryPickUp();
            }
            else
            {
                Drop();
            }
        }

        if (grabbedIngredient is not null)
        {
            grabbedIngredient.transform.position = Vector3.MoveTowards(
            grabbedIngredient.transform.position,
            holdPosition.position,
            grabSpeed * Time.deltaTime);
        }
    }

    private void TryPickUp()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableLayer))
        {
            GameObject clickedObject = hit.collider.gameObject;

            clickedObject.transform.SetParent(null);

            if (clickedObject.TryGetComponent<Ingredient>(out Ingredient ingredient))
            {
                grabbedIngredient = ingredient;

                IngredientStack parentStack = ingredient.GetComponentInParent<IngredientStack>();
                if (parentStack != null)
                {
                    parentStack.RemoveIngredient(ingredient);
                }

                if (grabbedIngredient.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.isKinematic = true;
                }
                grabbedIngredient.GetComponent<Collider>().enabled = true;
            }
        }
    }

    private void Drop()
    {
        if (grabbedIngredient != null)
        {
            if (grabbedIngredient.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = false;
                rb.linearVelocity = Vector3.zero;
            }
            grabbedIngredient = null;
        }
    }
}
