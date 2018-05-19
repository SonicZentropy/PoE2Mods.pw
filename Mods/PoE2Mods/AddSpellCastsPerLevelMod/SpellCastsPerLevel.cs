using Game;
using Game.GameData;
using Onyx;
using Patchwork;

namespace PoE2Mods
{
    [ModifiesType("Game.SpellMax")]
    class SpellCastsPerLevel : Game.SpellMax
    {
        [NewMember]
        bool ConfigHasBeenInit;

        [NewMember]
        bool UseMod;

        [NewMember]
        int BonusCastsPerLevel;

        [NewMemberAttribute]
        public OnyxInt orig_GetSpellCastMax(CharacterStats caster, CharacterClass spellClass, int spellLevel) {
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
                    onyxInt += caster.GetBonusSpellCasts(spellClass, spellLevel);
                }
            }
            return onyxInt;

        }


        [ModifiesMember("GetSpellCastMax")]
        public OnyxInt GetSpellCastMaxNew(CharacterStats caster, CharacterClass spellClass, int spellLevel)
		{
		    // Have to init this way because class initialization doesn't work
		    if (!ConfigHasBeenInit) {
		        ConfigHasBeenInit = true;
		        UseMod = UserConfig.GetValueAsBool("SpellCastsPerLevel","enableMod");
		        BonusCastsPerLevel = UserConfig.GetValueAsInt("SpellCastsPerLevel", "BonusCastsPerLevel");
		    }
		    if (! UseMod) {
		        return orig_GetSpellCastMax(caster, spellClass, spellLevel);
		        
		    }

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
					onyxInt += caster.GetBonusSpellCasts(spellClass, spellLevel) + BonusCastsPerLevel;
				}
			}
			return onyxInt;
		}
    }
}
