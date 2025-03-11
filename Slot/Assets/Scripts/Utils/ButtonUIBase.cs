using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ButtonUIBase : MonoBehaviour
{
    protected Image targetImage;
    protected Button targetButton;

    [SerializeField]
    private AudioClip clickSound;
    [SerializeField]
    private float volume;

    protected virtual void Awake()
    {
        targetImage = GetComponent<Image>();
        targetButton = GetComponent<Button>();
        targetButton.onClick.AddListener(() => OnButtonClick());
    }

    protected virtual void OnButtonClick()
    {
        SoundManager.Instance.ExecuteSfx(clickSound, volume);
    }
}
