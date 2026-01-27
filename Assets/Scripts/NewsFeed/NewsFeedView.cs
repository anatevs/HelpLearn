using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NewsFeed
{
    public class NewsFeedView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _newsText;

        [SerializeField]
        private NewsShowConfig _showConfig;

        [SerializeField]
        private Button _showAllButton;

        private float _showDelay = 2f;

        private Coroutine _showCoroutine;

        private List<NewsItem> _currentShowing;

        private string _currentErrors = "";

        private void Awake()
        {
            if (_showConfig == null)
            {
                Debug.LogWarning("no NewsShowConfig, a default will be using");
                _showConfig = ScriptableObject.CreateInstance<NewsShowConfig>();
            }
        }

        private void OnEnable()
        {
            _showAllButton.onClick.AddListener(ShowAll);
        }

        private void OnDisable()
        {
            _showAllButton.onClick.RemoveListener(ShowAll);
        }

        public void ShowNewsAndErrors(List<NewsItem> news, List<string> errors)
        {
            _currentErrors = _showConfig.GetErrosMessage(errors);

            _currentShowing = news;
            _showCoroutine = StartCoroutine(ShowNewsCoroutine());
        }

        public void ShowString(string line)
        {
            _newsText.text = line;
        }

        private void ShowAll()
        {
            if (_showCoroutine != null && _currentShowing != null)
            {
                StopCoroutine(_showCoroutine);
                _showCoroutine = null;
            }

            var stringAll = "";
            foreach (var news in _currentShowing)
            {
                stringAll = $"{stringAll}{_showConfig.GetNewsString(news)}\n";
            }

            ShowString($"{stringAll}{_currentErrors}");
        }

        private IEnumerator ShowNewsCoroutine()
        {
            foreach (var item in _currentShowing)
            {
                ShowString(_showConfig.GetNewsString(item));

                yield return new WaitForSeconds(_showDelay);
            }

            if (_currentErrors != "")
            {
                ShowString(_currentErrors);
            }
        }
    }
}