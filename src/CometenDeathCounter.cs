using System;
using System.Globalization;
using System.Text;

public class CPHInline
{
    // Existing persisted globals from the original Death Counter export.
    // "tottal" is intentionally kept misspelled to preserve the old total.
    private const string VarDeaths = "death";
    private const string VarKills = "kills";
    private const string VarTotalKills = "tottal";
    private const string VarTotalDeaths = "totaldeath";

    // Death Counter settings.
    private const string VarTitle = "DC_Title";
    private const string VarScene = "DC_Scene";
    private const string VarSource = "DC_Source";
    private const string VarUpdateObs = "DC_UpdateObs";
    private const string VarPosX = "DC_PosX";
    private const string VarPosY = "DC_PosY";
    private const string VarScale = "DC_Scale";
    private const string VarBoxWidth = "DC_BoxWidth";
    private const string VarBackgroundOpacity = "DC_BackgroundOpacity";
    private const string VarAccentColor = "DC_AccentColor";
    private const string VarTitleColor = "DC_TitleColor";
    private const string VarTextColor = "DC_TextColor";
    private const string VarLabelColor = "DC_LabelColor";
    private const string VarBackgroundColor = "DC_BackgroundColor";
    private const string VarShowDeaths = "DC_ShowDeaths";
    private const string VarShowKills = "DC_ShowKills";
    private const string VarShowTotal = "DC_ShowTotal";
    private const string VarShowTotalDeaths = "DC_ShowTotalDeaths";

    private const string DefaultTitle = "Invasion";
    private const string DefaultScene = "Live";
    private const string DefaultSource = "death couter";

