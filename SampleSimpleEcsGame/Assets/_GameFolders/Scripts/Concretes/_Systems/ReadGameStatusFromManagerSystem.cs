using EcsGame.Components;
using EcsGame.Managers;
using Unity.Entities;

namespace EcsGame.Systems
{
    public partial class ReadGameStatusFromManagerSystem : SystemBase
    {
        bool _isOneTimeProcess = false;
        bool _canUpdateProcessContinue;

        protected override void OnCreate()
        {
            RequireForUpdate<GameStatusData>();
        }

        protected override void OnUpdate()
        {
            if (GameManager.Instance == null) return;

            SingleTimeProcess();

            if (!_canUpdateProcessContinue) return;

            var gameStatusData = SystemAPI.GetSingleton<GameStatusData>();
            gameStatusData.IsGameEnded = true;
            SystemAPI.SetSingleton(gameStatusData);
            _canUpdateProcessContinue = false;
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.OnFinishedGame -= HandleOnFinishGame;
            GameManager.Instance.OnGameOvered -= HandleOnGameOvered;
        }

        void SingleTimeProcess()
        {
            if (!_isOneTimeProcess)
            {
                _isOneTimeProcess = true;
                GameManager.Instance.OnFinishedGame += HandleOnFinishGame;
                GameManager.Instance.OnGameOvered += HandleOnGameOvered;
            }
        }

        void HandleOnGameOvered()
        {
            _canUpdateProcessContinue = true;
        }

        void HandleOnFinishGame()
        {
            _canUpdateProcessContinue = true;
        }
    }
}