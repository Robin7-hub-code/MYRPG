using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PlayerManager instance;
    public Player player;
    private void Awake()
    {
        instance = this;
    }
}
