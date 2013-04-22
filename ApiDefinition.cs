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

        [Bind ("setImageWithURL:placeholderImage:")]
        void SetImage ([Target] UIImageView view, NSUrl url, [NullAllowed] UIImage placeholder);

        [Bind ("setImageWithURL:")]
        void SetImage ([Target] UIButton view, NSUrl url);

        [Bind ("setImageWithURL:placeholderImage:")]
        void SetImage ([Target] UIButton view, NSUrl url, [NullAllowed] UIImage placeholder);

        [Bind ("setBackgroundImageWithURL:")]
        void SetBackgroundImage ([Target] UIButton view, NSUrl url);
        
        [Export ("downloadWithURL:delegate:")]
        void Download (NSUrl url, NSObject del);

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

        [Export ("imageFromKey:fromDisk:")]
        UIImage GetImage (string key, bool fromDisk);

        [Export ("cachePathForKey:"), Advice ("This is a private method so be careful!")]
        string GetCachePath (string key);

        [Export ("initWithNamespace:")]
        IntPtr Constructor (string ns);

        [Export ("storeImage:forKey:toDisk:")]
        void StoreImage (UIImage image, string key, bool toDisk);

        [Export ("storeImage:imageData:forKey:toDisk:")]
        void StoreImage (UIImage image, NSData data, string key, bool toDisk);

        [Export ("queryDiskCacheForKey:done:")]
        void QueryDiskCache (string key, Action<UIImage, SDImageCacheType> done);

        [Export ("imageFromMemoryCacheForKey:")]
        UIImage ImageFromMemoryCache (string key);

        [Export ("imageFromDiskCacheForKey:")]
        UIImage ImageFromDiskCache (string key);

        [Export ("removeImageForKey:")]
        void RemoveImage (string key);

        [Export ("removeImageForKey:fromDisk:")]
        void RemoveImage (string key, bool fromDisk);

        [Export ("clearMemory")]
        void ClearMemory ();

        [Export ("clearDisk")]
        void ClearDisk ();

        [Export ("cleanDisk")]
        void CleanDisk ();

        [Export ("getSize")]
        int GetSize ();

        [Export ("getDiskCount")]
        int GetDiskCount ();
    }
}
