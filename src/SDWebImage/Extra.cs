using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading.Tasks;
using System.Threading;

namespace SDWebImage
{
    public partial class SDImageCache : NSObject
    {
    }

    public partial class SDWebImageManager : NSObject
    {
		public void Download (string url, SDWebImageOptions options, SDWebImageDownloaderProgressBlock progress, SDWebImageCompletedWithFinishedBlock completed)
        {
            if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) return;
            Download (NSUrl.FromString (url), options, progress, completed);
        }
    }

    public partial class SDWebImageManagerDelegate : UIView
    {
    }

	public class ImageDownloadResult
	{
		public ImageDownloadResult (UIImage image, SDImageCacheType cacheType)
		{
			Image = image;
			CacheType = cacheType;
		}
		public UIImage Image { get; private set; }
		public SDImageCacheType CacheType { get; private set; }
	}

    public static class Extensions
    {
		#region SDWebImageManager

		public static Task<ImageDownloadResult> DownloadAsync (this SDWebImageManager manager, NSUrl url, SDWebImageOptions options = SDWebImageOptions.None, SDWebImageDownloaderProgressBlock progress = null, CancellationToken token = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<ImageDownloadResult> ();

			SDWebImageOperation operation = null;

			operation = manager.Download (url, options, progress, (image, error, cacheType, finished) => {
				if (token.IsCancellationRequested) {
					operation.Cancel ();
					tcs.TrySetCanceled ();
					return;
				}
				if (finished) {
					if (image == null) {
						tcs.TrySetException (new NSErrorException (error));
						return;
					}
					tcs.TrySetResult (new ImageDownloadResult (image, cacheType));
				}
			});

			return tcs.Task;
		}

		public static Task<ImageDownloadResult> DownloadAsync (this SDWebImageManager manager, string url, SDWebImageOptions options = SDWebImageOptions.None, SDWebImageDownloaderProgressBlock progress = null, CancellationToken token = default(CancellationToken))
		{
			if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) throw new Exception (String.Format ("Malformed url: {0}", url));

			return DownloadAsync (manager, NSUrl.FromString (url), options, progress, token);
		}

		#endregion

		#region UIImageView

		public static void SetImage (this UIImageView view, NSUrl url, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None, SDWebImageDownloaderProgressBlock progress = null, SDWebImageCompletedBlock completed = null)
        {
            SDWebImageManager.SharedManager.SetImage (view, url, placeholder, options, progress, completed);
        }
        
		public static void SetImage (this UIImageView view, string url, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None, SDWebImageDownloaderProgressBlock progress = null, SDWebImageCompletedBlock completed = null)
        {
            if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) return;
			SDWebImageManager.SharedManager.SetImage (view, NSUrl.FromString (url), placeholder, options, progress, completed);
        }

		public static Task<ImageDownloadResult> SetImageAsync (this UIImageView view, NSUrl url, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None, SDWebImageDownloaderProgressBlock progress = null)
		{
			var tcs = new TaskCompletionSource<ImageDownloadResult> ();

			SDWebImageManager.SharedManager.SetImage (view, url, placeholder, options, progress, (image, error, cacheType) => {
				if (image == null) {
					tcs.SetException (new NSErrorException (error));
					return;
				}
				tcs.SetResult (new ImageDownloadResult (image, cacheType));
			});

			return tcs.Task;
		}

		public static Task<ImageDownloadResult> SetImageAsync (this UIImageView view, string url, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None, SDWebImageDownloaderProgressBlock progress = null)
		{
			if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) throw new Exception (String.Format ("Malformed url: {0}", url));

			return SetImageAsync (view, NSUrl.FromString (url), placeholder, options, progress);
		}

		public static void SetAnimationImages (this UIImageView view, NSUrl[] urls)
		{
			SDWebImageManager.SharedManager.SetAnimationImages (view, urls);
		}

		public static void CancelCurrentImageLoad (this UIImageView view)
		{
			SDWebImageManager.SharedManager.CancelCurrentImageLoad (view);
		}

		public static void CancelCurrentArrayLoad (this UIImageView view)
		{
			SDWebImageManager.SharedManager.CancelCurrentArrayLoad (view);
		}

		#endregion

		#region UIButton

		public static void SetImage (this UIButton button, NSUrl url, UIControlState state, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None, SDWebImageCompletedBlock completed = null)
        {
            SDWebImageManager.SharedManager.SetImage (button, url, state, placeholder, options, completed);
        }
        
		public static void SetImage (this UIButton button, string url, UIControlState state, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None, SDWebImageCompletedBlock completed = null)
        {
            if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) return;
			SDWebImageManager.SharedManager.SetImage (button, NSUrl.FromString (url), state, placeholder, options, completed);
        }

		public static Task<ImageDownloadResult> SetImageAsync (this UIButton button, NSUrl url, UIControlState state, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None)
		{
			var tcs = new TaskCompletionSource<ImageDownloadResult> ();

			SDWebImageManager.SharedManager.SetImage (button, url, state, placeholder, options, (image, error, cacheType) => {
				if (image == null) {
					tcs.SetException (new NSErrorException (error));
					return;
				}
				tcs.SetResult (new ImageDownloadResult (image, cacheType));
			});

			return tcs.Task;
		}

		public static Task<ImageDownloadResult> SetImageAsync (this UIButton button, string url, UIControlState state, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None)
		{
			if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) throw new Exception (String.Format ("Malformed url: {0}", url));

			return SetImageAsync (button, NSUrl.FromString (url), state, placeholder, options);
		}

   		public static void SetBackgroundImage (this UIButton button, NSUrl url, UIControlState state, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None, SDWebImageCompletedBlock completed = null)
        {
			SDWebImageManager.SharedManager.SetBackgroundImage (button, url, state, placeholder, options, completed);
        }
        
		public static void SetBackgroundImage (this UIButton button, string url, UIControlState state, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None, SDWebImageCompletedBlock completed = null)
        {
            if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) return;
			SDWebImageManager.SharedManager.SetBackgroundImage (button, NSUrl.FromString (url), state, placeholder, options, completed);
        }

		public static Task<ImageDownloadResult> SetBackgroundImageAsync (this UIButton button, NSUrl url, UIControlState state, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None)
		{
			var tcs = new TaskCompletionSource<ImageDownloadResult> ();

			SDWebImageManager.SharedManager.SetBackgroundImage (button, url, state, placeholder, options, (image, error, cacheType) => {
				if (image == null) {
					tcs.SetException (new NSErrorException (error));
					return;
				}
				tcs.SetResult (new ImageDownloadResult (image, cacheType));
			});

			return tcs.Task;
		}

		public static Task<ImageDownloadResult> SetBackgroundImageAsync (this UIButton button, string url, UIControlState state, UIImage placeholder = null, SDWebImageOptions options = SDWebImageOptions.None)
		{
			if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) throw new Exception (String.Format ("Malformed url: {0}", url));

			return SetImageAsync (button, NSUrl.FromString (url), state, placeholder, options);
		}

		public static void CancelCurrentImageLoad (this UIButton button)
		{
			SDWebImageManager.SharedManager.CancelCurrentImageLoad (button);
		}


		#endregion
    }
}

