﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using RimWorld.Planet;

namespace TinyTweaks
{

    public class Dialog_AssignCaravanFoodRestrictions : Window
    {

        public Dialog_AssignCaravanFoodRestrictions(Caravan caravan)
        {
            // I totally didn't just copy-paste Dialog_AssignCaravanDrugPolicies and adapt it
            this.caravan = caravan;
            doCloseButton = true;
        }

        public override Vector2 InitialSize => new Vector2(550f, 500f);

        public override void DoWindowContents(Rect rect)
        {
            rect.height -= CloseButSize.y;
            var num = 0f;
            var rect2 = new Rect(rect.width - 354f - 16f, num, AssignDrugPolicyButtonsTotalWidth, ManageDrugPoliciesButtonHeight);
            if (Widgets.ButtonText(rect2, "ManageFoodRestrictions".Translate(), true, false, true))
            {
                Find.WindowStack.Add(new Dialog_ManageFoodRestrictions(null));
            }
            num += 42f;
            var outRect = new Rect(0f, num, rect.width, rect.height - num);
            var viewRect = new Rect(0f, 0f, rect.width - 16f, lastHeight);
            Widgets.BeginScrollView(outRect, ref scrollPos, viewRect, true);
            var num2 = 0f;
            for (var i = 0; i < caravan.pawns.Count; i++)
            {
                if (caravan.pawns[i].foodRestriction != null)
                {
                    if (num2 + RowHeight >= scrollPos.y && num2 <= scrollPos.y + outRect.height)
                    {
                        DoRow(new Rect(0f, num2, viewRect.width, RowHeight), caravan.pawns[i]);
                    }
                    num2 += RowHeight;
                }
            }
            lastHeight = num2;
            Widgets.EndScrollView();
        }

        private void DoRow(Rect rect, Pawn pawn)
        {
            var rect2 = new Rect(rect.x, rect.y, rect.width - AssignDrugPolicyButtonsTotalWidth, RowHeight);
            Text.Anchor = TextAnchor.MiddleLeft;
            Text.WordWrap = false;
            Widgets.Label(rect2, pawn.LabelCap);
            Text.Anchor = TextAnchor.UpperLeft;
            Text.WordWrap = true;
            GUI.color = Color.white;
            var rect3 = new Rect(rect.x + rect.width - AssignDrugPolicyButtonsTotalWidth, rect.y, AssignDrugPolicyButtonsTotalWidth, RowHeight);
            DoAssignFoodRestrictionButtons(rect3, pawn);
        }

        private void DoAssignFoodRestrictionButtons(Rect rect, Pawn pawn)
        {
            var num = Mathf.FloorToInt((rect.width - 4f) * 0.714285731f);
            var num2 = Mathf.FloorToInt((rect.width - 4f) * 0.2857143f);
            var num3 = rect.x;
            var rect2 = new Rect(num3, rect.y + 2f, num, rect.height - 4f);
            Rect rect3 = rect2;
            FoodRestriction getPayload(Pawn p)
            {
                return p.foodRestriction.CurrentFoodRestriction;
            }

            var menuGenerator = new Func<Pawn, IEnumerable<Widgets.DropdownMenuElement<FoodRestriction>>>(Button_GenerateMenu);
            var buttonLabel = pawn.foodRestriction.CurrentFoodRestriction.label.Truncate(rect2.width, null);
            var label = pawn.foodRestriction.CurrentFoodRestriction.label;
            Widgets.Dropdown(rect3, pawn, getPayload, menuGenerator, buttonLabel, null, label, null, null, true);
            num3 += num;
            num3 += 4f;
            var rect4 = new Rect(num3, rect.y + 2f, num2, rect.height - 4f);
            if (Widgets.ButtonText(rect4, "AssignTabEdit".Translate(), true, false, true))
            {
                Find.WindowStack.Add(new Dialog_ManageFoodRestrictions(pawn.foodRestriction.CurrentFoodRestriction));
            }
            num3 += num2;
        }

        private IEnumerable<Widgets.DropdownMenuElement<FoodRestriction>> Button_GenerateMenu(Pawn pawn)
        {
            using (List<FoodRestriction>.Enumerator enumerator = Current.Game.foodRestrictionDatabase.AllFoodRestrictions.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    FoodRestriction foodRestriction = enumerator.Current;
                    yield return new Widgets.DropdownMenuElement<FoodRestriction>
                    {
                        option = new FloatMenuOption(foodRestriction.label, delegate ()
                        {
                            pawn.foodRestriction.CurrentFoodRestriction = foodRestriction;
                        }, MenuOptionPriority.Default, null, null, 0f, null, null),
                        payload = foodRestriction
                    };
                }
            }
            yield break;
        }

        private readonly Caravan caravan;

        private Vector2 scrollPos;

        private float lastHeight;

        private const float RowHeight = 30f;

        private const float AssignDrugPolicyButtonsTotalWidth = 354f;

        private const float ManageDrugPoliciesButtonHeight = 32f;

    }

}
