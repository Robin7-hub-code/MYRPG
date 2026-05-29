using TMPro;
using UnityEngine;

public class Black_Hole_key_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode myHotKey;
    private TextMeshProUGUI myText;
    private Transform enemyTrans;
    private Black_hole_skill_controller blackHole;

    
    

    public void SetupHotKey(KeyCode _myHotKey,Transform _enemyTrans,Black_hole_skill_controller _BlackHole)
    {
        sr=GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        enemyTrans = _enemyTrans;
        blackHole=_BlackHole;

        myHotKey = _myHotKey;
        myText.text=myHotKey.ToString();
    }
    private void Update()
    {
        
        
        if(Input.GetKeyDown(myHotKey))
        {
           
            blackHole.AddEnemyTolist(enemyTrans);
            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
