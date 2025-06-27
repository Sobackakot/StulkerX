 
using TMPro;
using UnityEngine; 

public class WindowUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText; 
    private string currentText;

    public void SetInteractText(string newText)
    { 
        currentText = newText; 
    }
    public void ShowInteractText()
    {
        interactText.text = currentText;  
    }
    public void FixedUpdate()
    {
        ShowInteractText();
    }
}
