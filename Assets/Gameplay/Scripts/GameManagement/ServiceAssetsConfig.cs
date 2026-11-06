using GameManagement;
using Gameplay;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AssetsLoading
{
    [CreateAssetMenu(fileName = "ServiceAssetsConfig",
        menuName = "Configs/ServiceAssets")]
    public class ServiceAssetsConfig : ScriptableObject
    {
        [SerializeField]
        private TypeAssetData _soundAssetData;

        [SerializeField]
        private TypeAssetData _canvasAssetData;

        [SerializeField]
        private TypeAssetData _enemiesAssetData;

        [SerializeField]
        private TypeAssetData _projectilesAssetData;

        [SerializeField]
        private TypeAssetData _itemsAssetData;

        private readonly Dictionary<Type, AssetReference> _assetReferences = new();

        public AssetReference GetAsset<T>() where T : MonoBehaviour
        {
            if (!_assetReferences.TryGetValue(typeof(T), out var asset))
            {
                Debug.LogError($"No asset of type {typeof(T)} in a {this.name}");
                return null;
            }

            return asset;
        }

        public void Init()
        {
            AddToAssets<SoundPlayer>(_soundAssetData);
            AddToAssets<CanvasView>(_canvasAssetData);
            AddToAssets<EnemySpawnService>(_enemiesAssetData);
            AddToAssets<ProjectileSpawnService>(_projectilesAssetData);
            AddToAssets<ItemsService>(_itemsAssetData);
        }

        private void AddToAssets<T>(TypeAssetData data) where T : MonoBehaviour
        {
            data.Type = typeof(T);
            _assetReferences.Add(data.Type, data.Asset);
        }
    }

    [Serializable]
    public struct TypeAssetData
    {
        public Type Type;
        public AssetReference Asset;
    }
}