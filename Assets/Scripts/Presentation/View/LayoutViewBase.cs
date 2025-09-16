using UnityEngine;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;

namespace Presentation.View
{
    public abstract class LayoutViewBase : MonoBehaviour
    {
        protected UIDocument uiDocument;
        protected VisualElement root;

        public virtual void Awake()
        {
            uiDocument = GetComponent<UIDocument>();
            root = uiDocument.rootVisualElement;

            InitializeElements();
        }

        /// <summary>
        /// Инициализация всех элементов UI (кнопок, панелей и т.д.)
        /// </summary>
        protected abstract void InitializeElements();

        /// <summary>
        /// Асинхронное отображение UI
        /// </summary>
        public virtual async UniTask ShowAsync()
        {
            Show();
            await UniTask.Yield();
        }

        /// <summary>
        /// Синхронное отображение UI
        /// </summary>
        protected virtual void Show()
        {
            root.style.display = DisplayStyle.Flex;
        }

        /// <summary>
        /// Асинхронное скрытие UI
        /// </summary>
        public virtual async UniTask HideAsync()
        {
            root.style.display = DisplayStyle.None;
            await UniTask.Yield();
        }
    }
}
