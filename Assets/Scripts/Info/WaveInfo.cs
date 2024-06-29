public class WaveInfo
{
    public WaveInfo(int id, int[] enemies)
    {
        Id = id;
        Enemies = enemies;
    }

    public int Id { get; private set; }
    public int[] Enemies { get; private set; }
}