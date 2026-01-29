using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NewsFeed
{
    public class NewsFeedView : MonoBehaviour
    {
        public bool IsShowing => _isShowing;

        public bool IsWaiting { get; set; }

        [SerializeField]
        private TMP_Text _newsText;

        [SerializeField]
        private NewsShowConfig _showConfig;

        [SerializeField]
        private SpinnerConfig _spinnerConfig;

        [SerializeField]
        private Button _showAllButton;

        [SerializeField]
        private Button _returnButton;

        private Coroutine _showCoroutine;

        private Coroutine _spinnerCoroutine;

        private List<string> _currentWaiting = new();

        private List<string> _allNewsStrings = new();

        private int _currentIndex = 0;

        private bool _isShowing = false;

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
            _returnButton.onClick.AddListener(ProceedFeedShow);
        }

        private void OnDisable()
        {
            _showAllButton.onClick.RemoveListener(ShowAll);
            _returnButton.onClick.RemoveListener(ProceedFeedShow);
        }

        private void Start()
        {
            RunSpinner();
        }

        public void ShowNews(List<NewsItem> news)
        {
            _currentWaiting.Clear();

            foreach(var item in news)
            {
                var newsString = _showConfig.GetNewsString(item);
                _allNewsStrings.Add(newsString);
                _currentWaiting.Add(newsString);
            }

            _showCoroutine = StartCoroutine(ShowNewsCoroutine(0));
        }

        public void ShowString(string line)
        {
            _newsText.text = line;
        }

        public void RunSpinner()
        {
            _spinnerCoroutine = StartCoroutine(ShowSpinnerCoroutine());
        }

        public void StopSpinner()
        {
            if (_spinnerCoroutine != null)
            {
                StopCoroutine(_spinnerCoroutine);
                _spinnerCoroutine = null;
            }
        }

        private void ProceedFeedShow()
        {
            _showAllButton.gameObject.SetActive(true);
            _returnButton.gameObject.SetActive(false);

            _showCoroutine = StartCoroutine(ShowNewsCoroutine(_currentIndex));
        }

        private void ShowAll()
        {
            _showAllButton.gameObject.SetActive(false);
            _returnButton.gameObject.SetActive(true);

            _isShowing = true;
            StopSpinner();

            if (_showCoroutine != null)
            {
                StopCoroutine(_showCoroutine);
                _showCoroutine = null;
            }

            var stringAll = "";
            foreach (var s in _allNewsStrings)
            {
                stringAll = $"{stringAll}{s}\n";
            }

            ShowString($"{stringAll}");
        }

        private IEnumerator ShowNewsCoroutine(int startIndex)
        {
            StopSpinner();

            _isShowing = true;
            _currentIndex = startIndex;

            for (int i = _currentIndex; i < _currentWaiting.Count; i++)
            {
                ShowString(_currentWaiting[i]);
                _currentIndex++;
                yield return new WaitForSeconds(_showConfig.ShowDelay);
            }

            _isShowing = false;

            if (IsWaiting)
            {
                RunSpinner();
            }
        }

        private IEnumerator ShowSpinnerCoroutine()
        {
            var charSequence = _spinnerConfig.CharSequence;
            var delay = _spinnerConfig.OneCharDelay;

            while (true)
            {
                for (int i = 0; i < charSequence.Length; i++)
                {
                    ShowString(charSequence[i]);

                    yield return new WaitForSeconds(delay);
                }
            }
        }
    }
}