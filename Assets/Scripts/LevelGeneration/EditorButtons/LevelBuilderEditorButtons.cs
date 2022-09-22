#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace LevelGeneration.EditorButtons
{
	[CustomEditor(typeof(LevelBuilder))]
	public class LevelBuilderEditorButtons : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			LevelBuilder levelBuilder = (LevelBuilder)target;

			if (GUILayout.Button("Generate"))
			{
				levelBuilder.Generate();
			}

			// if (GUILayout.Button("Generate"))
			// {
			// 	
			// }
		}
	}
}
#endif