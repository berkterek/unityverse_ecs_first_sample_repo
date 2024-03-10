using UnityEngine;

namespace EcsGame.Uis
{
    public class CanvasGroupHelper : MonoBehaviour
    {
        [SerializeField] CanvasGroup _canvasGroup;

        void OnValidate()
        {
            if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OpenCanvas()
        {
            OpenCloseCanvas(1f,true);
        }

        public void CloseCanvas()
        {
            OpenCloseCanvas(0f,false);
        }

        void OpenCloseCanvas(float alpha, bool value)
        {
            _canvasGroup.alpha = alpha;
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }
    }    
}

