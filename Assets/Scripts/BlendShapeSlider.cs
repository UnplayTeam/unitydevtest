using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlendShapeSlider : MonoBehaviour
{
    public string keyword;
    public string blendShapePositiveKeyword;
    public string blendShapeNegativeKeyword = "";
    
    private Slider slider;

    private void Start()
    {
        CharacterCustomization.Instance.AddToKeywordDictionary
            (keyword, blendShapePositiveKeyword, blendShapeNegativeKeyword);

        blendShapePositiveKeyword = blendShapePositiveKeyword.Trim();
        blendShapeNegativeKeyword = blendShapeNegativeKeyword.Trim();
        slider = GetComponent<Slider>();
    
        //When slider is moved, then call function based on the blendshape name and pass float of slider
        slider.onValueChanged.AddListener(value => 
            CharacterCustomization.Instance.ChangeBlendshapeValue(keyword, value));
        slider.onValueChanged.Invoke(slider.value);
    }

}

