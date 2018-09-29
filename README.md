# IMPORTANT!
This is no longer remotely maintained.  If someone else would like to take over ownership of the framework, that is GREAT!  I'm happy to help you as much as I can.  Otherwise, as far as I know, this should still work -- provided you update the DLLs to the current version of PoE2 and make whatever code changes are necessary to be compatible with whichever version is current.


# PoE2Mods.pw
Modding framework for Pillars of Eternity 2

** IF STEAM OR WHEREVER YOU GOT THE GAME FROM HAS UPDATED THE GAME, YOU NEED TO UPDATE THIS AS WELL**

This framework allows you to make changes to almost the entire gamut of PoE2's codebase.  Much like PoE was, PoE2's code appears to be pretty much straight non-obfuscated Unity code, so extremely easy and fun to change to your liking.

Most instructions can be followed directly from Patchwork.  This repo includes all of the relevant modifications for PoE already in place. Note that the release version of Patchwork itself is broken, so I highly recommend using the version in this repo.

# Requirements

Patchwork is a Mono/.NET application and so needs the .NET Framework or Mono to run.

    Windows: .NET Framework 4.5+
    Linux and Mac: Mono 4.2.1.102+

# Instructions

**If you ONLY want to use mods, not create them, please click the Release link at the top of the page!  Download Release.7z and continue with these instructions using it.**

Using the program is straight-forward:

    Extract it into an empty folder.
	
	Copy "ModIniFile.ini" and "INIFileParser.dll" to your base POE2 directory next to the exe. Copy the "INIFileParser.dll" to [base]\PillarsOfEternityII_Data\Managed as well.
	
	Edit the ModIniFile to your liking in a text editor to disable/enable individual mods or change settings.

    Launch the program (PatchworkLauncher.exe)

    Note: On Linux, you may need to open the program using mono explicitly (see instructions on running Mono applications in your distribution).

    Specify your game folder in the dialog box or type it in the textbox.

    Note: The dialog box will not display hidden files or folders.

    Go to the Active Mods menu and add the mod file(s) (usually ending with .pw.dll) to the list of mods, checking those you want enabled.

    Note: Mod files so chosen will normally be copied to the Mods folder.

    Use Launch with Mods and Launch without Mods to start the game.

# Building/Development

Building is fairly straightforward.  You will need to create an Open Assembly version of PoE2's original Assembly-CSharp.dll found in the PillarsOfEternity2_Data/Managed directory of your game install.  You do this via OpenAssemblyCreator.exe which is included in the repo.  Your mod projects will all need to reference this OPEN assembly.  Once this reference has been added, the solution should build just fine.  See my various included mod projects for development guidelines.

# Current Mods

  * GameSpeed adds a new way-faster speed option and turns the toggle fast/slow hotkeys into scaled steps.  Combat Speed has been removed and the whole thing feels very similar to PoE1.  The Combat Speed Up/Down keybindings in-game will adjust your in-game speed across (0.2x, 1x, 2x, 6x).  Pressing F or the Fast Mode toggle key will instantly toggle the speed to 10x for running around maps really fast wheee.
  * AchievementEnabler does what it sounds like - it removes the penalty for cheating in the Console, so you can still gain achievements.  With this, the most important aspect is NOT TO USE `iroll20s`.  You no longer need to enter this command in order to enable cheat commands, and doing so could possibly cause problems.  Since there's no reason to use it now, don't.  Just type your console commands directly.  If you've previously already used `iroll20s` I THINK typing it again will re-enable achievements anyway, but I'm not sure.
  * CipherFocus - this was my throwaway test mod.  In its current state it just slightly rearranges Focus totals and generation for Ciphers.  It's pretty pointless, but I left it as another example anyway.
  * CameraSpringRemovalMod - This one I made for myself.  If you're using the game speed mod, at the highest setting, the camera spring wobbles constantly and makes it really obnoxious to click stuff.  This removes that wobble and converts the camera to a slightly-smoothed locked camera.  It looks slightly less pretty, but way more usable! 
  * MulticlassPenaltyRemover - Experimentation mod.  This removes the level penalty to accessing each tier of class ability at levelup, so you gain them at the same rate as a single classed character.  This obviously makes the character insanely OP, and given that the game is apparently already really easy, you probably don't want to use this for an actual playthrough.  I made it because I always reroll a million times in these games and wanted to test a bunch of builds rapidly with minimal trouble.
  * DisableAutosave - It disables autosave!
  * ResetEmpowerAndSpells - This instantly restores all empower points and/or spells/abilities for all selected characters.  Select 1 or more characters, then press Ctrl + E to restore Empower points and press Ctrl + S to restore spellcasts/abilities.  This restores special stuff as well, like Cipher focus and Chanter phrases.
  * AddSpellCastsPerLevel - Adds additional spell casts per encounter for each tier, by request.  It's set at 2/level right now, but easy to change in the code.  You can see the proper variable at the top of the mod's replacement method.
  * CameraZoomMod - Removes zoom out limit, yay.
  * DifficultyIconsMod - Contributor mod, 5 stars.  Adds zone difficulty icons back to areas that setting Level Scaling removed them for no good reason.  Also optionally re-enables the character icons on individual enemies.  You can control each of these independently from the INI file.
  

# STEAM ACHIEVEMENTS

