using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationDialog : MonoBehaviour
{
    public GameObject CharacteristicAdjustmentSlot;
    public Transform LayoutGroup;

    private List<Characteristic> _characteristics;
    private AppManager _appManager;
    private Character _character;

    public void Init(AppManager appManager, Character character)
    {
        _appManager = appManager;
        _character = character;

        _characteristics = _appManager.CharacterManager.GetAllCharacteristics();

        foreach (Characteristic characteristic in _appManager.CharacterManager.GetAllCharacteristics())
        {
            CharacteristicType characteristicType = characteristic.Type;
            GameObject slotGameObject = Instantiate(CharacteristicAdjustmentSlot);
            CharacteristicAdjustmentSlot characteristicAdjustmentSlot = slotGameObject.GetComponent<CharacteristicAdjustmentSlot>();
            slotGameObject.transform.SetParent(LayoutGroup, false);

            characteristicAdjustmentSlot.Init(character, characteristicType, 0, characteristic.HasNegativeValues); //TODO weight
        }
    }

}
