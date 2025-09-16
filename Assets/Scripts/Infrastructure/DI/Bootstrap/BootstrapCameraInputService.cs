using Application.Interfaces.Camera;
using Application.UseCases.Camera;
using Infrastructure.InputSystem;
using Infrastructure.InputSystem.Services;
using VContainer.Unity;

namespace Infrastructure.Bootstrap
{
    // todo костыль
    public class BootstrapCameraInputService : IStartable
    {
        private readonly CameraInputService _service;

        public BootstrapCameraInputService(CameraInputService service, ICameraInput input)
        {
            _service = service;
        }

        public void Start()
        {
            // Можно здесь включить Input, подписаться на события и т.д.
        }
    }
}