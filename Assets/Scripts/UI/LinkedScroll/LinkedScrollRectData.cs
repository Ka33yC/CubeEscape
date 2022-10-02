using UnityEngine;
using UnityEngine.UI;

namespace UI.LinkedScroll
{
	public class LinkedScrollRectData : MonoBehaviour
	{
		[SerializeField] private ScrollRect linkedScrollRect;
		[SerializeField] private float minMagnitudeToDrag = 10;

		public ScrollRect LinkedScrollRect => linkedScrollRect;
		
		public float MinMagnitudeToDrag => minMagnitudeToDrag;
	}
}