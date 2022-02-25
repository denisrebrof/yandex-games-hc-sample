using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Utils.Editor
{
    public abstract class DefineSymbolsFieldController<T>
    {
        protected virtual BuildTargetGroup TargetGroup => BuildTargetGroup.WebGL;

        protected abstract Dictionary<T, string> Symbols { get; }

        protected void SetVariant(T fieldValueVariant)
        {
            Debug.Log("Set Var");
            CleanDefineSymbols();
            var symbolValue = Symbols[fieldValueVariant];
            Debug.Log("symbolValue " + symbolValue);
            if (!string.IsNullOrWhiteSpace(symbolValue))
            {
                SetPlatformDefineSymbol(symbolValue);
            }
            AssetDatabase.Refresh();
        }

        private void CleanDefineSymbols()
        {
            var currData = PlayerSettings.GetScriptingDefineSymbolsForGroup(TargetGroup);
            var definitions = currData.Split(';').ToList();
            var sdkDefinitions = Symbols.Values.ToList();
            definitions = definitions.Where(definition => !sdkDefinitions.Contains(definition)).ToList();
            var cleanedData = string.Join(";", definitions);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(TargetGroup, cleanedData);
        }

        private void SetPlatformDefineSymbol(string symbol)
        {
            var currData = PlayerSettings.GetScriptingDefineSymbolsForGroup(TargetGroup);
            //Add closure ';' if not exists
            if (currData.Length > 0 && !currData[currData.Length - 1].Equals(';')) currData += ';';
            currData += symbol;
            Debug.Log("currData " + currData);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(TargetGroup, currData);
        }
    }
}