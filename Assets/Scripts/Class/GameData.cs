[System.Serializable]
public class GameData
{
    public int levelReached;
    public bool editionUnlocked;

    public GameData(int levelReached, bool editionUnlocked)
    {
        this.levelReached = levelReached;

        if (editionUnlocked)
        {
            this.editionUnlocked = true;
            return;
        }

        if(levelReached >= GeneralManager._levelToReachToUnlockLevelCreation) 
            this.editionUnlocked = true;
    }
}
