using System.Collections.Generic;
using AudioSystem;
using TaskSystem;
using UnityEngine;
using UnityEngine.UI;

public class SwitchClothes : MonoBehaviour
{
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

    public Sprite ImageHeadClothes => _demonParts.HeadClothes[_currentIndexHead].Sprite;
    public Sprite ImageBodyClothes => _demonParts.BodyClothes[_currentIndexBody].Sprite;
    public Sprite ImageLegsClothes => _demonParts.LegsClothes[_currentIndexLegs].Sprite;

    private int _currentIndexHead = 0;
    private int _currentIndexBody = 0;
    private int _currentIndexLegs = 0;
    private DemonParts _demonParts;
    private AudioPlayer _audioPlayer;

    public void Construct(DemonParts demonParts, AudioPlayer audioPlayer)
    {
        _demonParts = demonParts;
        _audioPlayer = audioPlayer;
    }
    
    private void Start()
    {
        _nextHeadClothes.onClick.AddListener(() => ChangeImage(_demonParts.HeadClothes, _displayImageHeadClothes, ref _currentIndexHead, 1));
        _previousHeadClothes.onClick.AddListener(() => ChangeImage(_demonParts.HeadClothes, _displayImageHeadClothes, ref _currentIndexHead, -1));
        _nextBodyClothes.onClick.AddListener(() => ChangeImage(_demonParts.BodyClothes, _displayImageBodyClothes, ref _currentIndexBody, 1));
        _previousBodyClothes.onClick.AddListener(() => ChangeImage(_demonParts.BodyClothes, _displayImageBodyClothes, ref _currentIndexBody, -1));
        _nextLegsClothes.onClick.AddListener(() => ChangeImage(_demonParts.LegsClothes, _displayImageLegsClothes, ref _currentIndexLegs, 1));
        _previousLegsClothes.onClick.AddListener(() => ChangeImage(_demonParts.LegsClothes, _displayImageLegsClothes, ref _currentIndexLegs, -1));

        UpdateDisplayImages();
    }
    
    private void ChangeImage(DemonPart[] demonParts, Image displayImage, ref int currentIndex, int direction)
    {
        _audioPlayer.Play(Sounds.SwitchClothes);
        currentIndex = (currentIndex + direction + demonParts.Length) % demonParts.Length;
        displayImage.sprite = currentIndex >= 0 ? demonParts[currentIndex].Sprite : null;

        if (demonParts == _demonParts.BodyClothes & _currentIndexBody == 3)
            displayImage.transform.SetParent(_clothesUnderDemon.transform);
        else if (demonParts == _demonParts.BodyClothes)
            displayImage.transform.SetParent(_clothes.transform);

        if (demonParts == _demonParts.LegsClothes & _currentIndexLegs == 3)
            displayImage.transform.SetParent(_clothesUnderDemon.transform);
        else if (demonParts == _demonParts.LegsClothes)
            displayImage.transform.SetParent(_clothes.transform);
    }

    private void UpdateDisplayImages()
    {
        _displayImageHeadClothes.sprite = _currentIndexHead >= 0 ? _demonParts.HeadClothes[_currentIndexHead].Sprite : null;
        _displayImageBodyClothes.sprite = _currentIndexBody >= 0 ? _demonParts.BodyClothes[_currentIndexBody].Sprite : null;
        _displayImageLegsClothes.sprite = _currentIndexLegs >= 0 ? _demonParts.LegsClothes[_currentIndexLegs].Sprite : null;
    }

    public void ResetDisplayImages()
    {
        _displayImageHeadClothes.sprite = _demonParts.HeadClothes[0].Sprite;
        _displayImageBodyClothes.sprite = _demonParts.BodyClothes[0].Sprite;
        _displayImageLegsClothes.sprite = _demonParts.LegsClothes[0].Sprite;
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
