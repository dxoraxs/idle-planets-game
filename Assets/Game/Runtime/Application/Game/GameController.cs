using Game.Runtime.Application.Planet;
using Game.Runtime.Application.Resources;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Infrastructure.Repository;
using Game.Runtime.Presentation.GamePanel;
using Game.Runtime.Presentation.TopBar;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace Game.Runtime.Application.Game
{
    public class GameController : IInitializable
    {
        private readonly PlayerResourcesController _playerResourcesController;
        private readonly PlanetService _planetService;
        private readonly ISavesController _gameSaveController;
        private readonly IIocFactory _iocFactory;
        private readonly IPanelsService _panelsService;

        [Preserve]
        public GameController(PlayerResourcesController playerResourcesController, PlanetService planetService,
            ISavesController gameSaveController, IIocFactory iocFactory, IPanelsService panelsService)
        {
            _playerResourcesController = playerResourcesController;
            _planetService = planetService;
            _gameSaveController = gameSaveController;
            _iocFactory = iocFactory;
            _panelsService = panelsService;
        }

        void IInitializable.Initialize()
        {
            _playerResourcesController.Initialize();
            _planetService.Initialize();

            var topBarPanel = _panelsService.Open<TopPanel>();
            topBarPanel.SetPresenter(_iocFactory.Create<TopPanelPresenter>());

            var gamePanel = _panelsService.Open<GamePanel>();
            gamePanel.SetPresenter(_iocFactory.Create<GamePanelPresenter>());

            _iocFactory.Create<PlanetPanelPresenter>();

            _gameSaveController.SaveAllLocal();
        }
    }
}