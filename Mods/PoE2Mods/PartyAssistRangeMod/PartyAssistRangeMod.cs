using System.Collections.Generic;
using Game;
using Game.GameData;
using Onyx;
using Patchwork;
using UnityEngine;

namespace PoE2Mods.PartyAssistRangeMod
{
    [ModifiesType("Game.SkillManager")]
    public class PartyAssistRangeMod : SkillManager
    {
        [ModifiesMember("GetAssistValue")]
        public static int GetAssistValue(SkillGameData skill, CharacterStats primarySkillCheck, out int leftoverAssistPoints, out int pointsUntilNextLevel, StatBreakdown breakdown = null)
        {
            int num = 0;

            AssistBreakdown assistBreakdown = breakdown as AssistBreakdown;
            List<PartyMember> activePrimaryPartyMembers = SingletonBehavior<PartyManager>.Instance.GetActivePrimaryPartyMembers();

            foreach (PartyMember partyMember in activePrimaryPartyMembers)
            {
                CharacterStats component = partyMember.GetComponent<CharacterStats>();

                if (component == null || component == primarySkillCheck)
                {
                    continue;
                }

                if (ScriptedInteraction.ActiveInteraction != null && !ScriptedInteraction.IsPartyMemberAvailable(ScriptedInteraction.ActiveInteraction, partyMember))
                {
                    continue;
                }

                num += component.CalculateSkill(skill, null);
                assistBreakdown?.AddHelperCharacter(component.gameObject);
            }

            for (int j = SkillManager.GameData.AssistThresholds.Length - 1; j >= 0; j--)
            {
                int threshold = SkillManager.GameData.AssistThresholds[j].Threshold;

                if (num < threshold)
                {
                    continue;
                }

                int result = StatBreakdown.AddAdditiveBonus(breakdown, SkillManager.GameData.AssistThresholds[j].AssistValue, StringTableType.Gui, 2570);
                leftoverAssistPoints = num - threshold;

                if (j < SkillManager.GameData.AssistThresholds.Length - 1)
                {
                    pointsUntilNextLevel = SkillManager.GameData.AssistThresholds[j + 1].Threshold - num;
                }
                else
                {
                    Debug.LogError("Assist points reached the top of the assist bonus table. More rows need to be defined.");
                    pointsUntilNextLevel = 0;
                }

                return result;
            }

            leftoverAssistPoints = num;
            pointsUntilNextLevel = SkillManager.GameData.AssistThresholds[0].Threshold - leftoverAssistPoints;

            return 0;
        }
    }
}
