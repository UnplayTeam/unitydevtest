using UnityEngine;
using RPG.Character.Avatar;

namespace RPG.Character {
  public class CharacterPawn : MonoBehaviour {
    [SerializeField] private CharacterPawnAvatar _PawnAvatar;
    
    public CharacterPawnAvatar PawnAvatar => _PawnAvatar;
    
    // Stub for some kind of character controller, which should receive input from some object that represents a client
    // Input can be movement, actions etc or in our case customization data applied to the CharacterPawnAvatar
    
    // TODO If I have time, save and load the AvatarCustomizationDataEntries to/from PlayerPrefs
    // PlayerPrefs being simple to work with for demonstration, bad for an actual game
  }
}
