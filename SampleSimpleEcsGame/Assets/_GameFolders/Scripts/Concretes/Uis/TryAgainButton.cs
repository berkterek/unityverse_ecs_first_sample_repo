using EcsGame.Abstracts.Uis;
using EcsGame.Managers;

namespace EcsGame.Uis
{
    public class TryAgainButton : BaseButton
    {
        protected override void HandleOnButtonClicked()
        {
            if (GameManager.Instance == null) return;
            
            GameManager.Instance.LoadScene();
        }
    }
}