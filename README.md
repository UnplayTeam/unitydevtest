# jverticchio-unplay-devtest
This repository contains my submission for Unplay's Unity Dev Test as part of my application for the Senior Unity Developer Role.

I completed the required tasks and pushed my changes to the "character-creation" branch, for which I have submitted a pull request to the original "UnplayTeam/unitydevtest" repository.
Here, I will give an overview of my process and the steps I took to implement my character creator.

## Initial Setup
The first step I took was to visit the Unity Asset Store and add some visual assets to the project. I wanted the character creator to exist in a world-space other than a blank void,
so I first searched for a fantasy environment. I found a [suitable fantasy forest asset pack](https://assetstore.unity.com/packages/3d/environments/fantasy/fantasy-forest-environment-free-demo-35361) and used the demo scene provided as the basis for my character creator.
I also grabbed [a set of UI assets](https://assetstore.unity.com/packages/2d/gui/rpg-fantasy-mobile-gui-with-source-files-166086) to use for the various buttons, menus, etc.
After placing the character model in the scene and positioning the camera, I set up buttons to rotate the character model and zoom the camera in and out. 

## Modifying Blend Shapes
Having never worked with Skinned Mesh Renderers or Blend Shapes before, I did some investigating to learn how to modify the blend shape weight programatically. 
I read the [Unity documentation for the "SetBlendShapeWeight" method](https://docs.unity3d.com/ScriptReference/SkinnedMeshRenderer.SetBlendShapeWeight.html) and discovered that I would need a way
to keep track of each blend shape's index within each mesh renderer's list of blend shapes. I created the "CharacterAttributeInventory", "CharacterModelAttribute", and "AttributeBlendShape" classes to achieve this goal.
By setting up the attribute inventory with every attribute I wanted to modify, and connecting each attribute to the various mesh renderers and the appropriate index for each attribute, I had a means
for modifying the blend shape weights through code. To make it easier for me to set up the attribute inventory in Unity's inspector, I created a [spreadsheet with all of the modifiable mesh renderers and their blend shapes](https://docs.google.com/spreadsheets/d/1LLgAzo8V3LqaOCe664QkfWwgPib1FGVCx6CGykS0yJY/edit?usp=sharing).
I then created a basic UI panel with buttons to set the "gender_fem" attribute to either 0 or 1 for "Male" and "Female" options.

## Additional Attributes
I continued the process of adding UI controls to modify blend shape weights by creating buttons for each of the three species options, "Human", "Elf", and "Orc".
I also set up a slider to control the body weight attributes "body_weight_thin" and "body_weight_heavy". Using the Attribute Inventory, I was able to combine the "body_weight_thin_head" attribute with
"body_weight_thin", thinking that these values should be linked and adjusted simultaneously. 
Next I created a slider for the muscularity attributes "body_muscular_heavy" and "body_muscular_mid". I kept these attributes separate because I hadn't decided whether or not they should be linked or
if they should be modifiable individually. I settled on having a single slider that modified both values after playing around with modifying separately and thinking that they worked better when linked.

## Species Attributes
At this point I realized that one of the strength of blend shapes is, of course, the ability to blend different attributes together and adjust blend weights along a spectrum of values.
That insight led to me to get rid of the sex and species selection buttons and instead create sliders that would give the user more control over their body type and species selection.
The body type slider was straightforward to implement, using the slider to modify the "gender_fem" attribute and apply its value to the species "\_fem" and "\_masc" attributes proportionally. 
The species selection slider was a more interesting challenge. I wanted the user to be able to create any combination of the three available species in whatever proportions suited them.

I created the "SpeciesToggleSlider" class as a combination UI control that featured a toggle and a conditionally activated slider. My thinking was that if a user has a single
species toggle active, then there is no need to display the sliders since their character is only defined by a single set of species attributes. If multiple species toggles were active, however,
I would display the sliders so that the user could adjust the proportion of each active species, allowing them create species-hybrids of any combination. It took some experimentaiton 
and handling edge cases to get the toggle sliders working how I intended, but I am pleased with the end result.

## Facial Attributes
Now that I had ironed out the logic for adjusting attributes, I set up additional UI panels with sliders for all of the available facial attributes. Some attributes were independent, like muscularity,
and others were opposed, like "body_weight_thin" and "body_weight_heavy". By creating scripts and prefabs to handle these two kinds of attribute sliders, I was able to quickly populate the 
customization panels with all of the modifiable facial attributes.

## Final Touches
Now that all of the core functionality was implemented, I went back and cleaned up my code - simplifying things where possible and adding explanatory comments where appropriate.
The final step in the process was to add background music and sound effects. Again, I relied on the Unity Asset Store and found a set of [background music tracks](https://assetstore.unity.com/packages/audio/music/orchestral/fantasy-exploration-music-213668) and [UI sound effects](https://assetstore.unity.com/packages/audio/sound-fx/fantasy-menu-sfx-57238).
I created a simple "AudioManager" class to allow the user to enable/disable the music and/or sound effects. Finally I updated the various UI buttons, toggles, and sliders to call the "PlayClip" method on "AudioManager" to play my chosen sound effects at the appropriate times.

## Next Steps
I chose not to add sliders for the more detailed body attributes, the "iso_" attributes that control individual muscle groups and body parts, but adding additional panels with these sliders would be an easy next step.
A more interesting next step would be to implement a system for saving a character model's blend shape weights as data, likely a json document, that could be exported from a model and imported onto a model, applying the previously selected blend shape weights to the model. That way players could save their custom characters and load them in later sessions.
One other feature I'd like to implement is creating poses and animations for the character model. That way the user could see their custom character in a more dynamic context, rather than the standard "t-pose".
