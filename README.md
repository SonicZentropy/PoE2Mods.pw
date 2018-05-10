# TyrannyMod.pw
Modding framework for Tyranny (NON DLC)

This framework allows you to make changes to almost the entire gamut of Tyranny's codebase.  Much like PoE was, Tyranny's code appears to be pretty much straight non-obfuscated Unity code, so extremely easy and fun to change to your liking.

Most instructions can be followed directly from Patchwork.  This repo includes all of the relevant modifications for Tyranny already in place.  It also has a bugfix in the Patchwork framework that crashed Tyranny.  Once Patchwork merges my pull req, this repo can switch to using it as a submodule instead of duplicating it.

# Setup

Two files will need to be updated to match your personal file paths:
- AppInfoDLL.cs 
- TyrannyPatchInfo.cs

The needed changes are really obvious.  Note that a large portion of Tyranny's code base is inside Assembly-firstpass, not the plain assembly.  It looks like they reused a huge amount of the code from Pillars of Eternity in Tyranny and all of it ended up in Firstpass.  You'll just have to poke around the decompiled versions of both DLLs to find what you want.

# Building

The solution should build perfectly once the paths specified above are changed.  After build, you'll need to move AppInfo.DLL from its project's bin directory into TyrranyMods.pw's bin directory.  Then run PatchworkLauncher.exe from that same bin directory, point it to your Tyranny install, and add TyrannyMods.pw.dll as a mod. Enjoy!

# Current Mods

Most of these are just quick, hacky example mods I made to test.  GameSpeed adds a new way-faster speed option and turns the toggle fast/slow hotkeys into scaled steps.  CampingSupplies is an example of correctly modifying a property.  CameraControl is just a quick way to test to make sure your patch is working since changes to this are REALLY apparent.

FogOfWar is much more extensive and serves as an example of modifying a complex block of code. The UpdateVertices method decompiles fairly hideously with ILSpy and JustDecompile both producing a method that doesn't compile at all.  I ended up using DotPeek's decompilation as the base and cross referencing the other decompiled versions to decipher what was happening.
