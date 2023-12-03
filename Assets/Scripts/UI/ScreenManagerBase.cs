using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;

namespace ClothesShopToy.UI
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    public class ScreenManagerBase : MonoBehaviour
    {
        public delegate void ScreenManagerEvent();
        public static event ScreenManagerEvent OnScreenOpened;
        public static event ScreenManagerEvent OnScreenClosed;
        
        [Header("Screen Manager")]
        [SerializeField] protected Canvas canvas;
        [SerializeField] protected CanvasGroup canvasGroup;
        
        public bool IsShowingScreen => canvas.enabled && canvasGroup.alpha > 0f;
        
        #region Unity Messages
        protected virtual void Awake()
        {
            Assert.IsNotNull(canvas);
            Assert.IsNotNull(canvasGroup);
        }

        protected virtual void OnDestroy()
        {

        }

        protected virtual void Reset()
        {
            canvas = GetComponent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
        #endregion

        #region Public Methods
        [Button]
        public void ShowScreen() => ToggleScreen(true);
        [Button]
        public void HideScreen() => ToggleScreen(false);
        public void ToggleScreenShow() => ToggleScreen(!IsShowingScreen);
        #endregion

        #region Private Methods
        protected virtual void ToggleScreen(bool show)
        {
            canvas.enabled = show;
            canvasGroup.interactable = show;
            if (show)
                OnScreenOpened?.Invoke();
            else
                OnScreenClosed?.Invoke();
        }
        #endregion
    }
}