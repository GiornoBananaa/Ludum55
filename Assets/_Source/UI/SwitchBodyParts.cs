using AudioSystem;
using TaskSystem;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBodyParts : MonoBehaviour
{
    [SerializeField] private Button _nextHead;
    [SerializeField] private Button _previousHead;
    [SerializeField] private Button _nextBody;
    [SerializeField] private Button _previousBody;
    [SerializeField] private Button _nextLegs;
    [SerializeField] private Button _previousLegs;

    [SerializeField] private Image _displayImageHead;
    [SerializeField] private Image _displayImageBody;
    [SerializeField] private Image _displayImageLegs;
    
    private int _currentIndexHead = 0; 
    private int _currentIndexBody = 0; 
    private int _currentIndexLegs = 0;
    private DemonParts _demonParts;
    private AudioPlayer _audioPlayer;
    
    public Sprite ImageHead => _demonParts.Head[_currentIndexHead].Sprite;
    public Sprite ImageBody => _demonParts.Body[_currentIndexBody].Sprite;
    public Sprite ImageLegs => _demonParts.Legs[_currentIndexLegs].Sprite;
    
    private void Start()
    {
        if (_demonParts.Head.Length == 0 || _demonParts.Body.Length == 0 || _demonParts.Legs.Length == 0 || _displayImageHead == null || _displayImageBody == null || _displayImageLegs == null)
        {
            Debug.LogError("Необходимо назначить список изображений и компонент изображения!");
            return;
        }

        _nextHead.onClick.AddListener(() => ChangeImage(_demonParts.Head, _displayImageHead, ref _currentIndexHead, 1));
        _previousHead.onClick.AddListener(() => ChangeImage(_demonParts.Head, _displayImageHead, ref _currentIndexHead, -1));
        _nextBody.onClick.AddListener(() => ChangeImage(_demonParts.Body, _displayImageBody, ref _currentIndexBody, 1));
        _previousBody.onClick.AddListener(() => ChangeImage(_demonParts.Body, _displayImageBody, ref _currentIndexBody, -1));
        _nextLegs.onClick.AddListener(() => ChangeImage(_demonParts.Legs, _displayImageLegs, ref _currentIndexLegs, 1));
        _previousLegs.onClick.AddListener(() => ChangeImage(_demonParts.Legs, _displayImageLegs, ref _currentIndexLegs, -1));

        UpdateDisplayImages();
    }

    public void Construct(DemonParts demonParts, AudioPlayer audioPlayer)
    {
        _audioPlayer = audioPlayer;
        _demonParts = demonParts;
    }
    
    private void ChangeImage(DemonPart[] demonParts, Image displayImage, ref int currentIndex, int direction)
    {
        _audioPlayer.Play(Sounds.SwitchBody);
        currentIndex = (currentIndex + direction + demonParts.Length) % demonParts.Length;
        displayImage.sprite = demonParts[currentIndex].Sprite;
    }

    private void UpdateDisplayImages()
    {
        _displayImageHead.sprite = _demonParts.Head[_currentIndexHead].Sprite;
        _displayImageBody.sprite = _demonParts.Body[_currentIndexBody].Sprite;
        _displayImageLegs.sprite = _demonParts.Legs[_currentIndexLegs].Sprite;
    }

    public void ResetDisplayImages()
    {
        _displayImageHead.sprite = _demonParts.Head[0].Sprite;
        _displayImageBody.sprite = _demonParts.Body[0].Sprite;
        _displayImageLegs.sprite = _demonParts.Legs[0].Sprite;
        _currentIndexHead = 0;
        _currentIndexBody = 0;
        _currentIndexLegs = 0;
    }
}
