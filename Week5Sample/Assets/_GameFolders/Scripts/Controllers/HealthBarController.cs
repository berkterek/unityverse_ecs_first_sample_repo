using UnityEngine;
using UnityEngine.UI;

namespace SampleScripts
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] Image _healthBarImage;

        public void SetHealthValues(float currentHealth, float maxHealth)
        {
            _healthBarImage.fillAmount = currentHealth / maxHealth;
        }
    }
}