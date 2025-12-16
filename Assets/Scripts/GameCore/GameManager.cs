using System.Collections;
using TMPro;
using UnityEngine;

namespace GameCore
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        private TargetSpawner _targetSpawner;

        [SerializeField]
        private TMP_Text _countText;

        private float _spawnDelay = 4f;

        private float _startDelay = 1f;

        private void Start()
        {
            SetCountText();

            StartCoroutine(SpawnTargets());
        }

        private IEnumerator SpawnTargets()
        {
            yield return new WaitForSeconds(_startDelay);

            while (gameObject.activeSelf)
            {
                _targetSpawner.SpawnTarget();

                SetCountText();

                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        private void SetCountText()
        {
            _countText.text = _targetSpawner.TargetsCount.ToString();
        }
    }
}