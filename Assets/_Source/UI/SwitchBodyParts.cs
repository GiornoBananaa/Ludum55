using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBodyParts : MonoBehaviour
{
    [SerializeField] private List<Sprite> _headImages; 
    [SerializeField] private List<Sprite> _bodyImages; 
    [SerializeField] private List<Sprite> _legsImages;

    [SerializeField] private Button _nextHead;
    [SerializeField] private Button _previousHead;
    [SerializeField] private Button _nextBody;
    [SerializeField] private Button _previousBody;
    [SerializeField] private Button _nextLegs;
    [SerializeField] private Button _previousLegs;

    [SerializeField] private Image _displayImageHead;
    [SerializeField] private Image _displayImageBody;
    [SerializeField] private Image _displayImageLegs;

    public Sprite ImageHead => _headImages[_currentIndexHead];
    public Sprite ImageBody => _bodyImages[_currentIndexBody];
    public Sprite ImageLegs => _legsImages[_currentIndexLegs];

    private int _currentIndexHead = 0; 
    private int _currentIndexBody = 0; 
    private int _currentIndexLegs = 0;

    private void Start()
    {
        if (_headImages.Count == 0 || _bodyImages.Count == 0 || _legsImages.Count == 0 || _displayImageHead == null || _displayImageBody == null || _displayImageLegs == null)
        {
            Debug.LogError("Необходимо назначить список изображений и компонент изображения!");
            return;
        }

        _nextHead.onClick.AddListener(() => ChangeImage(_headImages, _displayImageHead, ref _currentIndexHead, 1));
        _previousHead.onClick.AddListener(() => ChangeImage(_headImages, _displayImageHead, ref _currentIndexHead, -1));
        _nextBody.onClick.AddListener(() => ChangeImage(_bodyImages, _displayImageBody, ref _currentIndexBody, 1));
        _previousBody.onClick.AddListener(() => ChangeImage(_bodyImages, _displayImageBody, ref _currentIndexBody, -1));
        _nextLegs.onClick.AddListener(() => ChangeImage(_legsImages, _displayImageLegs, ref _currentIndexLegs, 1));
        _previousLegs.onClick.AddListener(() => ChangeImage(_legsImages, _displayImageLegs, ref _currentIndexLegs, -1));

        UpdateDisplayImages();
    }

    private void ChangeImage(List<Sprite> images, Image displayImage, ref int currentIndex, int direction)
    {
        currentIndex = (currentIndex + direction + images.Count) % images.Count;
        displayImage.sprite = images[currentIndex];
    }

    private void UpdateDisplayImages()
    {
        _displayImageHead.sprite = _headImages[_currentIndexHead];
        _displayImageBody.sprite = _bodyImages[_currentIndexBody];
        _displayImageLegs.sprite = _legsImages[_currentIndexLegs];
    }

    public void ResetDisplayImages()
    {
        _displayImageHead.sprite = _headImages[0];
        _displayImageBody.sprite = _bodyImages[0];
        _displayImageLegs.sprite = _legsImages[0];
        _currentIndexHead = 0;
        _currentIndexBody = 0;
        _currentIndexLegs = 0;
    }
}
