using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Black_hole_skill_controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("敌人列表")]
    [SerializeField]private List<Transform> targets=new List<Transform>();//目标敌人
    [Header("热键列表")]
    [SerializeField] private GameObject HotkeyPreFab;
    [SerializeField] private List<KeyCode> KeyCodesList;
    private float maxSize;
    private float growSpeed;
    private float shrinkSpeed;
    private bool canGrow=true;
    private bool canShrink;
    private float blackholeTimer;

    private bool playerCanDisapear=true;

    private bool canAttack;
    private bool canCreateHotKey = true;

    private float cloneAttackCoolDown = 0.3f;
    private float cloneAttackTimer;
    private int numOfAttack = 4;

    private bool cloneAttackReleased=true;

    private List<GameObject> createdHotKey=new List<GameObject>();

    public bool playerCanExitState { get; private set; }

    public void SetupBlackHole(float _maxsize,float _growSpeeed,float _shrinkSpeed,int _numOfAttack,float _cloneAttackCoolDown,float _blackholeDuration)
    {
        maxSize = _maxsize;
        growSpeed = _growSpeeed;
        shrinkSpeed = _shrinkSpeed;
        growSpeed = _growSpeeed;
        numOfAttack=_numOfAttack;
        cloneAttackCoolDown=_cloneAttackCoolDown;

        blackholeTimer = _blackholeDuration;
    }

    private void Update()
    {

        
        blackholeTimer-=Time.deltaTime;

        if(blackholeTimer<0)
        {
            blackholeTimer=Mathf.Infinity;
            if (targets.Count >0)
            {
                ReleaseCloneAttack();
            }
            else
            {
                FinishBlackAt();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();
        }
        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);

        }
        CloneAttackLogic();
        if (canShrink)
        {
            Debug.Log("I m shrink");
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
                canShrink = false;
            }
        }
    }

    private void ReleaseCloneAttack()
    {
        if(targets.Count<=0)
        {
            return;
        }


        DestroyHotKeys();
        canAttack = true;
        cloneAttackReleased = true;
        canCreateHotKey = false;
        Debug.Log("I m transparent");

        if(playerCanDisapear)
        {
          playerCanDisapear = false;
          PlayerManager.instance.player.MakeTransparent(true);
        }
    }

    private void CloneAttackLogic()
    {
        cloneAttackTimer -= Time.deltaTime;
        if (cloneAttackTimer < 0 && canAttack && numOfAttack > 0&&cloneAttackReleased)
        {
            cloneAttackTimer = cloneAttackCoolDown;
            int randomIndex = Random.Range(0, targets.Count);

            float x_offset;
            if (Random.Range(0, 100) > 50)
            {
                x_offset = 2f;
            }
            else
            {
                x_offset = -2f;
            }
            SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(x_offset, 0));
            numOfAttack--;
            if (numOfAttack <= 0)
            {
                Invoke("FinishBlackAt", 0.8f);
            }
        }
    }

    private void FinishBlackAt()
    {
        DestroyHotKeys();
        playerCanExitState = true;
        canShrink = true;
        cloneAttackReleased = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>()!=null)
        {
            collision.GetComponent<Enemy>().FreezeTimer(true);
            bool flowControl = CreateHotKey(collision);
            if (!flowControl)
            {
                return;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>()!=null)
        {
            collision.GetComponent<Enemy>().FreezeTimer(false);
           
        }
    }
    private void DestroyHotKeys()
    {
        if(createdHotKey.Count<=0)
        {
            return;
        }
        for(int i=0;i<createdHotKey.Count; i++)
        {
            Destroy(createdHotKey[i]);
        }
    }
    private bool CreateHotKey(Collider2D collision)
    {
        if (HotkeyPreFab == null)
        {
            Debug.LogError("HotkeyPreFab is not assigned on Black_hole_skill_controller.");
            return false;
        }

        if (KeyCodesList == null || KeyCodesList.Count == 0)
        {
            Debug.LogWarning("Black hole has no available hotkeys.");
            return false;
        }

        if(!canCreateHotKey)
        {
            return true;
        }
        GameObject newHotKey = Instantiate(HotkeyPreFab, collision.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        createdHotKey.Add(newHotKey);
        KeyCode chooseKey = KeyCodesList[Random.Range(0, KeyCodesList.Count)];
        KeyCodesList.Remove(chooseKey);
        Black_Hole_key_Controller newHotKeyController = newHotKey.GetComponent<Black_Hole_key_Controller>();
        newHotKeyController.SetupHotKey(chooseKey, collision.transform, this);
        return true;
    }
    public void AddEnemyTolist(Transform _enemyTrans)=>targets.Add(_enemyTrans);
}
