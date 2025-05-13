[System.Serializable]
public class ScoreEntry
{
    public ScoreType scoreType;
    public int value;

    public ScoreEntry(ScoreType type, int val)
    {
        scoreType = type;
        value = val;    
    }
}