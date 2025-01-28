using Game.Runtime.Application.Resources;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Infrastructure.Repository;
using Game.Runtime.Presentation.TopBar;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace Game.Runtime.Application.Game
{
    public class GameController : IInitializable
    {
        private readonly PlayerResourcesController _playerResourcesController;
        private readonly ISavesController _gameSaveController;
        private readonly IIocFactory _iocFactory;
        private readonly IPanelsService _panelsService;

        [Preserve]
        public GameController(PlayerResourcesController playerResourcesController, ISavesController gameSaveController,
            IIocFactory iocFactory, IPanelsService panelsService)
        {
            _playerResourcesController = playerResourcesController;
            _gameSaveController = gameSaveController;
            _iocFactory = iocFactory;
            _panelsService = panelsService;
        }

        void IInitializable.Initialize()
        {
            _playerResourcesController.Initialize();

            var topBarPanel = _panelsService.Open<TopPanel>();
            topBarPanel.SetPresenter(_iocFactory.Create<TopPanelPresenter>());

            _gameSaveController.SaveAllLocal();
        }
    }
}