using Assets.Input;
using UI.Mobile;
using UnityEngine;

namespace GameManagement
{
    public class DeviceTypeAdjuter : MonoBehaviour
    {
        public InputHandler Input => _input;

        public ObjectsConfigsGetter ConfigsGetter => _configsGetter;

        [SerializeField]
        private InputsConfig _inputsConfig;

        [SerializeField]
        private GameObject _mobileControls;

        [SerializeField]
        private RectTransform _safeAreaPanel;

        [SerializeField]
        private PrefabsConfigsData _objectsConfigs;

#if UNITY_EDITOR
        [SerializeField]
        private DeviceType _editorModeDevice;
#endif

        private SafeAreaAdjuster _safeAreaAdjuster;

        private InputHandler _input;

        private ObjectsConfigsGetter _configsGetter;

        public void Init()
        {
            _objectsConfigs.Init();

            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                SetupPC();
            }
            else if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                SetupMobile();
            }

            var deviceType = SystemInfo.deviceType;

#if UNITY_EDITOR

            deviceType = _editorModeDevice;

            if (_editorModeDevice == DeviceType.Handheld)
            {
                SetupMobile();
            }
            else
            {
                SetupPC();
            }
#endif

            _configsGetter = new ObjectsConfigsGetter(deviceType, _objectsConfigs);
        }

        private void SetupPC()
        {
            _mobileControls.SetActive(false);
            _input = Instantiate<InputHandler>(_inputsConfig.PCPrefab, transform);
        }

        private void SetupMobile()
        {
            _mobileControls.SetActive(true);
            _input = Instantiate<InputHandler>(_inputsConfig.MobilePrefab, transform);

            _safeAreaAdjuster = new SafeAreaAdjuster(_safeAreaPanel);
            _safeAreaAdjuster.AdjustSafeArea();
        }
    }
}