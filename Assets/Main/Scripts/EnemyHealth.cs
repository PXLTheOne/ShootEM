using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    //objects
    public Slider healthBarSlider;
    public Slider easeHealthBarSlider;

    //variables
    float easeSpeed = 0.05f;

    private void Update()
    {
        if (healthBarSlider.value != easeHealthBarSlider.value)
        {
            easeHealthBarSlider.value = Mathf.Lerp(easeHealthBarSlider.value, healthBarSlider.value, easeSpeed);
        }
    }

    public void HealthEdit(float enemyHealth)
    {
        if (healthBarSlider.value != enemyHealth)
        {
            healthBarSlider.value = enemyHealth;
        }
    }
}
