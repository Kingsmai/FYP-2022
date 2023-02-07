using CraftsmanHero;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public delegate void GameManagerEventHandler();
    public event GameManagerEventHandler OnGoldChange;
    public event GameManagerEventHandler OnExperienceChange;
    
    public Player CurrentPlayer;

    int experience;

    public int Experience {
        get { return experience; }
        set {
            experience = value;
            OnExperienceChange?.Invoke();
        }
    }

    int gold;

    public int Gold {
        get {
            return gold;
        }
        set {
            gold = value;
            OnGoldChange?.Invoke();
        }
    }

    protected override void Awake() {
        base.Awake();
        
        // TODO: 加载存档
        Gold = 0;
    }

    public void SetCurrentPlayer(GameObject player) {
        CurrentPlayer = player.GetComponent<Player>();
    }

    public void AddExperience(int expAmount) {
        Experience += expAmount;
    }

    public void ObtainGold(int goldAmount) {
        Gold += goldAmount;
    }
}