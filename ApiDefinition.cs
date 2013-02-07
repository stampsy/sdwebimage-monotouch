using System;
using System.Drawing;

using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SDWebImage
{
	[BaseType (typeof (UIView))]
	[Model]
	interface SDWebImageManagerDelegate
 	{
		[Export ("webImageManager:didProgressWithPartialImage:forURL:userInfo:")]
		void DidProgressWithPartialImage (SDWebImageManager imageManager, UIImage image, NSUrl url, NSDictionary info);
		
		[Export ("webImageManager:didProgressWithPartialImage:forURL:")]
		void DidProgressWithPartialImage (SDWebImageManager imageManager, UIImage image, NSUrl url);
		
		[Export ("webImageManager:didFinishWithImage:forURL:userInfo:")]
		void DidFinishWithImage (SDWebImageManager imageManager, UIImage image, NSUrl url, NSDictionary info);
		
		[Export ("webImageManager:didFinishWithImage:forURL:")]
		void DidFinishWithImage (SDWebImageManager imageManager, UIImage image, NSUrl url);
		
		[Export ("webImageManager:didFinishWithImage:")]
		void DidFinishWithImage (SDWebImageManager imageManager, UIImage image);
		
		[Export ("webImageManager:didFailWithError:forURL:userInfo:")]
		void DidFailWithError (SDWebImageManager imageManager, NSError error, NSUrl url, NSDictionary info);
		
		[Export ("webImageManager:didFailWithError:forURL:")]
		void DidFailWithError (SDWebImageManager imageManager, NSError error, NSUrl url);
		
		[Export ("webImageManager:didFailWithError:")]
		void DidFailWithError (SDWebImageManager imageManager, NSError error);
	}

    public delegate void SDWebImageSuccessBlock (UIImage image, bool cached);
    public delegate void SDWebImageFailureBlock (NSError error);

    [BaseType (typeof (NSObject))]
	interface SDWebImageManager
	{
		[Static, Export ("sharedManager")]
		SDWebImageManager SharedManager { get; }

		[Export ("cancelForDelegate:")]
		void CancelForDelegate (NSObject del);

		[Bind ("setImageWithURL:")]
		void SetImage ([Target] UIImageView view, NSUrl url);

        [Bind ("setImageWithURL:")]
        void SetImage ([Target] UIButton view, NSUrl url);

        [Bind ("setBackgroundImageWithURL:")]
        void SetBackgroundImage ([Target] UIButton view, NSUrl url);
        
        [Export ("downloadWithURL:delegate:options:")]
		void Download (NSUrl url, NSObject del, SDWebImageOptions options);

        [Export ("downloadWithURL:delegate:options:success:failure:")]
        void Download (NSUrl url, [NullAllowed]NSObject del, SDWebImageOptions options, SDWebImageSuccessBlock success, SDWebImageFailureBlock failure);
    }

	[BaseType (typeof (NSObject))]
	interface SDImageCache
	{
        [Static, Export ("sharedImageCache")]
        SDImageCache SharedImageCache { get; }

		[Static, Export ("setMaxCacheAge:")]
		void SetMaxCacheAge (int age);

        [Export ("storeImage:forKey:")]
        void StoreImage (UIImage image, string key);
	}
}
