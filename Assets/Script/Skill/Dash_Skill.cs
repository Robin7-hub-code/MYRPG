using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;


public class Dash_Skill : Skill
{
    [Header("Dash")]
    public bool dashUnlocked;
    [SerializeField]public skilltreeSlot_UI unlockDashButton;

    [Header("clone on dash")]
    public bool cloneOnDashUnlocked;
    [SerializeField]public skilltreeSlot_UI unlockDashStartButton;

    [Header("clone on arrival")]
    public bool cloneOnArrivalUnlocked;
    [SerializeField]public skilltreeSlot_UI unlockDashOverButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public override void UseSkill()
    {
        base.UseSkill();
    }

    protected override void Start()
    {
        base.Start();

        // register button listeners once on start. Use guards to avoid null refs.
        if (unlockDashButton != null)
        {
            var btn = unlockDashButton.GetComponent<Button>();
            if (btn != null) btn.onClick.AddListener(UnlockDash);
        }
        if (unlockDashStartButton != null)
        {
            var btn = unlockDashStartButton.GetComponent<Button>();
            if (btn != null) btn.onClick.AddListener(UnlockDashStart);
        }
        if (unlockDashOverButton != null)
        {
            var btn = unlockDashOverButton.GetComponent<Button>();
            if (btn != null) btn.onClick.AddListener(UnlockDashOver);
        }
    }
    private void UnlockDash()
    {
        Debug.Log(" un lock");
        if(unlockDashButton.unlocked)
        {
           dashUnlocked = true;
        }
    }
    private void UnlockDashStart()
    {
        if (unlockDashStartButton.unlocked)
            cloneOnDashUnlocked =true;
    }
    private void UnlockDashOver()
    {
        if (unlockDashOverButton.unlocked)
            cloneOnArrivalUnlocked = true;
    }

    public void CreateCloneOnDashStart()
    {
        if (cloneOnDashUnlocked)
        {
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
        }
    }
    public void CreateCloneOnDashOver()
    {
        if (cloneOnArrivalUnlocked)
        {
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
        }
    }
}
