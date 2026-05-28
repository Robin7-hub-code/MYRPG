using UnityEngine;
using JetBrains.Annotations;
using Mono.Cecil.Cil;
using System.Collections;
public class Clone_Skill : Skill
{
    [SerializeField] private GameObject clonePreFab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private float colorLossSpeed;
    [Space]
    [SerializeField] private bool canAttack;

    [SerializeField] private bool createCloneOnDashStart;
    [SerializeField] private bool createCloneOnDashOver;
    [SerializeField] private bool canCreateCloneOnCounterAttack;
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;
    [Header("Ë®¾§Ìæ´ú¿ËÂ¡")]
    [SerializeField] private bool crystalInsteadClone;
    public void CreateClone(Transform _clonePos,Vector3 _offset)
    {

        if (crystalInsteadClone)
        {
            SkillManager.instance.crystal.CreateCrystal();
            return;
        }
        GameObject newClone = Instantiate(clonePreFab);
        newClone.GetComponent<Clone_Skill_Controller>().SetUpClone(_clonePos,cloneDuration,colorLossSpeed,canAttack,_offset,FindClosestEnemy(newClone.transform),canDuplicateClone,chanceToDuplicate);
    }
    public void CreateCloneOnDashStart()
    {
        if(createCloneOnDashStart)
        {
            CreateClone(player.transform, Vector3.zero);
        }
    }
    public void CreateCloneOnDashOver()
    {
        if(createCloneOnDashOver)
        {
            CreateClone(player.transform, Vector3.zero);
        }
    }
    public void CreateCloneOnCounterAttack(Transform _enemyTransform)
    {
        if (canCreateCloneOnCounterAttack)
        {
            StartCoroutine(CreateCloneDelay(_enemyTransform, new Vector3(2 * player.faceDir, 0)));
        }
    }
    private IEnumerator CreateCloneDelay(Transform _enemyTrans,Vector3 offset)
    {
        yield return new WaitForSeconds(0.4f);
        CreateClone(_enemyTrans, offset);
    }
}
