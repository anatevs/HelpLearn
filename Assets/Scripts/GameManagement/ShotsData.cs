using System;

namespace GameManagement
{
    [Serializable]
    public struct ShotsData
    {
        public int Hits;
        public int Shots;

        public ShotsData(int hits, int shots)
        {
            Hits = hits;
            Shots = shots;
        }
    }
}