using Infrastructure.InputSystem;
using VContainer.Unity;

namespace Infrastructure.Bootstrap
{
    public class BootstrapCameraInputService : IStartable
    {
        private readonly CameraInputService _service;

        public BootstrapCameraInputService(CameraInputService service)
        {
            _service = service;
        }

        public void Start()
        {
            // Можно здесь включить Input, подписаться на события и т.д.
        }
    }
}