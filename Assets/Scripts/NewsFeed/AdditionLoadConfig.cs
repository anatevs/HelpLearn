using UnityEngine;

namespace NewsFeed
{
    [CreateAssetMenu(
        fileName = "AdditionLoadConfig",
        menuName = "Configs/AdditionLoad")]
    public class AdditionLoadConfig : ScriptableObject
    {
        public string[] Filenames => _filenames;
        public string ResourcesPath => _resourcesPath;
        public int ResourcesDelay => _resourcesDelay;
        public int ReadDelay => _readDelay;

        [SerializeField]
        private string[] _filenames;

        [SerializeField]
        private string _resourcesPath = "JsonNews/";

        [SerializeField]
        private int _resourcesDelay = 20000;

        [SerializeField]
        private int _readDelay = 100;
    }
}