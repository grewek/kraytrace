using System;
using System.Drawing;
using System.Text;
using kraytrace.LinearAlgebra;

namespace kraytrace
{
	public class Image
	{

        private int _imageWidth;
		private int _imageHeight;
		private int _colorDepth = 255;

		private Vector3[] _pixelBuffer;

		public Image(int width, int height)
		{
			_imageWidth = width;
			_imageHeight = height;
			_pixelBuffer = new Vector3[width * height];
		}

		private int CalculatePixelIndex(int y, int x)
		{
			//TODO: Catch all index out of bounds errors here !
			return _imageWidth * y + x;
		}

		private Vector3 AveragePixelColor(Vector3 color, int samplesPerPixel)
		{
			//NOTE: Gamma correction applied as well
            var scale = 1f / samplesPerPixel;
			color[0] = (float)Math.Sqrt(color[0] * scale);
			color[1] = (float)Math.Sqrt(color[1] * scale);
			color[2] = (float)Math.Sqrt(color[2] * scale);

			return color;
        }

		public void SetPixel(int y, int x, Vector3 color, int samplesPerPixel)
		{
			if(_pixelBuffer == null)
			{
				return;
			}

			_pixelBuffer[CalculatePixelIndex(y, x)] = AveragePixelColor(color, samplesPerPixel);
		}

		private String GenerateColorString(Vector3 color)
		{
			StringBuilder builder = new StringBuilder();
            var rInt = (int)(256 * MathHelpers.ClampValue(color.X, 0f, 0.999f));
            var gInt = (int)(256 * MathHelpers.ClampValue(color.Y, 0f, 0.999f));
            var bInt = (int)(256 * MathHelpers.ClampValue(color.Z, 0f, 0.999f));
            //TODO: Do we need an alpha channel ?
            builder.AppendFormat("{0} {1} {2}", rInt, gInt, bInt);
            return builder.ToString();
        }

        public override string ToString()
        {
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat("P3\n{0} {1}\n{2}\n", _imageWidth, _imageHeight, _colorDepth);

			//NOTE: PPM Files are written top to bottom!

			for(int y = _imageHeight-1; y >= 0; y--)
			{
				for(int x = 0; x < _imageWidth; x++)
				{
					var pxCol = GenerateColorString(_pixelBuffer[CalculatePixelIndex(y, x)]);
					builder.AppendFormat("{0}\n", pxCol);
				}
			}

			return builder.ToString();
        }
    }
}

