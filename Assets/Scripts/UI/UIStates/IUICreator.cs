using System;

namespace UI.UIStates
{
	public interface IUICreator<T> where T: UIBasePage
	{
		public T Create();
	}
}