    public bool Execute()
    {
        int deaths = GetGlobalInt(VarDeaths, 0);
        int kills = GetGlobalInt(VarKills, 0);
        int totalKills = GetGlobalInt(VarTotalKills, 0);
        int totalDeaths = GetGlobalInt(VarTotalDeaths, deaths);

        string title = GetGlobalString(VarTitle, DefaultTitle);
        string scene = GetGlobalString(VarScene, DefaultScene);
        string source = GetGlobalString(VarSource, DefaultSource);
        // Legacy OBS GDI+ output is retired in V1.6.
        bool updateObs = false;
        bool showDeaths = GetGlobalBool(VarShowDeaths, true);
        bool showKills = GetGlobalBool(VarShowKills, true);
        bool showTotal = GetGlobalBool(VarShowTotal, true);
        bool showTotalDeaths = GetGlobalBool(VarShowTotalDeaths, true);

        double posX = Clamp(GetGlobalDouble(VarPosX, 50.0), 3.0, 97.0);
        double posY = Clamp(GetGlobalDouble(VarPosY, 15.0), 4.0, 96.0);
        double scale = Clamp(GetGlobalDouble(VarScale, 1.0), 0.35, 2.5);
        double boxWidth = Clamp(GetGlobalDouble(VarBoxWidth, 510.0), 320.0, 1000.0);
        double backgroundOpacity = Clamp(GetGlobalDouble(VarBackgroundOpacity, 0.94), 0.0, 1.0);

        string accentColor = NormalizeHex(GetGlobalString(VarAccentColor, "#ff8a1f"), "#ff8a1f");
        string titleColor = NormalizeHex(GetGlobalString(VarTitleColor, "#ff9b3f"), "#ff9b3f");
        string textColor = NormalizeHex(GetGlobalString(VarTextColor, "#ffffff"), "#ffffff");
        string labelColor = NormalizeHex(GetGlobalString(VarLabelColor, "#aab2c0"), "#aab2c0");
        string backgroundColor = NormalizeHex(GetGlobalString(VarBackgroundColor, "#0d1016"), "#0d1016");

        string operation = ResolveOperation();
        string resultMessage = "State synchronized.";
        bool sendStatusToChat = false;
        bool persistCounters = false;
        bool persistSettings = false;
        bool saveVerified = false;
        string saveToken = GetArgString("saveToken", "");

        switch (operation)
        {
            case "adddeath":
            case "death+":
            case "incrementdeath":
            {
                int amount = Math.Max(1, GetArgInt("amount", 1));
                deaths += amount;
                totalDeaths += amount;
                persistCounters = true;
                resultMessage = "Death and Total Deaths added.";
                break;
            }

            case "removedeath":
            case "death-":
            case "decrementdeath":
            {
                int amount = Math.Max(1, GetArgInt("amount", 1));
                int removable = Math.Min(amount, deaths);
                deaths -= removable;
                totalDeaths = Math.Max(0, totalDeaths - removable);
                persistCounters = true;
                resultMessage = "Death and Total Deaths removed.";
                break;
            }

            case "addkill":
            case "kill+":
            case "incrementkill":
            {
                int amount = Math.Max(1, GetArgInt("amount", 1));
                kills += amount;
                totalKills += amount;
                persistCounters = true;
                resultMessage = "Kill added.";
                break;
            }

            case "removekill":
            case "kill-":
            case "decrementkill":
            {
                int amount = Math.Max(1, GetArgInt("amount", 1));
                int removable = Math.Min(amount, kills);
                kills -= removable;
                totalKills = Math.Max(0, totalKills - removable);
                persistCounters = true;
                resultMessage = "Kill removed.";
                break;
            }

            case "reset":
            case "resetstream":
            {
                deaths = 0;
                kills = 0;
                persistCounters = true;
                resultMessage = "Stream Deaths and Kills reset. Totals preserved.";
                break;
            }

            case "resetkills":
            {
                kills = 0;
                totalKills = 0;
                persistCounters = true;
                resultMessage = "Stream Kills and Total Kills reset.";
                break;
            }

            case "resetdeaths":
            {
                deaths = 0;
                totalDeaths = 0;
                persistCounters = true;
                resultMessage = "Stream Deaths and Total Deaths reset.";
                break;
            }

            case "resetall":
            {
                deaths = 0;
                kills = 0;
                totalKills = 0;
                totalDeaths = 0;
                persistCounters = true;
                resultMessage = "All Death and Kill counters reset.";
                break;
            }

            case "set":
            case "setvalues":
            {
                int intValue;

                if (TryGetArgInt("deaths", out intValue) || TryGetArgInt("death", out intValue))
                    deaths = Math.Max(0, intValue);

                if (TryGetArgInt("kills", out intValue))
                    kills = Math.Max(0, intValue);

                if (TryGetArgInt("totalKills", out intValue) ||
                    TryGetArgInt("total", out intValue) ||
                    TryGetArgInt("tottal", out intValue))
                    totalKills = Math.Max(0, intValue);

                if (TryGetArgInt("totalDeaths", out intValue) ||
                    TryGetArgInt("totalDeath", out intValue) ||
                    TryGetArgInt("totaldeath", out intValue))
                    totalDeaths = Math.Max(0, intValue);

                persistCounters = true;
                resultMessage = "Counter values updated.";
                break;
            }

            case "saveall":
            case "saveeverything":
            {
                string stringValue;
                bool boolValue;
                double doubleValue;

                if (TryGetArgString("title", out stringValue))
                    title = stringValue.Trim();

                if (TryGetArgBool("showDeaths", out boolValue))
                    showDeaths = boolValue;

                if (TryGetArgBool("showKills", out boolValue))
                    showKills = boolValue;

                if (TryGetArgBool("showTotal", out boolValue))
                    showTotal = boolValue;

                if (TryGetArgBool("showTotalDeaths", out boolValue))
                    showTotalDeaths = boolValue;

                if (TryGetArgDouble("posX", out doubleValue))
                    posX = Clamp(doubleValue, 3.0, 97.0);

                if (TryGetArgDouble("posY", out doubleValue))
                    posY = Clamp(doubleValue, 4.0, 96.0);

                if (TryGetArgDouble("scale", out doubleValue))
                    scale = Clamp(doubleValue, 0.35, 2.5);

                if (TryGetArgDouble("boxWidth", out doubleValue))
                    boxWidth = Clamp(doubleValue, 320.0, 1000.0);

                if (TryGetArgDouble("backgroundOpacity", out doubleValue))
                    backgroundOpacity = Clamp(doubleValue, 0.0, 1.0);

                if (TryGetArgString("accentColor", out stringValue))
                    accentColor = NormalizeHex(stringValue, accentColor);

                if (TryGetArgString("titleColor", out stringValue))
                    titleColor = NormalizeHex(stringValue, titleColor);

                if (TryGetArgString("textColor", out stringValue))
                    textColor = NormalizeHex(stringValue, textColor);

                if (TryGetArgString("labelColor", out stringValue))
                    labelColor = NormalizeHex(stringValue, labelColor);

                if (TryGetArgString("backgroundColor", out stringValue))
                    backgroundColor = NormalizeHex(stringValue, backgroundColor);

                updateObs = false;
                persistSettings = true;
                resultMessage = "All Death Counter settings saved and verified.";
                break;
            }

            case "saveconfig":
            case "config":
            {
                string stringValue;

                if (TryGetArgString("title", out stringValue))
                    title = stringValue.Trim();

                bool boolValue;
                updateObs = false;

                if (TryGetArgBool("showDeaths", out boolValue))
                    showDeaths = boolValue;

                if (TryGetArgBool("showKills", out boolValue))
                    showKills = boolValue;

                if (TryGetArgBool("showTotal", out boolValue))
                    showTotal = boolValue;

                if (TryGetArgBool("showTotalDeaths", out boolValue))
                    showTotalDeaths = boolValue;

                persistSettings = true;
                resultMessage = "Name and visibility saved.";
                break;
            }

            case "savevisibility":
            case "visibility":
            {
                bool boolValue;

                if (TryGetArgBool("showDeaths", out boolValue))
                    showDeaths = boolValue;

                if (TryGetArgBool("showKills", out boolValue))
                    showKills = boolValue;

                if (TryGetArgBool("showTotal", out boolValue))
                    showTotal = boolValue;

                if (TryGetArgBool("showTotalDeaths", out boolValue))
                    showTotalDeaths = boolValue;

                persistSettings = true;
                resultMessage = "Counter visibility saved.";
                break;
            }

            case "savelayout":
            case "position":
            case "layout":
            {
                double doubleValue;

                if (TryGetArgDouble("posX", out doubleValue))
                    posX = Clamp(doubleValue, 3.0, 97.0);

                if (TryGetArgDouble("posY", out doubleValue))
                    posY = Clamp(doubleValue, 4.0, 96.0);

                if (TryGetArgDouble("scale", out doubleValue))
                    scale = Clamp(doubleValue, 0.35, 2.5);

                persistSettings = true;
                resultMessage = "Overlay position saved.";
                break;
            }

            case "saveappearance":
            case "appearance":
            case "style":
            {
                string colorValue;
                double doubleValue;

                if (TryGetArgString("accentColor", out colorValue))
                    accentColor = NormalizeHex(colorValue, accentColor);

                if (TryGetArgString("titleColor", out colorValue))
                    titleColor = NormalizeHex(colorValue, titleColor);

                if (TryGetArgString("textColor", out colorValue))
                    textColor = NormalizeHex(colorValue, textColor);

                if (TryGetArgString("labelColor", out colorValue))
                    labelColor = NormalizeHex(colorValue, labelColor);

                if (TryGetArgString("backgroundColor", out colorValue))
                    backgroundColor = NormalizeHex(colorValue, backgroundColor);

                if (TryGetArgDouble("backgroundOpacity", out doubleValue))
                    backgroundOpacity = Clamp(doubleValue, 0.0, 1.0);

                if (TryGetArgDouble("boxWidth", out doubleValue))
                    boxWidth = Clamp(doubleValue, 320.0, 1000.0);

                if (TryGetArgDouble("scale", out doubleValue))
                    scale = Clamp(doubleValue, 0.35, 2.5);

                persistSettings = true;
                resultMessage = "Overlay colors and size saved.";
                break;
            }

            case "status":
            case "chat":
            case "current":
            {
                sendStatusToChat = true;
                resultMessage = "Status sent to chat.";
                break;
            }

            case "sync":
            case "refresh":
                break;

            default:
                resultMessage = "Unknown operation: " + operation;
                CPH.LogWarn("[Death Counter] " + resultMessage);
                break;
        }

        if (persistCounters)
            PersistCounters(deaths, kills, totalKills, totalDeaths);

        if (persistSettings)
        {
            string requestedTitle = title;
            double requestedPosX = posX;
            double requestedPosY = posY;
            double requestedScale = scale;
            double requestedBoxWidth = boxWidth;
            double requestedBackgroundOpacity = backgroundOpacity;
            string requestedAccentColor = accentColor;
            string requestedTitleColor = titleColor;
            string requestedTextColor = textColor;
            string requestedLabelColor = labelColor;
            string requestedBackgroundColor = backgroundColor;
            bool requestedShowDeaths = showDeaths;
            bool requestedShowKills = showKills;
            bool requestedShowTotal = showTotal;
            bool requestedShowTotalDeaths = showTotalDeaths;

            PersistSettings(
                title,
                posX,
                posY,
                scale,
                boxWidth,
                backgroundOpacity,
                accentColor,
                titleColor,
                textColor,
                labelColor,
                backgroundColor,
                showDeaths,
                showKills,
                showTotal,
                showTotalDeaths
            );

            // Read back from persisted globals. The acknowledgement now reflects
            // what Streamer.bot actually stored, not only local variables.
            title = GetGlobalString(VarTitle, requestedTitle);
            posX = Clamp(GetGlobalDouble(VarPosX, requestedPosX), 3.0, 97.0);
            posY = Clamp(GetGlobalDouble(VarPosY, requestedPosY), 4.0, 96.0);
            scale = Clamp(GetGlobalDouble(VarScale, requestedScale), 0.35, 2.5);
            boxWidth = Clamp(
                GetGlobalDouble(VarBoxWidth, requestedBoxWidth),
                320.0,
                1000.0
            );
            backgroundOpacity = Clamp(
                GetGlobalDouble(
                    VarBackgroundOpacity,
                    requestedBackgroundOpacity
                ),
                0.0,
                1.0
            );

            accentColor = NormalizeHex(
                GetGlobalString(VarAccentColor, requestedAccentColor),
                requestedAccentColor
            );
            titleColor = NormalizeHex(
                GetGlobalString(VarTitleColor, requestedTitleColor),
                requestedTitleColor
            );
            textColor = NormalizeHex(
                GetGlobalString(VarTextColor, requestedTextColor),
                requestedTextColor
            );
            labelColor = NormalizeHex(
                GetGlobalString(VarLabelColor, requestedLabelColor),
                requestedLabelColor
            );
            backgroundColor = NormalizeHex(
                GetGlobalString(
                    VarBackgroundColor,
                    requestedBackgroundColor
                ),
                requestedBackgroundColor
            );

            showDeaths = GetGlobalBool(
                VarShowDeaths,
                requestedShowDeaths
            );
            showKills = GetGlobalBool(
                VarShowKills,
                requestedShowKills
            );
            showTotal = GetGlobalBool(
                VarShowTotal,
                requestedShowTotal
            );
            showTotalDeaths = GetGlobalBool(
                VarShowTotalDeaths,
                requestedShowTotalDeaths
            );

            saveVerified =
                string.Equals(
                    title,
                    requestedTitle,
                    StringComparison.Ordinal
                ) &&
                NearlyEqual(posX, requestedPosX) &&
                NearlyEqual(posY, requestedPosY) &&
                NearlyEqual(scale, requestedScale) &&
                NearlyEqual(boxWidth, requestedBoxWidth) &&
                NearlyEqual(
                    backgroundOpacity,
                    requestedBackgroundOpacity
                ) &&
                string.Equals(
                    accentColor,
                    requestedAccentColor,
                    StringComparison.OrdinalIgnoreCase
                ) &&
                string.Equals(
                    titleColor,
                    requestedTitleColor,
                    StringComparison.OrdinalIgnoreCase
                ) &&
                string.Equals(
                    textColor,
                    requestedTextColor,
                    StringComparison.OrdinalIgnoreCase
                ) &&
                string.Equals(
                    labelColor,
                    requestedLabelColor,
                    StringComparison.OrdinalIgnoreCase
                ) &&
                string.Equals(
                    backgroundColor,
                    requestedBackgroundColor,
                    StringComparison.OrdinalIgnoreCase
                ) &&
                showDeaths == requestedShowDeaths &&
                showKills == requestedShowKills &&
                showTotal == requestedShowTotal &&
                showTotalDeaths == requestedShowTotalDeaths;

            resultMessage = saveVerified
                ? "Settings saved and read back successfully."
                : "Settings could not be verified after saving.";
        }

        if (sendStatusToChat || GetArgBool("sendChat", false))
        {
            CPH.SendMessage(
                BuildStatusMessage(
                    deaths,
                    kills,
                    totalKills,
                    totalDeaths,
                    showDeaths,
                    showKills,
                    showTotal,
                    showTotalDeaths
                ),
                true,
                true
            );
        }

        string stateJson = BuildStateJson(
            operation,
            resultMessage,
            deaths,
            kills,
            totalKills,
            totalDeaths,
            title,
            scene,
            source,
            updateObs,
            posX,
            posY,
            scale,
            boxWidth,
            backgroundOpacity,
            accentColor,
            titleColor,
            textColor,
            labelColor,
            backgroundColor,
            showDeaths,
            showKills,
            showTotal,
            showTotalDeaths,
            saveToken,
            saveVerified
        );

        // Primary channel: direct JSON message to every connected WebSocket client.
        CPH.WebsocketBroadcastJson(stateJson);

        // Fallback channel: included in Raw.ActionCompleted event arguments.
        // The HTML files explicitly subscribe to that event.
        CPH.SetArgument("dcStateJson", stateJson);

        return true;
    }

