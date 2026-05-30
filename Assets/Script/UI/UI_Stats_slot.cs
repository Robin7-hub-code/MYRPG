using TMPro;
using UnityEngine;

public class UI_Stats_slot : MonoBehaviour
{
    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statNameText;
    [SerializeField] private TextMeshProUGUI statValueText;

    private void OnValidate()
    {
        gameObject.name = "Stat-" + statName;

        if(statNameText!=null)
        {
            statNameText.text = statName;
        }
    }
    void Start()
    {
        UpdateStatValueUI();
    }

    // Update is called once per frame
   
    public void UpdateStatValueUI()
    {
        PlayerStats playerStat = PlayerManager.instance.player.GetComponent<PlayerStats>();
        if(playerStat!=null)
        {
            statValueText.text=playerStat.GetStats(statType).GetValue().ToString();
        }
    }
}
