using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class ParallaxBackground : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    private float startCamPosX;
    private float camTravled;
    private float length;
    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        camTravled = 0;
        xPosition=transform.position.x;
        startCamPosX = cam.transform.position.x;
        length=GetComponent<SpriteRenderer>().bounds.size.x; 
    }
    private void Update()
    {
        camTravled= cam.transform.position.x-startCamPosX;
        float distanceToMove=camTravled*parallaxEffect;
        float diffDistanceInBK_Player =transform.position.x-cam.transform.position.x;
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y,0);
        if (diffDistanceInBK_Player > length)
        {
            xPosition = xPosition - length;
        }
        else if (diffDistanceInBK_Player < -length)
        {
            xPosition = xPosition + length;
        }
    }
}
