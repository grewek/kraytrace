using System;
using System.Text;

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

		public void SetPixel(int y, int x, Vector3 color)
		{
			if(_pixelBuffer == null)
			{
				return;
			}

			_pixelBuffer[CalculatePixelIndex(y, x)] = color;
		}

		private String GenerateColorString(Vector3 color)
		{
            StringBuilder builder = new StringBuilder();
            var rInt = (int)(color[0] * 255.9);
            var gInt = (int)(color[1] * 255.9);
            var bInt = (int)(color[2] * 255.9);
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

