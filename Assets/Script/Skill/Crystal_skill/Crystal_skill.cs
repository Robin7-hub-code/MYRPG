using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_skill : Skill
{
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private float crystalDuration;
    [SerializeField] private bool canMove;
    [SerializeField] private bool canExplode;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject current_crystal;

    [Header("Multi stacking crystal")]
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

        if (canUseMuticrystal)
        {
            RefilList();
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
