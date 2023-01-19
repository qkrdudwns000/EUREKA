using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public Slider BgmSlider;
    public Slider SfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        BgmSlider.onValueChanged.AddListener(SoundManager.inst.BGMSoundVolume);
        SfxSlider.onValueChanged.AddListener(SoundManager.inst.SFXSoundVolume);
    }
}
