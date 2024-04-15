using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchClothes : MonoBehaviour
{
    [SerializeField] private List<Sprite> _headClothesImages;
    [SerializeField] private List<Sprite> _bodyClothesImages;
    [SerializeField] private List<Sprite> _legsClothesImages;

    [SerializeField] private Button _nextHeadClothes;
    [SerializeField] private Button _previousHeadClothes;
    [SerializeField] private Button _nextBodyClothes;
    [SerializeField] private Button _previousBodyClothes;
    [SerializeField] private Button _nextLegsClothes;
    [SerializeField] private Button _previousLegsClothes;

    [SerializeField] private Image _displayImageHeadClothes;
    [SerializeField] private Image _displayImageBodyClothes;
    [SerializeField] private Image _displayImageLegsClothes;

    [SerializeField] private Image _displayImageHead;
    [SerializeField] private Image _displayImageBody;
    [SerializeField] private Image _displayImageLegs;
    
    [SerializeField] private GameObject _clothes;
    [SerializeField] private GameObject _clothesUnderDemon;

    public Sprite ImageHeadClothes => _headClothesImages[_currentIndexHead];
    public Sprite ImageBodyClothes => _bodyClothesImages[_currentIndexBody];
    public Sprite ImageLegsClothes => _legsClothesImages[_currentIndexLegs];

    private int _currentIndexHead = 0;
    private int _currentIndexBody = 0;
    private int _currentIndexLegs = 0;

    private void Start()
    {
        _nextHeadClothes.onClick.AddListener(() => ChangeImage(_headClothesImages, _displayImageHeadClothes, ref _currentIndexHead, 1));
        _previousHeadClothes.onClick.AddListener(() => ChangeImage(_headClothesImages, _displayImageHeadClothes, ref _currentIndexHead, -1));
        _nextBodyClothes.onClick.AddListener(() => ChangeImage(_bodyClothesImages, _displayImageBodyClothes, ref _currentIndexBody, 1));
        _previousBodyClothes.onClick.AddListener(() => ChangeImage(_bodyClothesImages, _displayImageBodyClothes, ref _currentIndexBody, -1));
        _nextLegsClothes.onClick.AddListener(() => ChangeImage(_legsClothesImages, _displayImageLegsClothes, ref _currentIndexLegs, 1));
        _previousLegsClothes.onClick.AddListener(() => ChangeImage(_legsClothesImages, _displayImageLegsClothes, ref _currentIndexLegs, -1));

        UpdateDisplayImages();
    }

    private void ChangeImage(List<Sprite> images, Image displayImage, ref int currentIndex, int direction)
    {
        currentIndex = (currentIndex + direction + images.Count) % images.Count;
        displayImage.sprite = currentIndex >= 0 ? images[currentIndex] : null;

        if (images == _bodyClothesImages & _currentIndexBody == 3)
            _displayImageBodyClothes.transform.SetParent(_clothesUnderDemon.transform);
        else if (images == _bodyClothesImages)
            _displayImageBodyClothes.transform.SetParent(_clothes.transform);

        if (images == _legsClothesImages & _currentIndexLegs == 3)
            _displayImageLegsClothes.transform.SetParent(_clothesUnderDemon.transform);
        else if (images == _legsClothesImages)
            _displayImageLegsClothes.transform.SetParent(_clothes.transform);
    }

    private void UpdateDisplayImages()
    {
        _displayImageHeadClothes.sprite = _currentIndexHead >= 0 ? _headClothesImages[_currentIndexHead] : null;
        _displayImageBodyClothes.sprite = _currentIndexBody >= 0 ? _bodyClothesImages[_currentIndexBody] : null;
        _displayImageLegsClothes.sprite = _currentIndexLegs >= 0 ? _legsClothesImages[_currentIndexLegs] : null;
    }

    public void ResetDisplayImages()
    {
        _displayImageHeadClothes.sprite = _headClothesImages[0];
        _displayImageBodyClothes.sprite = _bodyClothesImages[0];
        _displayImageLegsClothes.sprite = _legsClothesImages[0];
        _currentIndexHead = 0;
        _currentIndexBody = 0;
        _currentIndexLegs = 0;
    }

    public void SetBodyParts(Sprite imageHead, Sprite imageBody, Sprite imageLegs)
    {
        _displayImageHead.sprite = imageHead;
        _displayImageBody.sprite = imageBody;
        _displayImageLegs.sprite = imageLegs;
    }
}
