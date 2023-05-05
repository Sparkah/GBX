using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Andrix.Assets.Skillbox_6.Scripts.UI
{
    public class StorytellerMini : MonoBehaviour
    {
        //hardCode be like:

        [SerializeField]
        private Image storyImageRenderer;

        [SerializeField]
        private TMPro.TextMeshProUGUI srotyText;

        [Header("Story")]
        [SerializeField]
        private Button storySkipButton;

        [SerializeField]
        private List<Story> storiesList = new();

        [SerializeField]
        private UnityEvent onStoryEnds = new();

        private int _storyIndex = -1;
        private bool _isStoryOn;
        private float _timer = 0;
        private readonly float _storyDelay = .5f;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _storyIndex = -1;
            ShowSkip(true);
            _isStoryOn = true;
            NextStory();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isStoryOn && Time.time > _timer)
            {
                _timer = Time.time + _storyDelay;
                NextStory();
            }
        }

        private void SetStory(in Story story)
        {
            srotyText.text = story.GetText;
            storyImageRenderer.sprite = story.GetSprite;
        }

        private void NextStory()
        {
            if (_storyIndex < storiesList.Count - 1)
            {
                _storyIndex++;
                SetStory(storiesList[_storyIndex]);
            }
            else
            {
                onStoryEnds?.Invoke();
                ShowSkip(false);
                _isStoryOn = false;
            }
        }

        private void ShowSkip(bool value)
        {
            srotyText.gameObject.transform.parent.gameObject.SetActive(value);// sorry =(
            srotyText.gameObject.SetActive(value);

            storySkipButton.gameObject.SetActive(value);
        }

        public void SkipStory()
        {
            _storyIndex = storiesList.Count;
            SetStory(storiesList[storiesList.Count - 1]);
            NextStory();
            ShowSkip(false);
        }

        [System.Serializable]
        private class Story
        {
            [SerializeField]
            private Sprite sprite;

            [SerializeField]
            private string text;

            public Sprite GetSprite => sprite;
            public string GetText => text;
        }
    }
}