    private void PersistCounters(
        int deaths,
        int kills,
        int totalKills,
        int totalDeaths)
    {
        CPH.SetGlobalVar(VarDeaths, deaths, true);
        CPH.SetGlobalVar(VarKills, kills, true);
        CPH.SetGlobalVar(VarTotalKills, totalKills, true);
        CPH.SetGlobalVar(VarTotalDeaths, totalDeaths, true);
    }

    private void PersistSettings(
        string title,
        double posX,
        double posY,
        double scale,
        double boxWidth,
        double backgroundOpacity,
        string accentColor,
        string titleColor,
        string textColor,
        string labelColor,
        string backgroundColor,
        bool showDeaths,
        bool showKills,
        bool showTotal,
        bool showTotalDeaths)
    {
        // Store settings as strings. Streamer.bot handles persisted string globals
        // consistently across command, WebSocket and Stream Deck action runs.
        CPH.SetGlobalVar(VarTitle, title ?? "", true);
        CPH.SetGlobalVar(VarUpdateObs, "false", true);

        CPH.SetGlobalVar(
            VarPosX,
            posX.ToString("0.###", CultureInfo.InvariantCulture),
            true
        );
        CPH.SetGlobalVar(
            VarPosY,
            posY.ToString("0.###", CultureInfo.InvariantCulture),
            true
        );
        CPH.SetGlobalVar(
            VarScale,
            scale.ToString("0.###", CultureInfo.InvariantCulture),
            true
        );
        CPH.SetGlobalVar(
            VarBoxWidth,
            boxWidth.ToString("0.###", CultureInfo.InvariantCulture),
            true
        );
        CPH.SetGlobalVar(
            VarBackgroundOpacity,
            backgroundOpacity.ToString("0.###", CultureInfo.InvariantCulture),
            true
        );

        CPH.SetGlobalVar(VarAccentColor, accentColor, true);
        CPH.SetGlobalVar(VarTitleColor, titleColor, true);
        CPH.SetGlobalVar(VarTextColor, textColor, true);
        CPH.SetGlobalVar(VarLabelColor, labelColor, true);
        CPH.SetGlobalVar(VarBackgroundColor, backgroundColor, true);

        CPH.SetGlobalVar(
            VarShowDeaths,
            showDeaths ? "true" : "false",
            true
        );
        CPH.SetGlobalVar(
            VarShowKills,
            showKills ? "true" : "false",
            true
        );
        CPH.SetGlobalVar(
            VarShowTotal,
            showTotal ? "true" : "false",
            true
        );
        CPH.SetGlobalVar(
            VarShowTotalDeaths,
            showTotalDeaths ? "true" : "false",
            true
        );
    }

