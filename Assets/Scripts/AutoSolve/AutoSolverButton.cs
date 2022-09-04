#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace AutoSolve
{
	[CustomEditor(typeof(AutoSolver))]
	public class AutoSolverButton : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			AutoSolver autoSolver = (AutoSolver)target;

			if (!GUILayout.Button("Solve")) return;

			autoSolver.Solve();
		}
	}
}

#endif