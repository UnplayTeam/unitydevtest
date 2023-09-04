using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterCustomizeListener
{
    public void InitializeDataValues() { }

    public void HandlePropertyChange(string propertyName, float newValue) { }
}