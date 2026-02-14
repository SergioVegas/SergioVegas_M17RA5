using System.Collections.Generic;
using UnityEngine;

public class InteractBehavior : MonoBehaviour
{
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();

    public void Interact()
    {
        // Limpiamos la lista de objetos que hayan sido destruidos 
        _interactablesInRange.RemoveAll(i => i == null || (i is MonoBehaviour mb && mb == null));

        if (_interactablesInRange.Count > 0)
        {
            IInteractable closest = null;
            float minDistance = float.MaxValue;

            foreach (var interactable in _interactablesInRange)
            {
                if (interactable is MonoBehaviour mb)
                {
                    float dist = Vector3.Distance(transform.position, mb.transform.position);
                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        closest = interactable;
                    }
                }
            }

            if (closest != null)
            {
                closest.Interact();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && !_interactablesInRange.Contains(interactable))
        {
            _interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            _interactablesInRange.Remove(interactable);
        }
    }
}
