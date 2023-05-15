using System.Drawing;
using System.Drawing.Imaging;

namespace SpacedInvaders
{
	abstract class TokenGeneral
	{
		//Global Variables
		internal PointF location;
		internal Rectangle bounds;
	
		protected ImageAttributes  imgAttr = new ImageAttributes();
		protected Image sprite;

		//Global Methods
		internal abstract void Step (double elapsed);
		internal abstract void Render (Graphics g);
	}
}
