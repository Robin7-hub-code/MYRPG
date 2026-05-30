using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal_skill : Skill
{
    [SerializeField] private GameObject current_crystal;
    [SerializeField] private float crystalDuration;
    [SerializeField] private GameObject crystalPrefab;


    [Header("CrystalOrigin")]
    [SerializeField] private skilltreeSlot_UI unlockCrystalButton;
    public bool crystalUnlocked { get; private set; }
    [Header("CrystalMirrage")]
    [SerializeField] private skilltreeSlot_UI unlockCloneInsteadButton;
    public bool crystalInsteadOfUnlocked { get; private set; }

    [Header("can explode Crystal")]
    [SerializeField] private skilltreeSlot_UI unlockExplodeButton;
    [SerializeField] private bool canExplode;

    [Header("Moving crystal")]
    [SerializeField] private skilltreeSlot_UI unlockMovingCrystalButton;
    [SerializeField] private bool canMove;
    [SerializeField] private float moveSpeed;


    [Header("Multi stacking crystal")]
    [SerializeField] private skilltreeSlot_UI unlockMultiCrystalButton;
    [SerializeField] bool canUseMuticrystal;
    [SerializeField] private int numOfstacks;
    [SerializeField] private float multyStacksCooldown;
    [SerializeField] private float useWindow;
    [SerializeField] private List<GameObject> crystalStacks= new List<GameObject>();
    public override bool CanUseSkill()
    {
        if (canUseMuticrystal)
        {
            if (coolDownTimer > 0)
            {
                return false;
            }

            UseSkill();
            return true;
        }

        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        if(canUseMuticrystal)
        {
            CanUseMultyCrystal();
            return;
        }
       
        if(current_crystal==null)
        {
            CreateCrystal();
        }
        else
        {
            Vector2 playerPos=player.transform.position;

            player.transform.position=current_crystal.transform.position;

            current_crystal.transform.position=playerPos;

            // create a clone at the original player position after swapping
            // CreateClone uses the given transform position + offset, so compute offset
            // from the player's new position to the original position
            Vector3 offsetToOriginal = (Vector3)playerPos - player.transform.position;
            if (crystalInsteadOfUnlocked)
            {
                SkillManager.instance.clone.CreateClone(player.transform, offsetToOriginal);
            }

            current_crystal.GetComponent<Crystal_Controller>()?.FinishCrystal();
        }
    }

    public void CreateCrystal()
    {
        current_crystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        Crystal_Controller new_crystal_controller = current_crystal.GetComponent<Crystal_Controller>();

        new_crystal_controller.SetupCrystal(crystalDuration, canExplode, canMove, moveSpeed, FindClosestEnemy(current_crystal.transform));
    }

    protected override void Start()
    {
        base.Start();
        unlockCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockCrystal);
        unlockCloneInsteadButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalImage);
        unlockExplodeButton.GetComponent<Button>().onClick.AddListener(UnlockExplosionCrystal);
        unlockMultiCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMultiCrystal);
        unlockMovingCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMoveCrystal);

        if (canUseMuticrystal)
        {
            RefilList();
        }
    }
    private void UnlockCrystal()
    {
        if(unlockCrystalButton.unlocked)
        {
            crystalUnlocked = true;
        }
    }
    private void UnlockCrystalImage()
    {
        if(unlockCloneInsteadButton.unlocked)
        {
            crystalInsteadOfUnlocked = true;
        }
    }

    private void UnlockExplosionCrystal()
    {
        if(unlockExplodeButton.unlocked)
        {
            canExplode = true;
        }

    }
    private void UnlockMoveCrystal()
    {
        if(unlockMovingCrystalButton.unlocked)
        {
            canMove= true;
        }
    }
    private void UnlockMultiCrystal()
    {
        if(unlockMultiCrystalButton.unlocked)
        {
            canUseMuticrystal = true;
        }

    }

    protected override void Update()
    {
        base.Update();
    }
    private bool CanUseMultyCrystal()
    {
        if(canUseMuticrystal)
        {
            if(crystalStacks.Count>0)
            {
                CancelInvoke(nameof(ResetAbility));
                GameObject newCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);

                crystalStacks.RemoveAt(crystalStacks.Count - 1);
                newCrystal.GetComponent<Crystal_Controller>().SetupCrystal(crystalDuration, canExplode, canMove, moveSpeed, FindClosestEnemy(newCrystal.transform));

                if(crystalStacks.Count<=0)
                {
                    StartMultiCrystalCooldown();
                }
                else
                {
                    Invoke(nameof(ResetAbility), useWindow);
                }

                return true;
            }
        }
        return false;
    }
    private void RefilList()
    {
        crystalStacks.Clear();

        for(int i=0;i<numOfstacks;i++)
        {
            crystalStacks.Add(crystalPrefab);
        }
    }
    private void ResetAbility()
    {
        StartMultiCrystalCooldown();
    }

    private void StartMultiCrystalCooldown()
    {
        CancelInvoke(nameof(ResetAbility));
        coolDownTimer = multyStacksCooldown;
        RefilList();
    }

}
