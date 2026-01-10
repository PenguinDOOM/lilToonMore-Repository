// Created by ChatGPT

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class CopyLilblockPropsWindow : EditorWindow
{
    [MenuItem("Tools/Copy lilblock Property Tools")]
    public static void ShowWindow()
    {
        GetWindow<CopyLilblockPropsWindow>("lilblock Props");
    }

    private void OnGUI()
    {
        GUILayout.Label("lilblock Property Tools", EditorStyles.boldLabel);

        if (GUILayout.Button("1. Copy Property Names"))
        {
            CopyPropertyNames();
        }

        if (GUILayout.Button("2. Copy Declarations"))
        {
            CopyDeclarations();
        }

        if (GUILayout.Button("3. Copy FindProperty Calls"))
        {
            CopyFindPropertyCalls();
        }
    }

    // パース関数（コメント行は無視、空行はグループ区切り、型省略対応）
    private List<(string propName, string varName, bool isGroupBreak)> ParseProperties()
    {
        string projectRoot = Directory.GetParent(Application.dataPath).FullName;
        string filePath = Path.Combine(
            projectRoot,
            "Packages",
            "jp.penguin.liltoonmore",
            "Shaders",
            "lilCustomShaderProperties.lilblock"
        );

        if (!File.Exists(filePath))
        {
            EditorUtility.DisplayDialog("Error", "ファイルが見つかりません:\n" + filePath, "OK");
            return null;
        }

        string[] lines = File.ReadAllLines(filePath);
        var props = new List<(string, string, bool)>();

        string lastType = null;
        bool previousWasProp = false; // 直前にプロパティ行があったかどうか（コメントはこれを壊さない）

        foreach (var rawLine in lines)
        {
            string line = rawLine;
            string trimmed = line.Trim();

            // 空行はグループ区切りを意味する -> previousWasProp を false にして次のプロパティを新グループにする
            if (string.IsNullOrEmpty(trimmed))
            {
                previousWasProp = false;
                continue;
            }

            // コメント行（行頭が //）は **無視**（ただし previousWasProp を壊さない）
            if (trimmed.StartsWith("//"))
            {
                // コメントアウトされたプロパティ（例: "//[lil] _Foo ..."）もここで無視される
                continue;
            }

            // 型とプロパティ抽出（型が省略されている場合は前行の型を使用）
            Match match = Regex.Match(line, @"(?:\[(.+?)\])?\s*(_[A-Za-z0-9_]+)");
            if (match.Success)
            {
                string type = match.Groups[1].Success ? match.Groups[1].Value : lastType;
                string prop = match.Groups[2].Value;

                // 変数名に変換（先頭のアンダースコアを削除して先頭小文字）
                string varName = prop.Length > 1
                    ? char.ToLower(prop[1]) + prop.Substring(2)
                    : prop; // 念のための保険

                // 前行がプロパティでなければ新しいグループ（isGroupBreak = true）
                bool isGroupBreak = !previousWasProp;

                props.Add((prop, varName, isGroupBreak));

                previousWasProp = true;
                lastType = type;
            }
            else
            {
                // プロパティでない任意の行（ただしコメントは既に除外済）に遭遇した場合はグループ終了扱い
                previousWasProp = false;
            }
        }

        return props;
    }

    // 1. プロパティ名のみ（グループごとに空行）
    private void CopyPropertyNames()
    {
        var props = ParseProperties();
        if (props == null || props.Count == 0) return;

        List<string> result = new List<string>();

        foreach (var p in props)
        {
            if (p.isGroupBreak && result.Count > 0)
                result.Add(""); // グループ区切りの空行

            result.Add(p.propName);
        }

        GUIUtility.systemCopyBuffer = string.Join("\n", result);
        EditorUtility.DisplayDialog("Success", "プロパティ名をクリップボードにコピーしました。", "OK");
    }

    // 2. 変数宣言（private MaterialProperty varName;）
    private void CopyDeclarations()
    {
        var props = ParseProperties();
        if (props == null || props.Count == 0) return;

        List<string> result = new List<string>();

        foreach (var p in props)
        {
            if (p.isGroupBreak && result.Count > 0)
                result.Add(""); // グループ区切りの空行

            result.Add("private MaterialProperty " + p.varName + ";");
        }

        GUIUtility.systemCopyBuffer = string.Join("\n", result);
        EditorUtility.DisplayDialog("Success", "MaterialProperty 宣言をクリップボードにコピーしました。", "OK");
    }

    // 3. FindProperty 呼び出し（varName = FindProperty("_PropName", props);）
    private void CopyFindPropertyCalls()
    {
        var props = ParseProperties();
        if (props == null || props.Count == 0) return;

        List<string> result = new List<string>();

        foreach (var p in props)
        {
            if (p.isGroupBreak && result.Count > 0)
                result.Add(""); // グループ区切りの空行

            result.Add(p.varName + " = FindProperty(\"" + p.propName + "\", props);");
        }

        GUIUtility.systemCopyBuffer = string.Join("\n", result);
        EditorUtility.DisplayDialog("Success", "FindProperty 呼び出しコードをクリップボードにコピーしました。", "OK");
    }
}
