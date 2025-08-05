using _Asset.Scripts.MyAsset;

public interface ITowerUnlockHandler
{
    void OnTowerUnlocked(TowerType type, int index);
    void OnUnlockFailed(TowerType type);
}