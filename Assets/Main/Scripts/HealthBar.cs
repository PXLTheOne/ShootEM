using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //objects
    public Slider healthBarSlider;
    public Slider easeHealthBarSlider;
    private PlayerController playerControllerScript;

    //variables
    public float maxHealth = 100f;
    float easeSpeed = 0.05f;

    private void Start()
    {
        playerControllerScript = GameObject.Find("TurretHead").GetComponent<PlayerController>();
        playerControllerScript.health = maxHealth;
    }

    private void Update()
    {
        if (healthBarSlider.value != playerControllerScript.health)
        {
            healthBarSlider.value = playerControllerScript.health;
        }

        if (healthBarSlider.value != easeHealthBarSlider.value)
        {
            easeHealthBarSlider.value = Mathf.Lerp(easeHealthBarSlider.value, healthBarSlider.value, easeSpeed);
        }
    }
}
