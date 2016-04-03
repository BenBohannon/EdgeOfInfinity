using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

    private bool audioEnabled = true;

    public UnityEngine.UI.Button audioButton;
    public Sprite audioEnabledSprite;
    public Sprite audioDisabledSprite;


	public void AudioClick()
    {
        audioEnabled = !audioEnabled;

        AudioListener.pause = !audioEnabled;

        //Change button sprite.
        if (audioEnabled)
        {
            audioButton.image.sprite = audioEnabledSprite;
        }
        else
        {
            audioButton.image.sprite = audioDisabledSprite;
        }
    }

    public void HighGraphics()
    {
        QualitySettings.SetQualityLevel(5, true);
    }

    public void MedGraphics()
    {
        QualitySettings.SetQualityLevel(3, true);
    }

    public void LowGraphics()
    {
        QualitySettings.SetQualityLevel(0, true);
    }

    public void ExitButton()
    {
        this.gameObject.SetActive(false);
    }


}
