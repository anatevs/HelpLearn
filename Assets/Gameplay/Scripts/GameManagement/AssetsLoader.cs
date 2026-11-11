using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace AssetsLoading
{
    public class AssetsLoader : IDisposable
    {
        private readonly ServiceAssetsConfig _serviceAssets;

        private readonly Dictionary<Type, GameObject> _servicesInstances = new();

        private readonly List<AsyncOperationHandle<GameObject>> _serviceHandles = new();

        private readonly List<AsyncOperationHandle<GameObject>> _levelHandles = new();

        public AssetsLoader(ServiceAssetsConfig serviceAssetsConfig)
        {
            _serviceAssets = serviceAssetsConfig;
            _serviceAssets.Init();
        }

        public void Dispose()
        {
            ReleaseHandles(_serviceHandles);
            ReleaseHandles(_levelHandles);
        }

        public async Task LoadServiceGO<T>() where T : MonoBehaviour
        {
            var assetReference = _serviceAssets.GetAsset<T>();

            var handle = await LoadGO(assetReference);
            var prefab = handle.Result;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                handle.Release();
                throw new Exception($"could not load addressable {assetReference}");
            }

            if (!prefab.TryGetComponent<T>(out _))
            {
                handle.Release();
                throw new Exception($"there is no component of type {typeof(T)} on prefab {prefab.name}");
            }

            var go = GameObject.Instantiate(prefab);

            _servicesInstances.Add(typeof(T), go);

            _serviceHandles.Add(handle);
        }

        public GameObject GetServiceGO<T>() where T : MonoBehaviour
        {
            if (!_servicesInstances.TryGetValue(typeof(T), out var instance))
            {
                throw new Exception($"there is no instance of type {typeof(T)} in {this}");
            }

            return instance;
        }

        public void ClearLevelAssets()
        {
            ReleaseHandles(_levelHandles);
        }

        public async Task<GameObject> LoadLevelGO(AssetReference assetReference)
        {
            var handle = await LoadGO(assetReference);
            var go = handle.Result;

            _levelHandles.Add(handle);

            return go;
        }

        private async Task<AsyncOperationHandle<GameObject>> LoadGO(AssetReference assetReference)
        {
            var handle = assetReference.LoadAssetAsync<GameObject>();

            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                handle.Release();
                throw new Exception($"could not load addressable {assetReference}");
            }

            return handle;
        }

        private async Task<IList<GameObject>> LoadGOByLabel(string label)
        {
            var handle = Addressables.LoadAssetsAsync<GameObject>(label);

            var prefabs = await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                handle.Release();
                throw new Exception($"Unable to load assets with lable {label}");
            }

            handle.Release();

            return prefabs;
        }

        private void ReleaseHandles(List<AsyncOperationHandle<GameObject>> handles)
        {
            foreach (var handle in handles)
            {
                if (handle.IsValid())
                {
                    handle.Release();
                }
            }

            handles.Clear();
        }
    }
}