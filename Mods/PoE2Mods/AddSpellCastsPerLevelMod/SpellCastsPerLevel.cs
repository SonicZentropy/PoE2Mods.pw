using Game;
using Game.GameData;
using Onyx;
using Patchwork;

namespace PoE2Mods
{
    [ModifiesType("Game.SpellMax")]
    class SpellCastsPerLevel : Game.SpellMax
    {
        

        [ModifiesMember("GetSpellCastMax")]
        public OnyxInt GetSpellCastMaxNew(CharacterStats caster, CharacterClass spellClass, int spellLevel)
		{
            // Change this to change how many bonus spell casts you get per level
            int ModdedSpellCastsPerLevel = 2; 

			OnyxInt onyxInt = OnyxInt.PositiveInfinity;
			if (caster != null && spellLevel >= 1)
			{
				onyxInt = this.SpellCastMaxLookup(spellClass, caster.GetClassPowerLevel(spellClass), spellLevel);
				if (onyxInt == -1)
				{
					onyxInt = OnyxInt.PositiveInfinity;
				}
				if (onyxInt > 0)
				{
					onyxInt += caster.GetBonusSpellCasts(spellClass, spellLevel) + ModdedSpellCastsPerLevel;
				}
			}
			return onyxInt;
		}
    }
}