Due to how the game operates with Steam, you can't get Steam Achievements with this framework directly.  You can only receive them by running the game via Steam's executable, which my mod bypasses and runs directly.  You can work around it if you really want those sweet Steam points, but it's relatively painful.  First, you need to run the game WITH all mods active that you want to be active.  Once the game has fully loaded (DO NOT CLOSE IT), open File Explorer and find POE2's directory.  From there, go into PillarsOfEternity2_Data/Managed and you'll see "Assembly-CSharp.dll".  You need to copy this file somewhere else temporarily.  Now close your game entirely and let the Mod Launcher fully close as well.  Once that's done, go back to PillarsOfEternity2_Data/Managed and RENAME "Assembly-CSharp.dll" to "Assembly-CSharp.dll.ORIGINAL".  Now paste the file you copied into this directory, so it's the new "Assembly-CSharp.dll".  Run your game through Steam and it will have all Mods applied plus allow Steam achievements.

Note that at this point, you're bypassing the launcher.  The launcher automatically does this process at runtime.  You will need to redo the whole process every time you want to change which Mods you're using OR every time Steam updates. *PLEASE NOTE* every time Steam updates POE2 you're going to have to redo this process, but ONLY after downloading a new Release from here.  If there's been a Steam update of the game and you don't see a new release here, please open an Issue or otherwise send me a message, because you will need new DLLs to handle the update.  Undefined behaviors and most likely horrible, terrible things will happen to you otherwise.  I'm not responsible for this mod framework lighting your house on fire and killing your pets.  You have been warned!

To reverse this process and remove mods (for the excellent FrostFG, thanks buddy!), you need to go back to your PillarsOfEternity2_Data/Managed folder and delete the "Assembly-CSharp.dll" from above.  Now rename the "Assembly-CSharp.dll.ORIGINAL" that you changed earlier back to "Assembly-CSharp.dll" and you're done.  Your game is now completely mod free!

# MODIFYING CLASS OR SUBCLASS

This isn't strictly related to my mods, as you can do this natively.  It does, however, disable achievements, which my mod prevents.  It's also being asked a lot (for good reason) so I'm transferring my instructions here to a central location.

There are 2 Console Commands already built into the game.
  * SetClassLevel will alter a major class (not subclass).  It takes 4 arguments - the character ID, class ID, level, and a boolean reset.  Character ID can be found with the console command `FindCharacter <name>` such as `FindCharacter Eder`.  Class ID is just the class name, such as `Monk`. Level is the level to grant the new class to.  The boolean reset is either `true`	or `false`. If true, it erases ALL other class data.  If false, it persists them.  By passing false, this enables you to do things such as making a single character 5 different classes.  I have no idea what doing this will cause to happen, but it seems to work at a cursory level and will probably destroy your game.  Use at your own risk.
  * SetSubClass will add a subclass to a currently possessed major class.  It takes 3 arguments.  The first is Character ID, which is the same as above.  The second is Class ID for the MAJOR class, also the same as above.  The third is SubclassID, which is the subclass name prefixed with the major class name separated by an underscore. For example, to add the `Devoted` subclass to Eder, you'd use `SetSubclass Companion_Eder(Clone) Fighter Fighter_Devoted`.  This only takes effect if Eder is already a Fighter, otherwise it lurks and does nothing unless you add Fighter to Eder via SetClassLevel above.  Note that you don't get any of the related subclass choices, such as picking a favored weapon for Devoted, but you do seem to get the benefits/penalties.
  * For potential developers - all class/subclass data is handled really creatively by Obsidian.  Every character has every class as a possibility, and they coexist.  All actual realtime data is stored in subclassed PersistentData areas, which are what SetClassLevel/SetSubClass modify.  
  
# MODIFYING EXPERIENCE GAIN RATE

Also not related to my mods, but since probably lots of people will want this...Copy pasted from an issue:

The value is stored in JSON so you don't even need a mod to adjust it to your liking. To find it, head to

Pillars of Eternity II\PillarsOfEternityII_Data\exported\design\gamedata

now open global.gamedatabundle in whatever text editor you want. You'll need a JSON formatter to make it readable -- I use Atom with PrettyJSON package. Once you can read it, ctrl F for

"DebugName": "ExperienceTable",

What follows is the exp values for every single level. You can make them super huge if you want from there.  In that file:
"PartySizeBonusExperiencePercent"

Putting it at 12 results in a 25% deficit and putting it at 0 results in a 50% deficit. The setting likely is only used when your party consists of 2 or fewer characters, so quite great for soloplay.


# RELEASE NOTES

2.0 - Completely refactored codebase.  See the instructions above, as everything is now controlled from a new INI file.  Updated to the new PoE2 Patch as well.

1.30 - Lots of new mods!  First up, DifficultyIconsMod from our first official contributor other than me, thanks man!  Fixes not having difficulty icons when you scale levels, totally awesome.  I added a companion mod that adds onto DifficultyIconsMod called DifficultyIconsAlways which also ignores difficulty level in addition to scaling, so you always get difficulty icons on any difficulty (I'm playing on PotD and hate not having them).  
I added another request that bumps up the number of spells from each tier you can cast in a fight (set at 2 right now), and an unlocked camera zoom which feels so much better. 

1.20 - Added autosave disable and the requested spell/empower reset hotkeys.

1.10 - 2 new Mods, see above!

1.07 - More Readme warnings about updating this mod along with POE2 

1.06 - Started making release notes.  Please read the section on AchievementEnabler, as usage is VERY different now and you should not continue using `iroll20s`.  Also added instructions for class/subclass modification.

