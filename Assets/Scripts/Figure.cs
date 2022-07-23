using System.Collections.Generic;

public abstract class Figure
{
	protected FiguresParent _parent;
	
	public Figure(FiguresParent parent)
	{
		_parent = parent;
	}

	public abstract void SetRandomDirection();
	public abstract List<Figure> GetFiguresOnDirection();
}
