﻿
namespace Danmaku2Lib
{
	public class MusicVolumePicker
	{
		private int _xPos;
		private int _yPos;

		private int _currentVolume;
		private int _unmuteVolume;

		private bool _isDraggingVolumeSlider;
		
		public MusicVolumePicker(int xPos, int yPos, int initialVolume)
		{
			this._xPos = xPos;
			this._yPos = yPos;

			this._currentVolume = initialVolume;
			this._unmuteVolume = this._currentVolume;

			this._isDraggingVolumeSlider = false;
		}

		public void ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput)
		{
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			if (mouseInput.IsLeftMouseButtonPressed()
				&& !previousMouseInput.IsLeftMouseButtonPressed()
				&& this._xPos <= mouseX
				&& mouseX <= this._xPos + 40
				&& this._yPos <= mouseY
				&& mouseY <= this._yPos + 50)
			{
				if (this._currentVolume == 0)
				{
					this._currentVolume = this._unmuteVolume == 0 ? GlobalState.DEFAULT_VOLUME : this._unmuteVolume;
					this._unmuteVolume = this._currentVolume;
				}
				else
				{
					this._unmuteVolume = this._currentVolume;
					this._currentVolume = 0;
				}
			}

			if (mouseInput.IsLeftMouseButtonPressed()
				&& !previousMouseInput.IsLeftMouseButtonPressed()
				&& this._xPos + 50 <= mouseX
				&& mouseX <= this._xPos + 150
				&& this._yPos + 10 <= mouseY
				&& mouseY <= this._yPos + 40)
			{
				this._isDraggingVolumeSlider = true;
			}

			if (this._isDraggingVolumeSlider && mouseInput.IsLeftMouseButtonPressed())
			{
				int volume = mouseX - (this._xPos + 50);
				if (volume < 0)
					volume = 0;
				if (volume > 100)
					volume = 100;

				this._currentVolume = volume;
				this._unmuteVolume = this._currentVolume;
			}

			if (!mouseInput.IsLeftMouseButtonPressed())
				this._isDraggingVolumeSlider = false;
		}

		/// <summary>
		/// Returns a number from 0 to 100 (both inclusive)
		/// </summary>
		public int GetCurrentMusicVolume()
		{
			return this._currentVolume;
		}

		public void Render(IDisplay<Danmaku2Assets> display)
		{
			if (this._currentVolume > 0)
				display.GetAssets().DrawImage(Danmaku2Image.MusicOn, this._xPos, this._yPos);
			else
				display.GetAssets().DrawImage(Danmaku2Image.MusicOff, this._xPos, this._yPos);

			display.DrawRectangle(
				x: this._xPos + 50,
				y: this._yPos + 10,
				width: 101,
				height: 31,
				color: new DTColor(r: 255, g: 255, b: 255),
				fill: false);

			if (this._currentVolume > 0)
				display.DrawRectangle(
					x: this._xPos + 50,
					y: this._yPos + 10,
					width: this._currentVolume,
					height: 31,
					color: new DTColor(r: 255, g: 255, b: 255),
					fill: true);
		}
	}
}
