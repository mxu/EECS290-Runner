using UnityEditor;
using UnityEngine;

public class ColorManager
{
	public static Color getRandomColor() {
		float avg = Random.Range(200f, 240f),
		sum = avg * 3f,
		r = 0f,
		g = 0f,
		b = 0f;
		while(sum > 0f) {
			float x = Random.Range(0f, Mathf.Min(255f - r, sum));
			r += x;
			sum -= x;
			x = Random.Range(0f, Mathf.Min(255f - g, sum));
			g += x;
			sum -= x;
			x = Random.Range(0f, Mathf.Min(255f - b, sum));
			b += x;
			sum -= x;
		}
		return new Color(r / 255f, g / 255f, b / 255f);
	}

	public static Color offsetColor(Color c, float offset) {
		float avg = (c.r + c.g + c.b) / 3f;
		float newAvg = avg + (offset / 255f);
		float ratio = newAvg / avg;
		return new Color(c.r * ratio, c.g * ratio, c.b * ratio);
	}

	public static Color offsetHue(Color c, float deg) {
		float h = 0,
		s = 0,
		v = 0;
		EditorGUIUtility.RGBToHSV(c, out h, out s, out v);
		h = (h + (deg / 360f)) % 1f;
		return EditorGUIUtility.HSVToRGB(h, s, v);
	}
}

