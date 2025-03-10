using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    private AudioSource AudioSource;
    public AudioClip ButtonSound;
    private Button button;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonOnClickSound);
    }
    void ButtonOnClickSound()
    {
        AudioSource.PlayOneShot(ButtonSound, 1);
    }
}
