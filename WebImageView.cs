using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace SDWebImage
{
	[Register ("WebImageView")]
	public class WebImageView : SDWebImageManagerDelegate
	{
		public WebImageView (IntPtr handle) : base (handle)
		{
		}

        public WebImageView () : base ()
        {
        }

        public void Cancel()
        {
            var manager = SDWebImageManager.SharedManager;
            manager.CancelForDelegate (this);
        }

		public void SetImage (NSUrl url, SDWebImageOptions options)
		{
            Cancel ();
			
			if (url != null) {
                SDWebImageManager.SharedManager.Download (url, this, options);
			}
		}

		public override void DidFinishWithImage (SDWebImageManager imageManager, UIImage image)
		{
		}

		public override void DidFinishWithImage (SDWebImageManager imageManager, UIImage image, NSUrl url)
		{
		}

		public override void DidFinishWithImage (SDWebImageManager imageManager, UIImage image, NSUrl url, NSDictionary info)
		{
		}
	}
}

