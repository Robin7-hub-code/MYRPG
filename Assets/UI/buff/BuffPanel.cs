using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuffPanel : MonoBehaviour
{
    [Header("预制体和容器")]
    [SerializeField] private GameObject buffItemPrefab;
    [SerializeField] private RectTransform container;

    [Header("排列参数")]
    [SerializeField] private float itemSize = 56f;
    [SerializeField] private float spacing = 6f;
    [SerializeField] private bool alignRight = false;

    private List<BuffItem> activeBuffs = new List<BuffItem>();
    private Dictionary<string, BuffItem> buffLookup = new Dictionary<string, BuffItem>();
    private CanvasGroup panelCg;

    private void Awake()
    {
        panelCg = GetComponent<CanvasGroup>();
        if (panelCg == null)
            panelCg = gameObject.AddComponent<CanvasGroup>();
        UpdatePanelVisibility();
    }

    private void Update()
    {
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            var buff = activeBuffs[i];
            buff.Tick(Time.deltaTime);
            if (buff.IsExpired)
            {
                RemoveBuff(buff.BuffId);
            }
        }
    }

    public void AddBuff(BuffData buffData)
    {
        if (buffLookup.TryGetValue(buffData.buffId, out var existing))
        {
            existing.AddStack(buffData.currentStack);
            return;
        }

        var go = Instantiate(buffItemPrefab, container);
        go.name = $"Buff_{buffData.buffId}";

        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = alignRight ? new Vector2(1, 0.5f) : new Vector2(0, 0.5f);
        rt.anchorMax = rt.anchorMin;
        rt.pivot = alignRight ? new Vector2(1, 0.5f) : new Vector2(0, 0.5f);
        float startX = alignRight
            ? -(activeBuffs.Count * (itemSize + spacing))
            : (activeBuffs.Count * (itemSize + spacing));
        rt.anchoredPosition = new Vector2(startX, 0);

        var buffItem = go.GetComponent<BuffItem>();
        if (buffItem == null)
        {
            Debug.LogError("BuffItemPrefab 上缺少 BuffItem 组件！");
            Destroy(go);
            return;
        }

        buffItem.Initialize(buffData, playAnimation: true);
        activeBuffs.Add(buffItem);
        buffLookup[buffData.buffId] = buffItem;

        RefreshLayout(animate: true);
        UpdatePanelVisibility();
    }

    public void RemoveBuff(string buffId)
    {
        if (!buffLookup.TryGetValue(buffId, out var buffItem)) return;

        buffLookup.Remove(buffId);
        activeBuffs.Remove(buffItem);

        RefreshLayout(animate: true);
        UpdatePanelVisibility();

        buffItem.PlayRemoveAnimation(() =>
        {
            Destroy(buffItem.gameObject);
        });
    }

    public void ClearAll()
    {
        foreach (var buff in activeBuffs.ToList())
        {
            RemoveBuff(buff.BuffId);
        }
    }

    public bool HasBuff(string buffId) => buffLookup.ContainsKey(buffId);

    private void RefreshLayout(bool animate)
    {
        for (int i = 0; i < activeBuffs.Count; i++)
        {
            float targetX = alignRight
                ? -(i * (itemSize + spacing))
                : (i * (itemSize + spacing));

            if (animate)
            {
                activeBuffs[i].MoveTo(targetX, 0.3f);
            }
            else
            {
                var rt = activeBuffs[i].GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(targetX, 0);
            }
        }

        float totalWidth = activeBuffs.Count * itemSize + Mathf.Max(0, activeBuffs.Count - 1) * spacing;
        container.sizeDelta = new Vector2(totalWidth, container.sizeDelta.y);
    }

    private void UpdatePanelVisibility()
    {
        bool hasBuffs = activeBuffs.Count > 0;
        panelCg.alpha = hasBuffs ? 1f : 0f;
        panelCg.blocksRaycasts = false;
        panelCg.interactable = false;
    }
}
