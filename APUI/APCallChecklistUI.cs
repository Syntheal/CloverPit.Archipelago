using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CloverPit.Archipelago.APUI
{
    public enum CallCategory
    {
        Normal,
        Evil
    }

    public struct CallEntry
    {
        public string LocationName;
        public CallCategory Category;
        public AbilityScript.Identifier Identifier;
        public CallEntry(string locationName, CallCategory category, AbilityScript.Identifier identifier)
        {
            LocationName = locationName;
            Category = category;
            Identifier = identifier;
        }
    }
    public static class APCallTable
    {
        public static readonly CallEntry[] Calls =
        {
            // -------- NORMAL --------
            new CallEntry("Call: Please don't throw my stuff away!", CallCategory.Normal, AbilityScript.Identifier.extraSpace),
            new CallEntry("Call: I can't quit now!", CallCategory.Normal, AbilityScript.Identifier.jackpotIncreaseBase),
            new CallEntry("Call: Can I borrow some green?", CallCategory.Normal, AbilityScript.Identifier.ticketsPlus5),
            new CallEntry("Call: Can I eat something?", CallCategory.Normal, AbilityScript.Identifier.discount1),

            new CallEntry("Call: This time I'm betting on Green!", CallCategory.Normal, AbilityScript.Identifier.rndCharmMod_Clover),
            new CallEntry("Call: This time I'm betting on Yellow!", CallCategory.Normal, AbilityScript.Identifier.rndCharmMod_SymbMult),
            new CallEntry("Call: This time I'm betting on Orange!", CallCategory.Normal, AbilityScript.Identifier.rndCharmMod_PatMult),
            new CallEntry("Call: If I keep trying, the patterns will align!", CallCategory.Normal, AbilityScript.Identifier.rndCharmMod_Obsessive),
            new CallEntry("Call: It's kinda fun!", CallCategory.Normal, AbilityScript.Identifier.rndCharmMod_Gambler),
            new CallEntry("Call: I'm sure the value will rise!", CallCategory.Normal, AbilityScript.Identifier.rndCharmMod_Speculative),

            new CallEntry("Call: Can you give me some Energy Drinks?", CallCategory.Normal, AbilityScript.Identifier.rechargeRedButtonPowerups),

            new CallEntry("Call: Life gave me lemons", CallCategory.Normal, AbilityScript.Identifier.symbolChances_Lemon),
            new CallEntry("Call: I used to eat healthy...", CallCategory.Normal, AbilityScript.Identifier.symbolChances_Cherry),
            new CallEntry("Call: Today's my lucky day!", CallCategory.Normal, AbilityScript.Identifier.symbolChances_Clover),
            new CallEntry("Call: I need to be there!", CallCategory.Normal, AbilityScript.Identifier.symbolChances_Bell),
            new CallEntry("Call: Please! I'll give you anything!", CallCategory.Normal, AbilityScript.Identifier.symbolChances_Diamond),
            new CallEntry("Call: I need money!", CallCategory.Normal, AbilityScript.Identifier.symbolChances_Coins),
            new CallEntry("Call: I didn't hurt anybody...", CallCategory.Normal, AbilityScript.Identifier.symbolChances_Seven),

            new CallEntry("Call: I need supplements!", CallCategory.Normal, AbilityScript.Identifier.symbolsValue_LemonAndCherry),
            new CallEntry("Call: I'm feeling lucky!", CallCategory.Normal, AbilityScript.Identifier.symbolsValue_CloverAndBell),
            new CallEntry("Call: Gold and Diamonds are a good investment!", CallCategory.Normal, AbilityScript.Identifier.symbolsValue_DiamondAndCoins),
            new CallEntry("Call: I'm gonna go \"All In\"!", CallCategory.Normal, AbilityScript.Identifier.symbolsValue_Seven),

            new CallEntry("Call: I'm thinking of some strategies!", CallCategory.Normal, AbilityScript.Identifier.patternsValue_3LessElements),
            new CallEntry("Call: I found a winning strategy!", CallCategory.Normal, AbilityScript.Identifier.patternsValue_4MoreElements),

            // -------- EVIL --------
            new CallEntry("Call: I like cryptic values!", CallCategory.Evil, AbilityScript.Identifier.evilGeneric_DoubleCloversButMoney0),
            new CallEntry("Call: I love shiny stuff!", CallCategory.Evil, AbilityScript.Identifier.evilGeneric_ShinyObjects),
            new CallEntry("Call: I don't care about the price!", CallCategory.Evil, AbilityScript.Identifier.evilGeneric_2FreeItemsTicketsZero),
            new CallEntry("Call: My head hurts!", CallCategory.Evil, AbilityScript.Identifier.evilGeneric_TakeOtherAbilitiesButDeviousMod),
            new CallEntry("Call: Give me back my money!", CallCategory.Evil, AbilityScript.Identifier.evilGeneric_DoubleCoinsTicketsZero),

            new CallEntry("Call: There's nothing to eat but mould. ", CallCategory.Evil, AbilityScript.Identifier.evilHalvenChances_LemonAndCherry),
            new CallEntry("Call: Wait, what day is it?", CallCategory.Evil, AbilityScript.Identifier.evilHalvenChances_CloverAndBell),
            new CallEntry("Call: I've already bet it all!", CallCategory.Evil, AbilityScript.Identifier.evilHalvenChances_DiamondCoinsAndSeven),
        };
    }

    public class APCallChecklistUI : MonoBehaviour
    {
        private const float PanelWidth = 420f;
        private const float PanelHeight = 810f;
        private const float Padding = 10f;
        private const float TopOffset = 20f;

        private const float ReferenceWidth = 1920f;
        private const float ReferenceHeight = 1080f;

        private Matrix4x4 oldMatrix;

        private void BeginGUIScale()
        {
            oldMatrix = GUI.matrix;
            float scale = Mathf.Min(Screen.width / ReferenceWidth, Screen.height / ReferenceHeight);
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(scale, scale, 1f));
        }

        private void EndGUIScale()
        {
            GUI.matrix = oldMatrix;
        }

        private void OnGUI()
        {
            if (!APState.IsConnected || !APState.APSaveLoaded || !APState.IsAPUIOpen)
                return;

            BeginGUIScale();

            Rect panelRect = new Rect(
                ReferenceWidth - PanelWidth - 20f,
                TopOffset,
                PanelWidth,
                PanelHeight
            );

            DrawPanel(panelRect);
            EndGUIScale();
        }

        private void DrawPanel(Rect panelRect)
        {
            GUI.color = Color.black;
            GUI.DrawTexture(panelRect, Texture2D.whiteTexture);
            GUI.color = Color.white;

            DrawBorder(panelRect);

            float y = panelRect.y + Padding;

            GUI.Label(
                new Rect(panelRect.x, y, panelRect.width, 28),
                "CALL LOCATION TRACKER",
                new GUIStyle(GUI.skin.label)
                {
                    fontSize = 18,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = Color.white }
                });

            y += 35f;

            float contentX = panelRect.x + Padding;
            float contentWidth = panelRect.width - Padding * 2;

            DrawCategory(ref y, contentX, contentWidth, CallCategory.Normal, Color.white);
            DrawCategory(ref y, contentX, contentWidth, CallCategory.Evil, Color.white);
        }

        private void DrawCategory(ref float y, float x, float width, CallCategory category, Color headerColor)
        {
            GUIStyle header = new GUIStyle(GUI.skin.label)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                normal = { textColor = headerColor }
            };

            GUI.Label(new Rect(x, y, width, 26), category.ToString().ToUpper(), header);
            y += 28f;

            foreach (var call in APCallTable.Calls.Where(c => c.Category == category))
            {
                DrawCallLine(ref y, x, width, call);
            }

            y += 12f;
        }

        private void DrawCallLine(ref float y, float x, float width, CallEntry call)
        {
            long id = APLocations.GetLocationId(call.LocationName);
            bool checkedLoc = id != -1 && APLocationManager.IsChecked(id);

            bool abilityUnlocked =
                APState.UnlockedPhoneAbilities.Contains(call.Identifier);

            float leftWidth = width * 0.70f;
            float rightWidth = width * 0.30f;

            GUIStyle leftStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 13,
                wordWrap = true,
                normal = { textColor = checkedLoc ? Color.green : Color.red }
            };

            GUIStyle rightStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 13,
                alignment = TextAnchor.UpperRight,
                normal = { textColor = abilityUnlocked ? Color.green : Color.red }
            };

            string leftText = (checkedLoc ? "✔ " : "✖ ") + call.LocationName.Replace("Call: ", "");
            string rightText = abilityUnlocked ? "(Unlocked)" : "(Not Unlocked)";

            float leftHeight = leftStyle.CalcHeight(new GUIContent(leftText), leftWidth);
            float rightHeight = rightStyle.CalcHeight(new GUIContent(rightText), rightWidth);
            float rowHeight = Mathf.Max(leftHeight, rightHeight);

            Rect leftRect = new Rect(x, y, leftWidth, rowHeight);
            Rect rightRect = new Rect(x + leftWidth, y, rightWidth, rowHeight);

            GUI.Label(leftRect, leftText, leftStyle);
            GUI.Label(rightRect, rightText, rightStyle);

            y += rowHeight + 1f;
        }



        private void DrawBorder(Rect r)
        {
            GUI.DrawTexture(new Rect(r.x, r.y, r.width, 1), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(r.x, r.yMax - 1, r.width, 1), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(r.x, r.y, 1, r.height), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(r.xMax - 1, r.y, 1, r.height), Texture2D.whiteTexture);
        }
    }
}

