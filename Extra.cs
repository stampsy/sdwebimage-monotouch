using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SDWebImage
{
    public partial class SDImageCache : NSObject
    {
    }

    public partial class SDWebImageManager : NSObject
    {
        public void Download (string url, NSObject del, SDWebImageOptions options)
        {
            if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) return;
            Download (NSUrl.FromString (url), del, options);
        }
        
        public void Download (string url, NSObject del, SDWebImageOptions options, SDWebImageSuccessBlock success, SDWebImageFailureBlock failure)
        {
            if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) return;
            Download (NSUrl.FromString (url), del, options, success, failure);
        }
    }

    public partial class SDWebImageManagerDelegate : UIView
    {
    }

    public static class Extensions
    {
        public static void SetImage (this UIImageView view, NSUrl url)
        {
            SDWebImageManager.SharedManager.SetImage (view, url);
        }
        
        public static void SetImage (this UIImageView view, string url)
        {
            if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) return;
            SDWebImageManager.SharedManager.SetImage (view, NSUrl.FromString (url));
        }
        
        public static void SetImage (this UIButton button, NSUrl url)
        {
            SDWebImageManager.SharedManager.SetImage (button, url);
        }
        
        public static void SetImage (this UIButton button, string url)
        {
            if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) return;
            SDWebImageManager.SharedManager.SetImage (button, NSUrl.FromString (url));
        }
        
        public static void SetBackgroundImage (this UIButton button, NSUrl url)
        {
            SDWebImageManager.SharedManager.SetBackgroundImage (button, url);
        }
        
        public static void SetBackgroundImage (this UIButton button, string url)
        {
            if (!Uri.IsWellFormedUriString (url, UriKind.Absolute)) return;
            SDWebImageManager.SharedManager.SetBackgroundImage (button, NSUrl.FromString (url));
        }
    }
}

