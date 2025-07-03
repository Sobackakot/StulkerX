
using Character.MainCamera.Raycast;
using TMPro;
using UnityEngine;

namespace Window.UI
{
    public class WindowUI : MonoBehaviour 
    {
        public static WindowUI instanceUI;
        private RaycastPointCamera ray;

        [SerializeField] private TextMeshProUGUI interactText;
        private string currentText;
        public bool isPointerEnterUI { get; private set; }

        private void Awake()
        {
            if (instanceUI != null)
            {
                Destroy(gameObject);
                return;
            }
            instanceUI = this;
            DontDestroyOnLoad(gameObject);
            ray = FindObjectOfType<RaycastPointCamera>();
        }
        private void OnEnable()
        {
            ray.onShowTextByHitPoint += ShowTextByHitPoint;
        }
        private void OnDisable()
        {
            ray.onShowTextByHitPoint -= ShowTextByHitPoint;
        }
 
        public void ShowTextByHitPoint(string newText)
        {
            currentText = newText;
            interactText.text = currentText;
        } 
    } 
}

