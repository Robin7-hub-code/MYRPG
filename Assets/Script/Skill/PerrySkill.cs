using System;
using UnityEngine;
using UnityEngine.UI;

public class PerrySkill : Skill
{
    [Header("Parry")]
    [SerializeField] private skilltreeSlot_UI parryUnlockButton;
    public bool parryUnlocked;

    [Header("Parry restore")]
    [SerializeField] private skilltreeSlot_UI restoreUnlockButton;
    public bool restoreUnlocked;

    [Header("Parry with mirage")]
    [SerializeField] private skilltreeSlot_UI parryWithMirageUnlockButton;
    public bool parryWithMirageUnlocked;
    [Range(0f,1f)]
    [Header("回血量百分比")]
    [SerializeField] private float recovery;

    public override void UseSkill()
    {
        base.UseSkill();

        Debug.Log(" i m use skill");
        if(restoreUnlocked)
        {
            player.stats.IncreaseHealth(Mathf.RoundToInt(recovery * player.stats.maxHealth.GetValue()));
        }
    }

    protected override void Start()
    {
        base.Start();

        // register button listeners once on start. Use guards to avoid null refs.
        if (parryUnlockButton != null)
        {
            var btn = parryUnlockButton.GetComponent<Button>();
            if (btn != null) btn.onClick.AddListener(UnLockParry);
        }
        if (restoreUnlockButton != null)
        {
            var btn = restoreUnlockButton.GetComponent<Button>();
            if (btn != null) btn.onClick.AddListener(UnlockParryRestore);
        }
        if (parryWithMirageUnlockButton != null)
        {
            var btn = parryWithMirageUnlockButton.GetComponent<Button>();
            if (btn != null) btn.onClick.AddListener(UnlockParryWithMirrage);
        }
    }
    private void UnLockParry()
    {
        Debug.Log(" un lock");
        if (parryUnlockButton.unlocked)
        {
            parryUnlocked = true;
        }
    }
    private void UnlockParryRestore()
    {
        if (restoreUnlockButton.unlocked)
            restoreUnlocked = true;
    }
    private void UnlockParryWithMirrage()
    {
        if (parryWithMirageUnlockButton.unlocked)
            parryWithMirageUnlocked = true;
    }
    public void MakeMirrageOnParry(Transform _resPawn)
    {
        if(parryWithMirageUnlocked)
        {
            SkillManager.instance.clone.CreateCloneWithDelay(_resPawn);
        }
    }
}
