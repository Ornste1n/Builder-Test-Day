using Cysharp.Threading.Tasks;
using Presentation.View;

namespace Presentation.Presenters
{
    public abstract class LayoutPresenterBase<TView> where TView : LayoutViewBase
    {
        protected TView view;

        public LayoutPresenterBase(TView view)
        {
            this.view = view;
        }

        /// <summary>
        /// Инициализация презентера (например, подписка на события)
        /// </summary>
        public virtual void Initialize()
        {
            // Можно подписываться на события кнопок и т.д.
        }

        /// <summary>
        /// Асинхронная активация презентера
        /// </summary>
        public virtual async UniTask ActivateAsync()
        {
            if (view != null)
                await view.ShowAsync();
        }

        /// <summary>
        /// Очистка ресурсов, отписка от событий
        /// </summary>
        public virtual void Dispose()
        {
            // Очистка подписок и ресурсов
        }
    }
}