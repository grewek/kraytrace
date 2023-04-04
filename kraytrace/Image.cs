using System;
using System.Text;

namespace kraytrace
{
	public class Image
	{
        private struct PpmColor
        {
			private float _red;
			private float _green;
			private float _blue;

			public PpmColor(float r, float g, float b)
			{
				_red = r;
				_green = g;
				_blue = b;
			}

			public override string ToString()
			{
				StringBuilder builder = new StringBuilder();
				var rInt = (int)(_red * 255.9);
				var gInt = (int)(_green * 255.9);
				var bInt = (int)(_blue * 255.9);
				//TODO: Do we need an alpha channel ?
				builder.AppendFormat("{0} {1} {2}", rInt, gInt, bInt);
				return builder.ToString();
			}
        }

        private int _imageWidth;
		private int _imageHeight;
		private int _colorDepth = 255;

		private PpmColor[] _pixelBuffer;

		public Image(int width, int height)
		{
			_imageWidth = width;
			_imageHeight = height;
			_pixelBuffer = new PpmColor[width * height];
		}

		public void SetPixel(int y, int x, float r, float g, float b)
		{
			var targetIndex = _imageWidth * y + x;

			if(_pixelBuffer == null)
			{
				return;
			}

			if (targetIndex < _pixelBuffer.Length)
			{
				_pixelBuffer[targetIndex] = new PpmColor(r, g, b);
			}
		}

        public override string ToString()
        {
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat("P3\n{0} {1}\n{2}\n", _imageWidth, _imageHeight, _colorDepth);

			//NOTE: PPM Files are written top to bottom!
			for(int y = _imageWidth-1; y >= 0; y--)
			{
				for(int x = 0; x < _imageHeight; x++)
				{
					builder.AppendFormat("{0}\n", _pixelBuffer[_imageWidth * y + x]);
				}
			}

			return builder.ToString();
        }
    }
}

