using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private AppManager _appManager;
    private List<Characteristic> _characteristics;

    public void Init(AppManager appManager)
    {
        _appManager = appManager;
        SetupCharacteristics();
    }

    // This would in production be a data ingestion process, but for the sake of convenience I thought it would
    // be good to have it all in the inspector! I would never use GetComponents in this way!
    private void SetupCharacteristics()
    {
        _characteristics = new List<Characteristic>();
        Characteristic[] characteristics = GetComponents<Characteristic>();
        for (int i = 0; i < characteristics.Length; i++)
        {
            _characteristics.Add(characteristics[i]);
        }
    }
    public Characteristic GetCharacteristicByType(CharacteristicType characteristicType)
    {
        foreach (Characteristic characteristic in _characteristics)
        {
            if (characteristic.Type == characteristicType)
            {
                return characteristic;
            }
        }
        return null;
    }

    public List<Characteristic> GetAllCharacteristics()
    {
        return _characteristics;
    }
}
