using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash Fix")]
    [SerializeField] private Material hitMat;
    [Header("Flash Time")]
    [SerializeField] private float flashTime;
    private Material originalMat;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMat=sr.material;
    }
    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(flashTime);
        sr.material = originalMat;
    }
    private void RedColorBlink()
    {
        if(sr.color!=Color.white)
            sr.color = Color.white;
        else sr.color = Color.red;
    }    
    private void CancelColorBlink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
