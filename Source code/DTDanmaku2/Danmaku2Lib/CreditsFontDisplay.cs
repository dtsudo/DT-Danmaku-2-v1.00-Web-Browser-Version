﻿
namespace Danmaku2Lib
{
	public class CreditsFontDisplay
	{
		public static void Render(
			int x,
			int y,
			IDisplay<Danmaku2Assets> display)
		{
			display.GetAssets().DrawImage(
				image: Danmaku2Image.CreditsFont,
				x: x + 5,
				y: y + 5);
		}
	}
}
