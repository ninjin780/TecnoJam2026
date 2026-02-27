using UnityEngine;

namespace Assets.Scripts
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interacted with " + gameObject);
        }
    }
}
