//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Game;
//using Game.GameData;
//using Onyx;
//using Patchwork;
//using UnityEngine;
//using Console = Game.Console;

//namespace PoE2Mods.DPSMeter
//{
//    [ModifiesType("Game.AttackBase")]
//    class DPSMeter
//    {
//        [ModifiesMember("PostAttackMessages")]
//        public static void PostAttackMessagesNew(DamageInfo info, IList<StatusEffectAddResult> appliedEffects, bool primaryAttack, string simpleFormatString, string missFormatString)
//        {
//            try {
//                if (!info.TargetPreviouslyDead) {
//                    if (!(info.AttackData != null) || !info.AttackData.HideFromCombatLog) {
//                        GameObject target = info.Target;
//                        GameObject attacker = info.Attacker;
//                        Faction component = target.GetComponent<Faction>();
//                        Faction component2 = attacker.GetComponent<Faction>();
//                        string text = CharacterStatsUtility.NameColored(attacker);
//                        string text2 = text;
//                        string text3 = CharacterStatsUtility.NameColored(target);
//                        GenericAbility genericAbility = info.Ability;
//                        Phrase phrase = null;
//                        if (info.Attack != null) {
//                            if (info.Attack.PhraseOrigin != null) {
//                                phrase = info.Attack.PhraseOrigin;
//                            }
//                            if (info.Attack.TriggeringAbility != null) {
//                                genericAbility = info.Attack.TriggeringAbility;
//                            }
//                            else if (info.Attack.AbilityOrigin != null) {
//                                genericAbility = info.Attack.AbilityOrigin;
//                            }
//                        }
//                        string text4 = string.Empty;
//                        if (phrase != null) {
//                            text4 = ((!info.IsEmpowered) ? string.Empty : TextTagging.CreateSpriteTag("Empowered")) + phrase.GetName();
//                            text += GuiStringTable.Format(1731, new object[]
//                            {
//                        text4
//                            });
//                        }
//                        else if (genericAbility != null) {
//                            text4 = ((!info.IsEmpowered) ? string.Empty : TextTagging.CreateSpriteTag("Empowered")) + info.GetName();
//                            text += GuiStringTable.Format(1731, new object[]
//                            {
//                        text4
//                            });
//                        }
//                        Color messageColor = AttackBase.GetMessageColor(component2, component);
//                        if (info.Immune) {
//                            StringBuilder stringBuilder = new StringBuilder();
//                            info.SimpleImmuneReport(missFormatString, text, text3, stringBuilder);
//                            Console.AddBatchedMessage(stringBuilder.ToString(), attacker, Color.white, info);
//                        }
//                        else if (info.IsMiss) {
//                            StringBuilder stringBuilder2 = new StringBuilder();
//                            info.SimpleMissReport(missFormatString, text, text3, stringBuilder2);
//                            StringBuilder stringBuilder3 = new StringBuilder();
//                            if (info.DefendedBy != DefenseType.None) {
//                                info.GetToHitReport(attacker, target, stringBuilder3);
//                            }
//                            if (AttackBase.s_PostUseDelta) {
//                                Console.InsertBatchedMessage(stringBuilder2.ToString(), stringBuilder3.ToString(), attacker, Color.white, Console.Instance.MessageDelta, info);
//                            }
//                            else {
//                                Console.AddBatchedMessage(stringBuilder2.ToString(), stringBuilder3.ToString(), attacker, Color.white, info);
//                            }
//                        }
//                        else {
//                            StringBuilder stringBuilder4 = new StringBuilder();
//                            StringBuilder stringBuilder5 = new StringBuilder();
//                            if (info.DefendedBy != DefenseType.None) {
//                                info.GetToHitReport(attacker, target, stringBuilder4);
//                            }
//                            StringBuilder stringBuilder6 = new StringBuilder(stringBuilder4.ToString());
//                            if (info.DamageAmount != 0f && !info.NegateEffects) {
//                                stringBuilder6.AppendLine();
//                                info.GetDamageReport(stringBuilder6);
//                            }
//                            string name = info.GetName();
//                            if (appliedEffects != null && appliedEffects.Count > 0) {
//                                AttackBase.s_statusEffectBuffer.Clear();
//                                for (int i = 0; i < appliedEffects.Count; i++) {
//                                    if (appliedEffects[i].StatusEffect != null && appliedEffects[i].StatusEffect.GetShowInUI(StatusEffectFormatMode.CombatLog, false)) {
//                                        AttackBase.s_statusEffectBuffer.Add(appliedEffects[i].StatusEffect);
//                                    }
//                                }
//                                //IStatusEffectOrigin origin = info.Attack ?? info.Ability;
//                                IStatusEffectOrigin origin;
//                                if (info.Attack != null) {
//                                    origin = info.Attack;
//                                }
//                                else {
//                                    origin = info.Ability;
//                                }

//                                foreach (StatusEffectInstance se in AttackBase.s_statusEffectBuffer) {
                                    
//                                }

//                                float statusEffectCumulative = 0;
//                                string text5 = StatusEffectTextUtility.ListToString(new StatusEffectListFormatProvider(StatusEffectFormatMode.CombatLog, origin), AttackBase.s_statusEffectBuffer);
//                                for (int j = 0; j < appliedEffects.Count; j++) {
                                    
                                    
//                                    //HERE
//                                    var seData = appliedEffects[j].StatusEffectData;
//                                    statusEffectCumulative = seData.CalculateValue(origin, info.Target.GetComponent<IStatusEffectTarget>(), )



