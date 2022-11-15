using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Voxell.Inspector
{
    public static class VXEditorStyles
    {
        public const int SPACE_A = 20, SPACE_B = 10;

        public static readonly GUIStyleState foldoutNormal = new GUIStyleState { textColor = Color.gray };
        public static readonly GUIStyleState foldoutOnNormal = new GUIStyleState { textColor = new Color(0.7f, 1f, 1f, 1f) };

        public static readonly GUIStyleState subFoldoutNormal = new GUIStyleState { textColor = Color.gray * Color.cyan };
        public static readonly GUIStyleState subFoldoutOnNormal = new GUIStyleState { textColor = Color.cyan };

        public static GUIStyle CenteredTitleStyle => new GUIStyle(EditorStyles.largeLabel)
        {
            alignment = TextAnchor.UpperCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 18,
            fixedHeight = 26
        };

        public static GUIStyle CenteredLabelStyle => new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.UpperCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 12
        };

        public static GUIStyle FoldoutStyle => new GUIStyle(EditorStyles.foldout)
        {
            fontStyle = FontStyle.Bold,
            fontSize = 14,
            normal = foldoutNormal,
            onNormal = foldoutOnNormal,
            hover = foldoutOnNormal
        };

        public static GUIStyle SubFoldoutStyle => new GUIStyle(EditorStyles.foldout)
        {
            fontStyle = FontStyle.Bold,
            fontSize = 12,
            normal = subFoldoutNormal,
            onNormal = subFoldoutOnNormal
        };

        public static GUIStyle ReordableFoldoutStyle => new GUIStyle(GUI.skin.label)
        {
            fontStyle = FontStyle.Bold,
            fontSize = 12,
            normal = subFoldoutNormal,
            onNormal = subFoldoutOnNormal
        };

        public static GUIStyle NotesLabel => new GUIStyle(GUI.skin.label)
        {
            fontStyle = FontStyle.Italic,
            fontSize = 10,
            alignment = TextAnchor.MiddleRight
        };

        public static GUIStyle parentBox => new GUIStyle(GUI.skin.box)
        { padding = new RectOffset(15, 15, 15, 15) };

        public static GUIStyle box => new GUIStyle(GUI.skin.box)
        { padding = new RectOffset(10, 10, 10, 10) };
    }
}