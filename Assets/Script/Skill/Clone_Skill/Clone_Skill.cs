using UnityEngine;
using JetBrains.Annotations;
using Mono.Cecil.Cil;
using System.Collections;
using UnityEngine.UI;
public class Clone_Skill : Skill
{
    [SerializeField] private GameObject clonePreFab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private float colorLossSpeed;
    [Space]
    [Header("克隆攻击")]
    [SerializeField] private bool canAttack;
    [SerializeField] private skilltreeSlot_UI cloneAttackUnlockButton;

    [Header("使克隆有攻击性")]
    [SerializeField] private skilltreeSlot_UI aggressiveCloneUnlockButton;
    public bool canApplyOnHitEffect { get; private set; }

    [SerializeField] private bool createCloneOnDashStart;
    [SerializeField] private bool createCloneOnDashOver;
    [Header("多重分身")]
    [SerializeField] private skilltreeSlot_UI multipleUnlockButton;
    [SerializeField] private float attackMultiplier;
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;
    [Header("水晶替代克隆")]
    [SerializeField] private skilltreeSlot_UI crystalInseadUnlockButton;
    [SerializeField] private bool crystalInseadOfClone ;
    protected override void Start()
    {
        base.Start();
        cloneAttackUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneAttack);
        aggressiveCloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockAggressiveClone);
        multipleUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMultiClone);
        crystalInseadUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalInstead);
    }
    public void CreateClone(Transform _clonePos,Vector3 _offset)
    {

        if (crystalInseadOfClone )
        {
            SkillManager.instance.crystal.CreateCrystal();
            return;
        }
        GameObject newClone = Instantiate(clonePreFab);
        newClone.GetComponent<Clone_Skill_Controller>().SetUpClone(_clonePos,cloneDuration,colorLossSpeed,canAttack,_offset,FindClosestEnemy(newClone.transform),canDuplicateClone,chanceToDuplicate);
    }
    
    public void CreateCloneWithDelay(Transform _enemyTransform)
    {
        
        
            StartCoroutine(CreateCloneDelayCor(_enemyTransform, new Vector3(2 * player.faceDir, 0)));
        
    }
    private IEnumerator CreateCloneDelayCor(Transform _enemyTrans,Vector3 offset)
    {
        yield return new WaitForSeconds(0.4f);
        CreateClone(_enemyTrans, offset);
    }
    private void UnlockCloneAttack()
    {
        if (cloneAttackUnlockButton.unlocked)
        {
            canAttack = true;
            
        }
    }

    private void UnlockAggressiveClone()
    {
        if (aggressiveCloneUnlockButton.unlocked)
        {
           
        }
    }

    private void UnlockMultiClone()
    {
        if (multipleUnlockButton.unlocked)
        {
            canDuplicateClone = true;
           
        }
    }

    private void UnlockCrystalInstead()
    {
        if (crystalInseadUnlockButton.unlocked)
        {
            crystalInseadOfClone = true;
        }
    }

}
