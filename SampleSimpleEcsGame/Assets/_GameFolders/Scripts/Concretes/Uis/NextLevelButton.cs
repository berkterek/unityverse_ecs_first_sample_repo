using EcsGame.Abstracts.Uis;
using EcsGame.Managers;

namespace EcsGame.Uis
{
    public class NextLevelButton : BaseButton
    {
        protected override void HandleOnButtonClicked()
        {
            if (GameManager.Instance == null) return;
            
            GameManager.Instance.LevelDataContainer.IncreaseLevel();
            GameManager.Instance.ChangeMaxScore();
            
            GameManager.Instance.LoadScene();
        }
    }
}