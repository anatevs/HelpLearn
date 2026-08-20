namespace GameTest
{
    public struct SpawnCountData
    {
        public int ActiveCount;
        public int TotalCount;

        public void AddActiveCount(int addAmount)
        {
            ActiveCount += addAmount;
        }
        public void AddTotalCount(int addAmount)
        {
            TotalCount += addAmount;
        }
    }
}