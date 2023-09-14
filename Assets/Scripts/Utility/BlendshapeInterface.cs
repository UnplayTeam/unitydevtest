using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlendshapeSystem;

/// <summary>
/// An interface for all components that might need to send data to the BlendshapeControls instance.
/// This is an artefact from an earlier implementation which was going to have multiple kinds of inputs, 
/// but for time reasons that functionality was scrapped.
/// </summary>
public interface BlendshapeInterface
{
    virtual void UpdateBlendshape(BlendKey key, float value)
    {
        if (BlendshapeControls.instance != null)
        {
            BlendshapeControls.instance[key] = value;
        }
    }

}
