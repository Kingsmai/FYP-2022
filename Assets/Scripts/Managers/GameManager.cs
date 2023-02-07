using CraftsmanHero;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public Player CurrentPlayer;

    [Header("UI Stuffs")] public GameObject damageTextPrefab;
    public GameObject recoverTextPrefab;

    [Header("Game Item Stuffs")] public GameObject gameItemPrefab;

    protected override void Awake() {
        base.Awake();
        
        // TODO: 读取存档
        CurrentPlayer.Gold = 0;
    }

    public void SetCurrentPlayer(GameObject player) {
        CurrentPlayer = player.GetComponent<Player>();
    }
}