    private void UpdateObsText(
        string scene,
        string source,
        string title,
        int deaths,
        int kills,
        int totalKills,
        int totalDeaths,
        bool showDeaths,
        bool showKills,
        bool showTotal,
        bool showTotalDeaths)
    {
        try
        {
            StringBuilder text = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(title))
                text.Append(title.Trim());

            StringBuilder streamLine = new StringBuilder();

            if (showDeaths)
                streamLine.Append("Deaths: ").Append(deaths);

            if (showKills)
            {
                if (streamLine.Length > 0)
                    streamLine.Append("  ");

                streamLine.Append("Kills: ").Append(kills);
            }

            if (streamLine.Length > 0)
            {
                if (text.Length > 0)
                    text.AppendLine();

                text.Append(streamLine);
            }

            StringBuilder totalLine = new StringBuilder();

            if (showTotal)
                totalLine.Append("Total Kills: ").Append(totalKills);

            if (showTotalDeaths)
            {
                if (totalLine.Length > 0)
                    totalLine.Append("  ");

                totalLine.Append("Total Deaths: ").Append(totalDeaths);
            }

            if (totalLine.Length > 0)
            {
                if (text.Length > 0)
                    text.AppendLine();

                text.Append(totalLine);
            }

            CPH.ObsSetGdiText(scene, source, text.ToString());
        }
        catch (Exception ex)
        {
            CPH.LogWarn("[Death Counter] Could not update OBS text source: " + ex.Message);
        }
    }

    private string BuildStatusMessage(
        int deaths,
        int kills,
        int totalKills,
        int totalDeaths,
        bool showDeaths,
        bool showKills,
        bool showTotal,
        bool showTotalDeaths)
    {
        StringBuilder status = new StringBuilder("Death Counter:");

        if (showDeaths)
            status.Append(" Deaths: ").Append(deaths).Append(".");

        if (showKills)
            status.Append(" Stream kills: ").Append(kills).Append(".");

        if (showTotal)
            status.Append(" Total kills: ").Append(totalKills).Append(".");

        if (showTotalDeaths)
            status.Append(" Total deaths: ").Append(totalDeaths).Append(".");

        if (!showDeaths && !showKills && !showTotal && !showTotalDeaths)
            status.Append(" No counters are enabled.");

        return status.ToString();
    }

    private string BuildStateJson(
        string operation,
        string message,
        int deaths,
        int kills,
        int totalKills,
        int totalDeaths,
        string title,
        string scene,
        string source,
        bool updateObs,
        double posX,
        double posY,
        double scale,
        double boxWidth,
        double backgroundOpacity,
        string accentColor,
        string titleColor,
        string textColor,
        string labelColor,
        string backgroundColor,
        bool showDeaths,
        bool showKills,
        bool showTotal,
        bool showTotalDeaths,
        string saveToken,
        bool saveVerified)
    {
        StringBuilder json = new StringBuilder();

        json.Append("{");
        json.Append("\"type\":\"DEATH_COUNTER_STATE\",");
        json.Append("\"version\":\"1.11.0\",");
        json.Append("\"operation\":").Append(JsonString(operation)).Append(",");
        json.Append("\"message\":").Append(JsonString(message)).Append(",");
        json.Append("\"deaths\":").Append(deaths).Append(",");
        json.Append("\"kills\":").Append(kills).Append(",");
        json.Append("\"totalKills\":").Append(totalKills).Append(",");
        json.Append("\"totalDeaths\":").Append(totalDeaths).Append(",");
        json.Append("\"title\":").Append(JsonString(title)).Append(",");
        json.Append("\"scene\":").Append(JsonString(scene)).Append(",");
        json.Append("\"source\":").Append(JsonString(source)).Append(",");
        json.Append("\"updateObs\":").Append(updateObs ? "true" : "false").Append(",");
        json.Append("\"posX\":").Append(posX.ToString("0.###", CultureInfo.InvariantCulture)).Append(",");
        json.Append("\"posY\":").Append(posY.ToString("0.###", CultureInfo.InvariantCulture)).Append(",");
        json.Append("\"scale\":").Append(scale.ToString("0.###", CultureInfo.InvariantCulture)).Append(",");
        json.Append("\"boxWidth\":").Append(boxWidth.ToString("0.###", CultureInfo.InvariantCulture)).Append(",");
        json.Append("\"backgroundOpacity\":").Append(backgroundOpacity.ToString("0.###", CultureInfo.InvariantCulture)).Append(",");
        json.Append("\"accentColor\":").Append(JsonString(accentColor)).Append(",");
        json.Append("\"titleColor\":").Append(JsonString(titleColor)).Append(",");
        json.Append("\"textColor\":").Append(JsonString(textColor)).Append(",");
        json.Append("\"labelColor\":").Append(JsonString(labelColor)).Append(",");
        json.Append("\"backgroundColor\":").Append(JsonString(backgroundColor)).Append(",");
        json.Append("\"showDeaths\":").Append(showDeaths ? "true" : "false").Append(",");
        json.Append("\"showKills\":").Append(showKills ? "true" : "false").Append(",");
        json.Append("\"showTotal\":").Append(showTotal ? "true" : "false").Append(",");
        json.Append("\"showTotalDeaths\":").Append(showTotalDeaths ? "true" : "false").Append(",");
        json.Append("\"saveToken\":").Append(JsonString(saveToken)).Append(",");
        json.Append("\"saveVerified\":").Append(saveVerified ? "true" : "false").Append(",");
        json.Append("\"updatedAt\":").Append(JsonString(DateTime.UtcNow.ToString("o")));
        json.Append("}");

        return json.ToString();
    }

    private string ResolveOperation()
    {
        string explicitOperation = FirstArgument(
            "operation",
            "dcOperation",
            "op"
        );

        string command = FirstArgument(
            "command",
            "commandName",
            "rawInput"
        );

        string normalizedOperation = NormalizeInvocation(explicitOperation);
        string normalizedCommand = NormalizeInvocation(command);

        CPH.LogInfo(
            "[Death Counter] Invoke - operation='" + explicitOperation +
            "', command='" + GetArgString("command", "") +
            "', commandName='" + GetArgString("commandName", "") +
            "', rawInput='" + GetArgString("rawInput", "") + "'."
        );

        if (!string.IsNullOrWhiteSpace(normalizedOperation))
        {
            string mappedOperation = MapInvocation(normalizedOperation);

            if (!string.IsNullOrWhiteSpace(mappedOperation))
            {
                CPH.LogInfo("[Death Counter] Resolved operation: " + mappedOperation);
                return mappedOperation;
            }

            // WebAdmin operations such as saveAll and sync pass through unchanged.
            CPH.LogInfo("[Death Counter] Explicit operation passed through: " + normalizedOperation);
            return normalizedOperation;
        }

        if (!string.IsNullOrWhiteSpace(normalizedCommand))
        {
            string mappedCommand = MapInvocation(normalizedCommand);

            if (!string.IsNullOrWhiteSpace(mappedCommand))
            {
                CPH.LogInfo("[Death Counter] Resolved command: " + mappedCommand);
                return mappedCommand;
            }
        }

        CPH.LogInfo("[Death Counter] No counter operation found - running sync.");
        return "sync";
    }

    private string FirstArgument(params string[] names)
    {
        for (int i = 0; i < names.Length; i++)
        {
            string value = GetArgString(names[i], "");

            if (!string.IsNullOrWhiteSpace(value))
                return value;
        }

        return "";
    }

    private string NormalizeInvocation(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "";

        string normalized = value.Trim().ToLowerInvariant();

        // Keep only the command itself when the source contains extra input.
        int firstSpace = normalized.IndexOf(' ');
        if (firstSpace >= 0)
            normalized = normalized.Substring(0, firstSpace);

        normalized = normalized.TrimStart('!', '/', '\\');
        normalized = normalized.Replace("_", "");
        normalized = normalized.Replace("-", "");
        normalized = normalized.Replace(" ", "");

        return normalized;
    }

    private string MapInvocation(string value)
    {
        switch (value)
        {
            case "kill":
            case "addkill":
            case "kill+":
            case "incrementkill":
            case "dcaddkill":
                return "addkill";

            case "death":
            case "adddeath":
            case "death+":
            case "incrementdeath":
            case "dcadddeath":
                return "adddeath";

            case "rkill":
            case "removekill":
            case "kill-":
            case "decrementkill":
            case "dcremovekill":
                return "removekill";

            case "rdeath":
            case "removedeath":
            case "death-":
            case "decrementdeath":
            case "dcremovedeath":
                return "removedeath";

            case "reset":
            case "resetstream":
            case "dcresetstream":
                return "resetstream";

            case "resetkills":
            case "resetkill":
                return "resetkills";

            case "resetdeaths":
            case "resetdeath":
                return "resetdeaths";

            case "resetall":
                return "resetall";

            case "sync":
            case "refresh":
                return "sync";

            case "saveall":
            case "saveeverything":
                return "saveall";

            case "saveconfig":
            case "config":
                return "saveconfig";

            case "savelayout":
            case "position":
            case "layout":
                return "savelayout";

            case "saveappearance":
            case "appearance":
            case "style":
                return "saveappearance";

            case "status":
            case "chat":
            case "current":
                return "status";

            default:
                return "";
        }
    }

    private int GetGlobalInt(string name, int fallback)
    {
        try
        {
            int? value = CPH.GetGlobalVar<int?>(name, true);
            return value ?? fallback;
        }
        catch
        {
            object raw = null;

            try
            {
                raw = CPH.GetGlobalVar<object>(name, true);
            }
            catch
            {
            }

            int parsed;
            if (raw != null &&
                int.TryParse(raw.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed))
                return parsed;

            return fallback;
        }
    }

    private double GetGlobalDouble(string name, double fallback)
    {
        // Preferred format in V1.10: persisted invariant string.
        try
        {
            string text = CPH.GetGlobalVar<string>(name, true);

            double parsedText;
            if (!string.IsNullOrWhiteSpace(text) &&
                double.TryParse(
                    text.Replace(',', '.'),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out parsedText))
            {
                return parsedText;
            }
        }
        catch
        {
        }

        // Migration support for values stored as numeric types by older versions.
        try
        {
            double? doubleValue = CPH.GetGlobalVar<double?>(name, true);
            if (doubleValue.HasValue)
                return doubleValue.Value;
        }
        catch
        {
        }

        try
        {
            int? intValue = CPH.GetGlobalVar<int?>(name, true);
            if (intValue.HasValue)
                return Convert.ToDouble(intValue.Value);
        }
        catch
        {
        }

        return fallback;
    }

    private string GetGlobalString(string name, string fallback)
    {
        try
        {
            string value = CPH.GetGlobalVar<string>(name, true);
            return value ?? fallback;
        }
        catch
        {
            return fallback;
        }
    }

    private bool GetGlobalBool(string name, bool fallback)
    {
        // Preferred format in V1.10: persisted "true" / "false" string.
        try
        {
            string text = CPH.GetGlobalVar<string>(name, true);

            bool parsedText;
            if (!string.IsNullOrWhiteSpace(text) &&
                bool.TryParse(text.Trim(), out parsedText))
            {
                return parsedText;
            }

            int parsedInt;
            if (!string.IsNullOrWhiteSpace(text) &&
                int.TryParse(text.Trim(), out parsedInt))
            {
                return parsedInt != 0;
            }
        }
        catch
        {
        }

        // Migration support for actual bool globals from older versions.
        try
        {
            bool? boolValue = CPH.GetGlobalVar<bool?>(name, true);
            if (boolValue.HasValue)
                return boolValue.Value;
        }
        catch
        {
        }

        return fallback;
    }

    private bool TryGetRawArg(string name, out object raw)
    {
        raw = null;

        // Official Streamer.bot argument API. Works for command triggers,
        // Set Argument, WebSocket DoAction and inline Run Action calls.
        try
        {
            if (CPH.TryGetArg(name, out raw) && raw != null)
                return true;
        }
        catch (Exception ex)
        {
            CPH.LogWarn(
                "[Death Counter] Could not read argument '" +
                name + "': " + ex.Message
            );
        }

        raw = null;
        return false;
    }

    private string GetArgString(string name, string fallback)
    {
        string value;
        return TryGetArgString(name, out value) ? value : fallback;
    }

    private bool TryGetArgString(string name, out string value)
    {
        value = null;
        object raw;

        if (!TryGetRawArg(name, out raw))
            return false;

        value = raw.ToString();
        return true;
    }

    private int GetArgInt(string name, int fallback)
    {
        int value;
        return TryGetArgInt(name, out value) ? value : fallback;
    }

    private bool TryGetArgInt(string name, out int value)
    {
        value = 0;
        object raw;

        if (!TryGetRawArg(name, out raw))
            return false;

        if (raw is int)
        {
            value = (int)raw;
            return true;
        }

        return int.TryParse(
            raw.ToString(),
            NumberStyles.Integer,
            CultureInfo.InvariantCulture,
            out value
        );
    }

    private bool TryGetArgDouble(string name, out double value)
    {
        value = 0;
        object raw;

        if (!TryGetRawArg(name, out raw))
            return false;

        if (raw is double)
        {
            value = (double)raw;
            return true;
        }

        if (raw is float)
        {
            value = Convert.ToDouble(raw);
            return true;
        }

        if (raw is int)
        {
            value = Convert.ToDouble(raw);
            return true;
        }

        return double.TryParse(
            raw.ToString().Replace(',', '.'),
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            out value
        );
    }

    private bool GetArgBool(string name, bool fallback)
    {
        bool value;
        return TryGetArgBool(name, out value) ? value : fallback;
    }

    private bool TryGetArgBool(string name, out bool value)
    {
        value = false;
        object raw;

        if (!TryGetRawArg(name, out raw))
            return false;

        if (raw is bool)
        {
            value = (bool)raw;
            return true;
        }

        string text = raw.ToString().Trim();

        if (bool.TryParse(text, out value))
            return true;

        if (text == "1")
        {
            value = true;
            return true;
        }

        if (text == "0")
        {
            value = false;
            return true;
        }

        return false;
    }

    private double Clamp(double value, double minimum, double maximum)
    {
        return Math.Max(minimum, Math.Min(maximum, value));
    }

    private bool NearlyEqual(double left, double right)
    {
        return Math.Abs(left - right) < 0.01;
    }

    private string NormalizeHex(string value, string fallback)
    {
        if (string.IsNullOrWhiteSpace(value))
            return fallback;

        string color = value.Trim();

        if (!color.StartsWith("#"))
            color = "#" + color;

        if (color.Length != 7)
            return fallback;

        for (int i = 1; i < color.Length; i++)
        {
            char c = color[i];
            bool valid =
                (c >= '0' && c <= '9') ||
                (c >= 'a' && c <= 'f') ||
                (c >= 'A' && c <= 'F');

            if (!valid)
                return fallback;
        }

        return color.ToLowerInvariant();
    }

    private string JsonString(string value)
    {
        if (value == null)
            return "null";

        StringBuilder escaped = new StringBuilder();
        escaped.Append("\"");

        foreach (char c in value)
        {
            switch (c)
            {
                case '\\': escaped.Append("\\\\"); break;
                case '"': escaped.Append("\\\""); break;
                case '\r': escaped.Append("\\r"); break;
                case '\n': escaped.Append("\\n"); break;
                case '\t': escaped.Append("\\t"); break;
                default:
                    if (c < 32)
                        escaped.Append("\\u").Append(((int)c).ToString("x4"));
                    else
                        escaped.Append(c);
                    break;
            }
        }

        escaped.Append("\"");
        return escaped.ToString();
    }
}
