using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : ButtonUIBase
{
    [SerializeField]
    private List<SoundSprite> soundSprites = new List<SoundSprite>();

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

    protected override void OnButtonClick()
    {
        base.OnButtonClick();

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
