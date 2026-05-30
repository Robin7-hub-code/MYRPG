using UnityEngine;
using UnityEngine.UI;

public class ControlImage : MonoBehaviour
{
    public int layer;
    public Image  image;
    public void Awake()
    {
        image = GetComponent<Image>();
    }
    public void Start()
    {
        setLayer(layer);
        Debug.Log("i m set");
    }
    private void setLayer(int layer)
    {
        image.transform.SetSiblingIndex(layer);
    }
}
