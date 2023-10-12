using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlendshapeSystem;

/// <summary>
/// Monobehaviour that manages the interplay between gender and race in blendshape keys
/// </summary>
[System.Serializable]
public class GenderAndRaceManager
{
    #region Backing fields for gender and race data
    // These are just self-explanatory backing fields, but I serialized them for ease of testing
    [SerializeField] private float _gender = 0;

    [SerializeField] private float _human = 1;
    [SerializeField] private float _elf = 0;
    [SerializeField] private float _orc = 0;
    #endregion

    #region Accessors

    #region Gender Accessors
    // Accessor for the raw gender value stored in the backing field
    public float gender
    {
        set
        {
            _gender = value;
        }
    }
    // Accessor for how feminine the character is - meant to improve readability
    private float fem
    {
        get
        {
            return _gender;
        }
    }
    // Accessor for how masculine the character is - also meant to improve readability
    private float masc
    {
        get
        {
            return 1 - _gender;
        }
    }
    #endregion

    #region Racial Accessors
    // Accessor for how human the character is. Automatically clamps the value and rebalances other race stats
    public float human
    {
        get
        {
            return _human;
        }
        set
        {
            _human = Mathf.Clamp(value, 0, 1);
            RebalanceRaces(ref _human, ref _orc, ref _elf);
        }
    }
    // Accessor for how orcish the character is - automatically clamps the value and rebalances other race stats
    public float orc
    {
        get
        {
            return _orc;
        }
        set
        {
            _orc = Mathf.Clamp(value, 0, 1);
            RebalanceRaces(ref _orc, ref _elf, ref _human);
        }
    }
    // Accessor for how elven the character is - automatically clamps the value and rebalances other race stats
    public float elf
    {
        get
        {
            return _elf;
        }
        set
        {
            _elf = Mathf.Clamp(value, 0, 1);
            RebalanceRaces(ref _elf, ref _human, ref _orc);
        }
    }
    #endregion

    /// <summary>
    /// Accesses the blend value that should be associated with a given key after rebalancing races
    /// and taking gender into account.
    /// </summary>
    /// <param name="key"> the key to be associated with a calculated value.</param>
    /// <returns></returns>
    public float this[BlendKey key]
    {
        get
        {
            switch (key)
            {
                case BlendKey.gender_fem:
                    return fem;
                case BlendKey.species_elf_fem:
                    return elf * fem;
                case BlendKey.species_orc_fem:
                    return orc * fem;
                case BlendKey.species_elf_masc:
                    return elf * masc;
                case BlendKey.species_orc_masc:
                    return orc * masc;
                default:
                    return 0;
            }
        }
    }

    #endregion

    #region Utility Functions
    /// <summary>
    /// Rebalances the other two race values when one is changed
    /// </summary>
    /// <param name="modifiedRace">The race that was changed. Clamped here just in case.</param>
    /// <param name="secondaryRaceA">The first race that was not changed directly, must be modified</param>
    /// <param name="secondaryRaceB">The second race that was not changed directly, must be modified</param>
    private void RebalanceRaces(ref float modifiedRace, ref float secondaryRaceA, ref float secondaryRaceB)
    {
        // Clamp the race that was changed
        modifiedRace = Mathf.Clamp(modifiedRace, 0, 1);

        // while the sum of the race values is not (roughly) 1, adjust to fix that.
        while (Mathf.Abs(1-(modifiedRace + secondaryRaceA + secondaryRaceB)) > 0.001)
        {
            // Sum the race values - they should add up to 1, so get the difference between the sum and 1
            float sum = modifiedRace + secondaryRaceA + secondaryRaceB;
            float difference = 1 - sum;

            // Correct the difference by modifying the other two races
            secondaryRaceA = Mathf.Clamp(secondaryRaceA + difference / 2, 0, 1);
            secondaryRaceB = Mathf.Clamp(secondaryRaceB + difference / 2, 0, 1);
        }
    }
    #endregion
}
