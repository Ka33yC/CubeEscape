#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace LevelGeneration.EditorButtons
{
	[CustomEditor(typeof(DirectedFigureForLevelBuilder))]
	public class DirectedFigureForLevelBuilderEditorButtons : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			DirectedFigureForLevelBuilder directedFigureForLevelBuilder = (DirectedFigureForLevelBuilder)target;

			if (GUILayout.Button("Reverse Exist State"))
			{
				directedFigureForLevelBuilder.ReverseExistState();
			}
		}
	}
}
#endif