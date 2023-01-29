using CraftsmanHero;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public Player CurrentPlayer;

    public delegate void GameManagerEventHandler();

    public event GameManagerEventHandler OnGoldChange;

    int goldAmount;

    public int GoldAmount {
        get {
            return goldAmount;
        }
        set {
            goldAmount = value;
            OnGoldChange?.Invoke();
        }
    }

    protected override void Awake() {
        base.Awake();
        
        // TODO: 加载存档
        GoldAmount = 0;
    }

    public void SetCurrentPlayer(GameObject player) {
        CurrentPlayer = player.GetComponent<Player>();
    }
}