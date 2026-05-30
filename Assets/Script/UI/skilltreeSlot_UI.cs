using UnityEngine;
using UnityEngine.UI;

public class skilltreeSlot_UI : MonoBehaviour
{
    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;
    [SerializeField] private Color  lockedSkillColor;

    public bool unlocked;
    [SerializeField] private skilltreeSlot_UI[] shouldBeUnlocked;//处理不可共存技能
    [SerializeField] private skilltreeSlot_UI[]  shouldBelocked;//处理前置技能


    [SerializeField] private Image skillImage;
    private void OnValidate()
    {
        gameObject.name="SkillTreeSlot-"+skillName;
    }
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSlot());
        
    }
    private void Start()
    {
        skillImage=GetComponent<Image>();

        skillImage.color= lockedSkillColor;

    }

    public void UnlockSlot()
    {
        
        for(int i=0;i<shouldBeUnlocked.Length;i++)
        {
            if (shouldBeUnlocked[i].unlocked==false)
            {
                return;
            }
        }
        for(int i=0;i<shouldBelocked.Length;i++)
        {
            if (shouldBelocked[i].unlocked==true)
            {
                return;
            }
        }

        unlocked = true;
        Debug.Log("UnLock is" + unlocked);
        
        skillImage.color = Color.white; 
    }
}
