using TMPro;
using UnityEngine;

namespace UI
{
    public class SpawnInfoView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _objectsName;

        [SerializeField]
        private TMP_Text _totalValueText;

        [SerializeField]
        private TMP_Text _currentValueText;

        [Header("Pool info")]
        [SerializeField]
        private GameObject[] _poolInfoObjects;

        [SerializeField]
        private TMP_Text _poolSizeText;

        [SerializeField]
        private TMP_Text _inPoolCountText;

        [SerializeField]
        private TMP_Text _repeatUseCountText;

        public void SetObjectsName(string objectsName)
        {
            _objectsName.text = objectsName;
        }

        public void SetTotalAndCurrent(string totalValue, string currentValue)
        {
            _totalValueText.text = totalValue;
            _currentValueText.text = currentValue;
        }

        public void SetTotal(string value)
        {
            _totalValueText.text = value;
        }

        public void SetCurrent(string value)
        {
            _currentValueText.text = value;
        }

        public void ShowPoolInfo(bool isShow)
        {
            for (int i = 0; i < _poolInfoObjects.Length; i++)
            {
                _poolInfoObjects[i].gameObject.SetActive(isShow);
            }
        }

        public void SetPoolSize(string poolSize)
        {
            _poolSizeText.text = poolSize;
        }

        public void SetFreeCount(string freeCount)
        {
            _inPoolCountText.text = freeCount;
        }

        public void SetRepeatUsing(string repeatUsing)
        {
            _repeatUseCountText.text = repeatUsing;
        }
    }
}