public class StageInfo
{
    public StageInfo(int id, float waveTerm, int[] waves)
    {
        Id = id;
        WaveTerm = waveTerm;
        Waves = waves;
    }

    public int Id { get; private set; }
    public float WaveTerm { get; private set; }
    public int[] Waves { get; private set; }
}