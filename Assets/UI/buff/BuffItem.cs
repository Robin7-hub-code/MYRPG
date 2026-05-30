using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuffItem : MonoBehaviour
{
    [Header("UI 组件引用")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text stackText;

    private BuffData data;
    private CanvasGroup cg;

    public string BuffId => data?.buffId ?? "";

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void Initialize(BuffData buffData, bool playAnimation = true)
    {
        data = buffData;
        data.remainTime = data.duration;

        if (iconImage) iconImage.sprite = data.icon;
        UpdateStackDisplay();

        if (cg) cg.alpha = 1f;

        if (playAnimation) PlayAppearAnimation();
    }

    public void PlayAppearAnimation()
    {
        var rt = GetComponent<RectTransform>();
        rt.localScale = Vector3.one * 0.3f;
        rt.localScale = Vector3.one;
    }

    public void PlayRemoveAnimation(Action onComplete)
    {
        var rt = GetComponent<RectTransform>();
        rt.localScale = Vector3.one * 0.3f;
        onComplete?.Invoke();
    }

    public void MoveTo(float targetX, float duration = 0.3f)
    {
        var rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(targetX, rt.anchoredPosition.y);
    }

    public void Tick(float deltaTime)
    {
        if (data == null || data.duration <= 0) return;

        data.remainTime -= deltaTime;

        UpdateAlphaDisplay();

        if (data.remainTime > 0 && data.remainTime < 2f)
        {
            float blink = Mathf.Sin(Time.time * 10f) * 0.3f + 0.5f;
            if(cg) cg.alpha = Mathf.Max(cg.alpha, blink);
        }
    }

    public bool IsExpired => data != null && data.duration > 0 && data.remainTime <= 0;

    public void AddStack(int amount = 1)
    {
        data.currentStack = Mathf.Min(data.currentStack + amount, data.maxStack);
        data.remainTime = data.duration;

        if (cg) cg.alpha = 1f;
        UpdateStackDisplay();
    }

    private void UpdateStackDisplay()
    {
        if (stackText == null) return;
        stackText.gameObject.SetActive(data.currentStack > 1);
        stackText.text = "x" + data.currentStack.ToString();
    }

    private void UpdateAlphaDisplay()
    {
        if (cg == null) return;
        cg.alpha = Mathf.Clamp01(data.remainTime / data.duration);
    }
}
