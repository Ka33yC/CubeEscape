using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIStates.LevelPage
{
	public class OpenLevelButton : MonoBehaviour
	{
		[SerializeField] private Button button;
		[SerializeField] private TextMeshProUGUI text;

		public Button Button => button;
		public TextMeshProUGUI Text => text;
	}
}