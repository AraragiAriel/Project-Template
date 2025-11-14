using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(UpgradesData), true)]
public class UpgradesDataEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        UpgradesData script = target as UpgradesData;

        int width = 10;
        int counter = 0;
        int dimension = 40;
        GUILayout.BeginHorizontal();
            foreach(UpgradeData data in script.upgrades){
                if(counter % width == 0){
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                } 
                var texture = AssetPreview.GetAssetPreview(data.icon);
                GUILayout.Label(texture, GUILayout.Width(dimension), GUILayout.Height(dimension));
                counter++;
            }
        GUILayout.EndHorizontal();

        if(GUILayout.Button("Set")){
            string path = "Assets/Data/Upgrades";
            string filter = "t:UpgradeData";

            string[] assetGuids = AssetDatabase.FindAssets(filter, new[] {path});
            var datas = assetGuids
                                .Select(AssetDatabase.GUIDToAssetPath)
                                .Select(AssetDatabase.LoadAssetAtPath<UpgradeData>)
                                .Where(asset => asset != null)
                                .ToList();

            script.upgrades.Clear();
            foreach(UpgradeData data in datas)
                script.upgrades.Add(data);

            EditorUtility.SetDirty(script);
        }
        if(GUILayout.Button("Print Total Cost")){
            var enums = System.Enum.GetValues(typeof(Currency));
            List<CurrencyAmount> currencies = new();
            foreach(Currency currency in enums)
                currencies.Add(new CurrencyAmount(currency, 0f));

            foreach(UpgradeData data in script.upgrades){
                float dataTotalCost = (2*data.initialCost.amount + data.costIncrease*(data.maxLevel-1))*data.maxLevel/2;
                for(int i = 0; i < currencies.Count; i++)
                    if(currencies[i].type == data.initialCost.type)
                        currencies[i] = new CurrencyAmount(currencies[i].type, currencies[i].amount + dataTotalCost);
            }

            string toPrint = "Upgrades total cost:";
            foreach(CurrencyAmount c in currencies)
                toPrint += "\n    " + c.type + ": " + c.amount;
            Debug.Log(toPrint);
        }
    }
}
