using Game;
using Game.GameData;
using Onyx;
using Patchwork;

namespace ResetEmpowerAndSpellsMod
{
    [ModifiesType("Game.SpellMax")]
    class SpellCastsPerLevel : Game.SpellMax
    {
        [ModifiesMember("GetSpellCastMax")]
        public OnyxInt GetSpellCastMaxNew(CharacterStats caster, CharacterClass spellClass, int spellLevel)
		{
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
					onyxInt += caster.GetBonusSpellCasts(spellClass, spellLevel) + 2;
				}
			}
			return onyxInt;
		}
    }
}
