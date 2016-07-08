/*
using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject
{

    [MenuItem("Asset/Create/Database")]
    public static void CreateDatabaseAsset()
    {

        Database database = ScriptableObject.CreateInstance<Database>();
        AssetDatabase.CreateAsset(database, "Assets/Database.asset");
        AssetDatabase.SaveAssets();
    }



}
*/