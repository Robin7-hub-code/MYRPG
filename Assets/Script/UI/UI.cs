using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skilltreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionsUI;



    void Start()
    {
        SwitchTo(null);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            SwitchWithKeyto(characterUI);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            SwitchWithKeyto(skilltreeUI);
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            SwitchWithKeyto(craftUI);
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            SwitchWithKeyto(optionsUI);
        }
    }

    public void SwitchTo(GameObject _menu)
    {
        for(int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        if(_menu!=null)
        {
            _menu.SetActive(true);
        }
    }

    public void SwitchWithKeyto(GameObject _menu)
    {
        if(_menu!=null&&_menu.activeSelf)
        {
            _menu.SetActive(false);
            return;
        }
        SwitchTo(_menu);
    }
}
