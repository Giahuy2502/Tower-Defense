namespace _Asset.Scripts.MyAsset
{
    public enum GameState { Start, Game, End }
    public enum TowerType {Runeblast,Catapult,Cannon,Crossbow}
    public enum MonsterType {Minion,Warrior,Rouge,Mage}
    public enum MonsterState {Normal,Die,}
    public enum EventName{UpdateGoldTxt,UpdateMonsterTxt,SelectTower,WinGame,LoseGame,StartGame,EndGame}
    public enum MapTag{Road,Tower,Obstacle}
}