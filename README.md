# PoE2Mods.pw
Modding framework for Pillars of Eternity 2

This framework allows you to make changes to almost the entire gamut of PoE2's codebase.  Much like PoE was, PoE2's code appears to be pretty much straight non-obfuscated Unity code, so extremely easy and fun to change to your liking.

Most instructions can be followed directly from Patchwork.  This repo includes all of the relevant modifications for PoE already in place. Note that the release version of Patchwork itself is broken, so I highly recommend using the version in this repo.

# Requirements

Patchwork is a Mono/.NET application and so needs the .NET Framework or Mono to run.

    Windows: .NET Framework 4.5+
    Linux and Mac: Mono 4.2.1.102+

# Instructions

Using the program is straight-forward:

    Extract it into an empty folder.

    Launch the program (PatchworkLauncher.exe)

    Note: On Linux, you may need to open the program using mono explicitly (see instructions on running Mono applications in your distribution).

    Specify your game folder in the dialog box or type it in the textbox.

    Note: The dialog box will not display hidden files or folders.

    Go to the Active Mods menu and add the mod file(s) (usually ending with .pw.dll) to the list of mods, checking those you want enabled.

    Note: Mod files so chosen will normally be copied to the Mods folder.

    Use Launch with Mods and Launch without Mods to start the game.

# Building/Development

Building is fairly straightforward.  You will need to create an Open Assembly version of PoE2's original Assembly-CSharp.dll found in the PillarsOfEternity2_Data/Managed directory of your game install.  You do this via OpenAssemblyCreator.exe which is included in the repo.  Your mod projects will all need to reference this OPEN assembly.  Once this reference has been added, the solution should build just fine.  See my various included mod projects for development guidelines.

# Deployment

Once you've successfully built, you'll find your mods inside of each of the Mod build paths.  Those DLLs need to be moved to your PatchworkLauncher folder, preferably into a Mods subdir like I've done in my Release.

# Current Mods

  * GameSpeed adds a new way-faster speed option and turns the toggle fast/slow hotkeys into scaled steps.  Combat Speed has been removed and the whole thing feels very similar to PoE1.  The Combat Speed Up/Down keybindings in-game will adjust your in-game speed across (0.2x, 1x, 2x, 6x).  Pressing F or the Fast Mode toggle key will instantly toggle the speed to 10x for running around maps really fast wheee.
  * AchievementEnabler does what it sounds like - it removes the penalty for using `iroll20s` in the Console, so you can still gain achievements.  If you've previously already used `iroll20s` I THINK this will re-enable achievements anyway, but I'm not sure.
  * CipherFocus - this was my throwaway test mod.  In its current state it just slightly rearranges Focus totals and generation for Ciphers.  It's pretty pointless, but I left it as another example anyway.
  * Multiclass Penalty Remover - Removes the level penalty at which multiclass characters upgrade power tiers.  This makes the game obviously WAY WAY EASY.  For experimental use with SetClassLevel and SetSubclass console commands.