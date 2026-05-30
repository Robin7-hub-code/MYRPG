using UnityEngine;
using UnityEngine.UI;
public enum SwordType
{
    Regular,
    Bounce,
    Spin,
    Pierce
}
public class Sword_Skill : Skill
{
    public SwordType swordType=SwordType.Regular;
    [Header("剑销毁时间")]
    [SerializeField] private float destroyTime=3.0f; 

    [Header("时间冻结")]
    [SerializeField] private float freezeTimeDuration;
    [Header("剑")]
    [SerializeField] private skilltreeSlot_UI swordUnlockButton;
    public bool swordUnlocked { get; private set; }
    [SerializeField] private GameObject swordPreFab;
    [SerializeField] private Vector2 launchDir;
    [SerializeField] private float swordGravity;
    [SerializeField] private float returnSpeed = 22;
    [SerializeField] private float disapearDistance = 0.1f;

    [Header("剑弹射信息")]
    [SerializeField] private skilltreeSlot_UI bounceUnlockButton;
    [SerializeField] private float bounceSpeed;
    [SerializeField] private int numOfBounce;
    [SerializeField] private float bounceGravity;
    private Vector2 finalDir;

    [Header("剑串射信息")]
    [SerializeField] private skilltreeSlot_UI pierceUnlockButton;
    [SerializeField] private int numOfPierce;
    [SerializeField] private float pierceGravity;

    [Header("剑旋转信息")]
    [SerializeField] private skilltreeSlot_UI spinUnlockButton;
    [SerializeField] private float maxDis;
    [SerializeField] private float spinGravity;
    [SerializeField] private float spinDuration;
    [SerializeField] private float hitCoolDown=0.35f;
    [SerializeField] private float spinMoveSpeed = 1.5f;

    [Header("投掷辅助线")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    [Header("剑技被动")]
    [SerializeField] private skilltreeSlot_UI timeStopUnlockButton;
    public bool timeStopUnlocked{ get; private set; }
    [SerializeField] private skilltreeSlot_UI vulnerableUnlockButton;
    public bool vulnerableUnlocked { get; private set; } 

    private GameObject[] dots;

    protected override void  Start()
    {
        base.Start();
        GenerateDots();
        SetGravity();
        swordUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSword);
        bounceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBounceSword);
        pierceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockPierceSword);
        spinUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSpinSword);
        timeStopUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockTimeStop);
        vulnerableUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockVulnerable);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            finalDir = new Vector2(AimDirection().normalized.x*launchDir.x, AimDirection().normalized.y*launchDir.y);
        }
        if(Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPos(i * spaceBetweenDots);
            }
        }
    }
    private void SetGravity()
    {
        if(swordType==SwordType.Bounce)
        {
            swordGravity = bounceGravity;
        }
        if(swordType==SwordType.Pierce)
        {
            swordGravity= pierceGravity;
        }
        if(swordType==SwordType.Spin)
        {
            swordGravity= spinGravity;
        }
    }
    public void CreateSword()
    {
        GameObject newSword=Instantiate(swordPreFab,player.transform.position,transform.rotation);
        Sword_Skiill_Controller newSwordScript=newSword.GetComponent<Sword_Skiill_Controller>();

        if (swordType == SwordType.Bounce)
        {
            newSwordScript.SetupBounce(true, numOfBounce,bounceSpeed);
        }
        else if (swordType == SwordType.Pierce)
        {
            newSwordScript.SetUpPierce(true,numOfPierce);
        }
        else if (swordType == SwordType.Spin)
        {
            newSwordScript.SetUpSpin(true,maxDis,spinDuration,hitCoolDown,spinMoveSpeed);
        }
        
        newSwordScript.SetUpSword(finalDir, swordGravity,freezeTimeDuration,returnSpeed,disapearDistance,destroyTime);
        player.AssignSword(newSword);
        DotsActive(false);
    }
    #region 瞄准
    public Vector2 AimDirection()
    {
        Vector2 playerPos=player.transform.position; 
        Vector2 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePosition - playerPos;
        return dir;
    }
    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }
    public void GenerateDots()
    {
        dots=new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i]=Instantiate(dotPrefab,player.transform.position,Quaternion.identity,dotsParent);
            dots[i].SetActive(false);
        }
    }
    private Vector2 DotsPos(float t)
    {
        Vector2 pos=(Vector2)player.transform.position+new Vector2(AimDirection().normalized.x * launchDir.x, AimDirection().normalized.y * launchDir.y)*t+0.5f*Physics2D.gravity*swordGravity*t*t;
        return pos;
    }
    #endregion
    private void UnlockTimeStop()
    {
        if (timeStopUnlockButton.unlocked)
            timeStopUnlocked = true;
    }

    private void UnlockVulnerable()
    {
        if (vulnerableUnlockButton.unlocked)
            vulnerableUnlocked = true;
    }

    private void UnlockSword()
    {
        if (swordUnlockButton.unlocked)
        {
            swordType = SwordType.Regular;
            swordUnlocked = true;
        }
    }

    private void UnlockBounceSword()
    {
        if (bounceUnlockButton.unlocked)
            swordType = SwordType.Bounce;
    }

    private void UnlockPierceSword()
    {
        if (pierceUnlockButton.unlocked)
            swordType = SwordType.Pierce;
    }

    private void UnlockSpinSword()
    {
        if (spinUnlockButton.unlocked)
            swordType = SwordType.Spin;
    }
}
