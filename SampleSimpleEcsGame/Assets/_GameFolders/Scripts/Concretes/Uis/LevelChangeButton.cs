using EcsGame.Abstracts.Uis;
using EcsGame.Managers;

namespace EcsGame.Uis
{
    public class LevelChangeButton : BaseButton
    {
        protected override void HandleOnButtonClicked()
        {
            if (GameManager.Instance == null) return;
            
            GameManager.Instance.LoadScene();
        }
    }
}