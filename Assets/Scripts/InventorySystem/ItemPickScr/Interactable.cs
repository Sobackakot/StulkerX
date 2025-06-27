 
using UnityEngine; 

// Class representing an interactable object. 
public class Interactable : MonoBehaviour 
{ 
    // Method to perform the interaction.
    public virtual void Interaction()
    {
        Debug.Log("Interactable");
    } 
     
}
