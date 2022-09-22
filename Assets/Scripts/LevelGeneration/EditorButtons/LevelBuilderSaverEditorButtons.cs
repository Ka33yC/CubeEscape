#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace LevelGeneration.EditorButtons
{
	[CustomEditor(typeof(LevelBuilderSaver))]
	public class LevelBuilderSaverEditorButtons : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			LevelBuilderSaver levelBuilder = (LevelBuilderSaver)target;

			if (GUILayout.Button("Save"))
			{
				levelBuilder.Save();
			}
		}
	}
}
#endif