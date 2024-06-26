public class WaveInfo
{
    public int Id { get; private set; }
    public int[] Enemies { get; private set; }

    public WaveInfo(int id, int[] enemies)
    {
        Id = id;
        Enemies = enemies;
    }
}