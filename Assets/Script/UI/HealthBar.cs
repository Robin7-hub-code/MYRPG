using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Entity entity;
    private CharacterStats stats;
    private RectTransform rectTransform;
    private Slider slider;
    private void Start()
    {
        entity = GetComponentInParent<Entity>();
        rectTransform = GetComponent<RectTransform>();
        slider=GetComponentInChildren<Slider>();
        entity.onFlipped += FlipUI;
        stats=GetComponentInParent<CharacterStats>();

        stats.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
    }
    private void FlipUI()
    {
        rectTransform.Rotate(0, 180, 0);
    }
    private void UpdateHealthUI()
    {
        slider.maxValue = stats.GetMaxHealthValue();
        slider.value = Mathf.Clamp(stats.currentHealth,0,stats.GetMaxHealthValue());
    }

    private void OnDisable()
    {
        entity.onFlipped-= FlipUI;
        stats.onHealthChanged -= UpdateHealthUI;
    }
}
