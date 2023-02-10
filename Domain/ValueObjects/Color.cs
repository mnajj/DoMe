namespace DoMe.Domain.ValueObjects;

public record Color
{
	private Color() { }

	private Color(string code)
		=> Code = code;

	public static Color From(string code)
	{
		var color = new Color { Code = code };
		if (!SupportedColours.Contains(color))
		{
			throw new UnsupportedColourException(code);
		}

		return color;
	}
	
	protected static IEnumerable<Color> SupportedColours
	{
		get
		{
			yield return White;
			yield return Red;
			yield return Orange;
			yield return Yellow;
			yield return Green;
			yield return Blue;
			yield return Purple;
			yield return Grey;
		}
	}
	
	public static Color White => new("#FFFFFF");

	public static Color Red => new("#FF5733");

	public static Color Orange => new("#FFC300");

	public static Color Yellow => new("#FFFF66");

	public static Color Green => new("#CCFF99");

	public static Color Blue => new("#6666FF");

	public static Color Purple => new("#9966CC");

	public static Color Grey => new("#999999");

	public string Code { get; private set; }

	public static explicit operator Color(string code)
		=> From(code);
	
	public static implicit operator string(Color colour)
		=> colour.ToString();

	public override string ToString() => Code;
}