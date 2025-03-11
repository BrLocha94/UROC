using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class SoundButton : MonoBehaviour
{
    [SerializeField]
    private List<SoundSprite> soundSprites = new List<SoundSprite>();

    private Image targetImage;
    private Button targetButton;

    private void Awake()
    {
        targetImage = GetComponent<Image>();
        targetButton = GetComponent<Button>();
        targetButton.onClick.AddListener(() => OnButtonClick());
    }

    private void Start()
    {
        SoundVolume currentVolume = SoundManager.Instance.currentVolume;
        ChangeSprite(currentVolume);
    }

    private void ChangeSprite(SoundVolume volume)
    {
        foreach (SoundSprite soundSprite in soundSprites) 
        {
            if(soundSprite.volume == volume)
            {
                targetImage.sprite = soundSprite.sprite;
                break;
            }
        }
    }

    private void OnButtonClick()
    {
        SoundVolume volume = SoundManager.Instance.ChangeCurrentVolume();
        ChangeSprite(volume);
    }
}

[System.Serializable]
public class SoundSprite
{
    public SoundVolume volume;
    public Sprite sprite;
}
