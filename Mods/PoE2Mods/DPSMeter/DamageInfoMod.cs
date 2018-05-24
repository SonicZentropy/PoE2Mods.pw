//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Game;
//using Game.GameData;
//using Game.UI;
//using Onyx;
//using Patchwork;
//using UnityEngine;

//namespace PoE2Mods.DPSMeter
//{
//    [ModifiesType("Game.DamageInfo")]
//    class DamageInfoMod : Game.DamageInfo
//    {
//        [ModifiesMember("GetDamageReport")]
//        public void GetDamageReportNew(StringBuilder stringBuilder)
//        {
//            if (this.MaxDamage > 0f && this.DamageType != DamageType.None) {
//                string value = string.Empty;
//                if (this.PostDtDamageMult != 1f) {
//                    value = string.Concat(new object[]
//                    {
//                " ",
//                '×',
//                " ",
//                GuiStringTable.GetText(166),
//                ":",
//                this.PostDtDamageMult.ToStringLocal("#0.##"), "ZZZZ"
//                    });
//                }
//                CharacterStats component = ComponentUtils.GetComponent<CharacterStats>(this.Target);
//                string value2;
//                if (component != null && component.IsImmuneToDamageType(this.DamageType)) {
//                    value2 = GuiStringTable.GetText(2187);
//                }
//                else {
//                    value2 = Mathf.Max(0f, this.ArmorAdjustedDamage).ToStringLocal("#0.0") + " " + UICharacterStatIconGetter.GetDamageTypeSpriteTag(this.DamageType);
//                    Game.Console.AddMessage("IN XXXX with value2 = " + value2);
//                }
//                string text = string.Empty;
//                if (this.Damage != null) {
//                    for (int i = 0; i < this.ProcDamage.Length; i++) {
//                        if (this.ProcDamage[i] > 0f) {
//                            Game.Console.AddMessage("IN YYYY");
//                            string text2 = text;
//                            text = string.Concat(new string[]
//                            {
//                        text2,
//                        " + ",
//                        this.ProcDamage[i].ToStringLocal("#0.0"),
//                        " ",
//                        UICharacterStatIconGetter.GetDamageTypeSpriteTag((DamageType)i)
//                            });
//                        }
//                    }
//                }
//                if (this.DamageType == DamageType.Raw) {
//                    Game.Console.AddMessage("AAAA");
//                    stringBuilder.Append(GuiStringTable.GetText(428));
//                    stringBuilder.Append(": ");
//                    stringBuilder.Append(this.DamageAmount.Value.ToStringLocal("#0.0"));
//                    stringBuilder.Append(" = ");
//                    stringBuilder.Append(value2);
//                    stringBuilder.Append('.');
//                    stringBuilder.Append("JJJJ");
//                }
//                else {
//                    string text3 = TextTagging.CreateTintedSpriteTag("CS_Penetration") + ":" + this.PenetrationRating.ToStringLocal("#0.#");
//                    Game.Console.AddMessage("KKKK");
//                    float num = (float)ThresholdValue.GetIndexForThreshold(GlobalGameSettingsGameData.Instance.PenetrationRatioMultipliers, this.PenetrationRatio);
//                    string text4 = TextTagging.CreateTintedSpriteTag("CS_AR") + ":" + this.ArmorRating.ToStringLocal("#0.#");
//                    string text5;
//                    if (num >= (float)(GlobalGameSettingsGameData.Instance.PenetrationRatioMultipliers.Length - 1)) {
//                        text5 = GuiStringTable.GetText(2948);
//                    }
//                    else if (num > 0f) {
//                        text5 = GuiStringTable.GetText(2947);
//                    }
//                    else {
//                        text5 = GuiStringTable.GetText(2946);
//                    }
//                    stringBuilder.Append(GuiStringTable.GetText(2585));
//                    stringBuilder.Append(": ");
//                    stringBuilder.Append("2222");
//                    stringBuilder.Append(GuiStringTable.Format(1605, new object[]
//                    {
//                text3,
//                text4
//                    }));
//                    stringBuilder.AppendGuiFormat(1731, new object[]
//                    {
//                text5
//                    });
//                    stringBuilder.AppendLine();
//                    stringBuilder.Append(GuiStringTable.GetText(428));
//                    stringBuilder.Append(": ");
//                    stringBuilder.Append(GuiStringTable.GetText(426));
//                    stringBuilder.Append(':');
//                    stringBuilder.Append(this.DamageBase.ToStringLocal("#0.0"));
//                    stringBuilder.Append(" => ");
//                    if (!string.IsNullOrEmpty(value)) {
//                        Game.Console.AddMessage("3333");
//                        stringBuilder.Append(this.DamageAmount.Value.ToStringLocal("#0.0"));
//                        stringBuilder.Append(value);
//                        stringBuilder.Append(" = ");
//                    }

//                    stringBuilder.Append("BEFORE");
//                    stringBuilder.Append(value2);
//                    stringBuilder.Append(text);
//                    stringBuilder.Append(".A.A.A");
//                }
//            }
//        }
//    }
//}