//                                    if (!string.IsNullOrEmpty(appliedEffects[j].OtherString)) {
//                                        StatusEffectAddResultType resultType = appliedEffects[j].ResultType;
//                                        if (resultType != StatusEffectAddResultType.CancelledOut) {
//                                            if (resultType == StatusEffectAddResultType.FailedRequirement) {
//                                                text5 = TextUtils.Join(GuiStringTable.Comma(), text5, appliedEffects[j].OtherString);
//                                            }
//                                        }
//                                        else {
//                                            string b = appliedEffects[j].OtherString + TextTagging.CreateSpriteTag("tooltip_countericon") + appliedEffects[j].StatusEffectData.GetDisplayString(new StatusEffectFormatProvider(origin));
//                                            text5 = TextUtils.Join(GuiStringTable.Comma(), text5, b);
//                                        }
//                                    }
//                                }
//                                if (!string.IsNullOrEmpty(text5)) {
//                                    stringBuilder6.Append(' ');
//                                    Game.Console.AddMessage("Appending text5: " + text5);
//                                    stringBuilder6.Append(text5);
//                                }
//                                AttackBase.s_statusEffectBundleBuffer.Clear();
//                                StatusEffectUtility.GetBundles(AttackBase.s_statusEffectBuffer, AttackBase.s_statusEffectBundleBuffer);
//                                bool flag = true;
//                                for (int k = 0; k < AttackBase.s_statusEffectBundleBuffer.Count; k++) {
//                                    if (AttackBase.s_statusEffectBundleBuffer[k].GetShowInUI(StatusEffectFormatMode.CombatLog, false)) {
//                                        string text6 = AttackBase.s_statusEffectBundleBuffer[k].GetName();
//                                        if (text6 == text4) {
//                                            text6 = GuiStringTable.GetText(1213);
//                                        }
//                                        if (!flag) {
//                                            stringBuilder5.Append(GuiStringTable.Comma());
//                                        }
//                                        else {
//                                            flag = false;
//                                        }
//                                        float maxDurationLeft = AttackBase.s_statusEffectBundleBuffer[k].GetMaxDurationLeft();
//                                        if (maxDurationLeft > 0f && !float.IsInfinity(maxDurationLeft)) {
//                                            stringBuilder5.AppendGuiFormat(1665, new object[]
//                                            {
//                                        text6,
//                                        GuiStringTable.Seconds(maxDurationLeft, "#0.0")
//                                            });
//                                        }
//                                        else {
//                                            stringBuilder5.Append(text6);
//                                        }
//                                    }
//                                }
//                            }
//                            StringBuilder stringBuilder7 = new StringBuilder();
//                            if (!info.AttackIsHostile) {
//                                if (string.IsNullOrEmpty(stringBuilder6.ToString().Trim())) {
//                                    return;
//                                }
//                                stringBuilder7.AppendGuiFormat(824, new object[]
//                                {
//                            text2,
//                            text3,
//                            name
//                                });
//                            }
//                            else if ((info.DamageAmount != 0f || !primaryAttack) && info.MinDamage > 0f) {
//                                info.SimpleAttackReport(simpleFormatString, text, text3, info.GetExtraAttackEffects(), stringBuilder7);
//                            }
//                            else {
//                                if (info.MinDamage == 0f && string.IsNullOrEmpty(stringBuilder6.ToString().Trim())) {
//                                    return;
//                                }
//                                if (info.DefendedBy != DefenseType.None) {
//                                    info.SimpleAttackReportNoDamage(GuiStringTable.GetTextWithLinks(1324, Gender.Neuter), text, text3, stringBuilder7);
//                                }
//                                else {
//                                    stringBuilder7.AppendGuiFormat(824, new object[]
//                                    {
//                                text2,
//                                text3,
//                                name
//                                    });
//                                }
//                            }
//                            string str = string.Empty;
//                            if (stringBuilder5.Length > 0) {
//                                str = GuiStringTable.Format(1731, new object[]
//                                {
//                            stringBuilder5.ToString()
//                                });
//                            }
//                            if (AttackBase.s_PostUseDelta) {
//                                DPSDebug(info, attacker);
//                                Console.InsertBatchedMessage(stringBuilder7.ToString() + str, stringBuilder6.ToString(), attacker, messageColor, Console.Instance.MessageDelta, info);
//                            }
//                            else {
//                                DPSDebug(info, attacker);
//                                Console.AddBatchedMessage(stringBuilder7.ToString() + str, stringBuilder6.ToString(), attacker, messageColor, info);
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex) {
//                Console.AddMessage("PostAttackMessages Error: " + ex.Message, Color.red);
//                Debug.LogException(ex);
//            }
//        }

//        [NewMember]
//        static void DPSDebug(DamageInfo info, GameObject attacker) {
//            float value2 = Mathf.Max(0f, info.ArmorAdjustedDamage);
//            float totdmg = info.FinalAdjustedDamage + value2;
//            UnityEngine.Debug.Log("Damage is: " + info.FinalAdjustedDamage + " From: " + attacker.name);
//            Game.Console.AddMessage("Damage is: " + totdmg + " From: " + attacker.name);
//        }
//    }
//}
