using System;
using System.Drawing;

using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SDWebImage
{
	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface SDWebImageManagerDelegate
 	{
		[Export ("imageManager:shouldDownloadImageForURL:")]
		void ShouldDownloadImage (SDWebImageManager imageManager, NSUrl url);
		
		[Export ("imageManager:transformDownloadedImage:withURL:")]
		UIImage TransformDownloadedImage (SDWebImageManager imageManager, UIImage image, NSUrl url);
	}

	public delegate void SDWebImageCompletedBlock (UIImage image, NSError error, SDImageCacheType cacheType);
	public delegate void SDWebImageCompletedWithFinishedBlock (UIImage image, NSError error, SDImageCacheType cacheType, bool finished);

	public delegate void SDWebImageDownloaderProgressBlock (uint receivedSize, long expectedSize);
	public delegate void SDWebImageDownloaderCompletedBlock (UIImage image, NSData data, NSError error, bool finished);

	public delegate string CacheKeyFilterBlock (NSUrl url);
	public delegate void CalculateSizeCompletionBlock (uint fileCount, ulong totalSize);

	[BaseType (typeof (NSObject))]
	interface SDWebImageDownloader
	{
		[Export ("maxConcurrentDownloads", ArgumentSemantic.Assign)]
		int MaxConcurrentDownloads { get; set; }

		[Export ("executionOrder", ArgumentSemantic.Assign)]
		SDWebImageDownloaderExecutionOrder ExecutionOrder { get; set; }

		[Static, Export ("sharedDownloader")]
		SDWebImageDownloader SharedDownloader { get; }

		[Export ("setValue:forHTTPHeaderField:")]
		void SetHTTPHeaderValue (string value, string field);
		[Export ("valueForHTTPHeaderField:")]
		string GetHTTPHeaderValue (string field);
		[Export ("downloadImageWithURL:options:progress:completed:")]
		void DownloadImage (NSUrl url, SDWebImageDownloaderOptions options, [NullAllowed] SDWebImageDownloaderProgressBlock progress, [NullAllowed] SDWebImageDownloaderCompletedBlock completed);
	}

	[BaseType (typeof (NSObject))]
	[Protocol]
	interface SDWebImageOperation
	{
		[Export ("cancel")]
		void Cancel ();
	}

	[BaseType (typeof (NSObject))]
	interface SDWebImageManager
	{
		[Wrap ("WeakDelegate")]
		SDWebImageManagerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)]
		NSObject WeakDelegate { get; set; }

		[Static, Export ("sharedManager")]
		SDWebImageManager SharedManager { get; }

		[Export ("cancelAll")]
		void CancelAll ();

		[Export ("isRunning")]
		bool IsRunning ();

		[Export ("diskImageExistsForURL:")]
		bool DiskImageExists (NSUrl url);

		[Export ("cacheKeyFilter")]
		Func<NSUrl, string> CacheKeyFilter { get; }

		[Export ("imageCache")]
		SDImageCache ImageCache { get; }

		[Export ("imageDownloader")]
		SDWebImageDownloader ImageDownloader { get; }

        [Export ("downloadWithURL:options:progress:completed:")]
		SDWebImageOperation Download (NSUrl url, SDWebImageOptions options, [NullAllowed] SDWebImageDownloaderProgressBlock progress, SDWebImageCompletedWithFinishedBlock completed);

		#region UIImageView

		[Bind ("setImageWithURL:")]
		void SetImage ([Target] UIImageView view, NSUrl url);

		[Bind ("setImageWithURL:placeholderImage:")]
		void SetImage ([Target] UIImageView view, NSUrl url, [NullAllowed] UIImage placeholder);

		[Bind ("setImageWithURL:placeholderImage:options:")]
		void SetImage ([Target] UIImageView view, NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options);

		[Bind ("setImageWithURL:completed:")]
		void SetImage ([Target] UIImageView view, NSUrl url, SDWebImageCompletedBlock completedBlock);

		[Bind ("setImageWithURL:placeholderImage:completed:")]
		void SetImage ([Target] UIImageView view, NSUrl url, [NullAllowed] UIImage placeholder, [NullAllowed] SDWebImageCompletedBlock completedBlock);
		
		[Bind ("setImageWithURL:placeholderImage:options:completed:")]
		void SetImage ([Target] UIImageView view, NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] SDWebImageCompletedBlock completedBlock);

		[Bind ("setImageWithURL:placeholderImage:options:progress:completed:")]
		void SetImage ([Target] UIImageView view, NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] SDWebImageDownloaderProgressBlock progress, [NullAllowed] SDWebImageCompletedBlock completedBlock);

		[Bind ("setAnimationImagesWithURLs:")]
		void SetAnimationImages ([Target] UIImageView view, NSUrl[] urls);

		[Bind ("cancelCurrentImageLoad")]
		void CancelCurrentImageLoad ([Target] UIImageView view);

		[Bind ("cancelCurrentArrayLoad")]
		void CancelCurrentArrayLoad ([Target] UIImageView view);

		#endregion

		#region UIButton

		[Bind ("setImageWithURL:forState:")]
		void SetImage ([Target] UIButton view, NSUrl url, UIControlState state);

		[Bind ("setImageWithURL:forState:placeholderImage:")]
		void SetImage ([Target] UIButton view, NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder);

		[Bind ("setImageWithURL:forState:placeholderImage:options:")]
		void SetImage ([Target] UIButton view, NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, SDWebImageOptions options);

		[Bind ("setImageWithURL:forState:completed:")]
		void SetImage ([Target] UIButton view, NSUrl url, UIControlState state, [NullAllowed] SDWebImageCompletedBlock completedBlock);

		[Bind ("setImageWithURL:forState:placeholderImage:completed:")]
		void SetImage ([Target] UIButton view, NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, [NullAllowed] SDWebImageCompletedBlock completedBlock);

		[Bind ("setImageWithURL:forState:placeholderImage:options:completed:")]
		void SetImage ([Target] UIButton view, NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] SDWebImageCompletedBlock completedBlock);

		[Bind ("setBackgroundImageWithURL:forState:")]
		void SetBackgroundImage ([Target] UIButton view, NSUrl url, UIControlState state);

		[Bind ("setBackgroundImageWithURL:forState:placeholderImage:")]
		void SetBackgroundImage ([Target] UIButton view, NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder);

		[Bind ("setBackgroundImageWithURL:forState:placeholderImage:options:")]
		void SetBackgroundImage ([Target] UIButton view, NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, SDWebImageOptions options);

		[Bind ("setBackgroundImageWithURL:forState:completed:")]
		void SetBackgroundImage ([Target] UIButton view, NSUrl url, UIControlState state, [NullAllowed] SDWebImageCompletedBlock completedBlock);

		[Bind ("setBackgroundImageWithURL:forState:placeholderImage:completed:")]
		void SetBackgroundImage ([Target] UIButton view, NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, [NullAllowed] SDWebImageCompletedBlock completedBlock);

		[Bind ("setBackgroundImageWithURL:forState:placeholderImage:options:completed:")]
		void SetBackgroundImage ([Target] UIButton view, NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] SDWebImageCompletedBlock completedBlock);

		[Bind ("cancelCurrentImageLoad")]
		void CancelCurrentImageLoad ([Target] UIButton view);

		#endregion
    }

	[BaseType (typeof (NSObject))]
	interface SDImageCache
	{
        [Static, Export ("sharedImageCache")]
        SDImageCache SharedImageCache { get; }

		[Export ("maxCacheAge", ArgumentSemantic.Assign)]
		int MaxCacheAge { get; set; }

		[Export ("maxCacheSize", ArgumentSemantic.Assign)]
		ulong MaxCacheSize { get; set; }

		[Export ("initWithNamespace:")]
		IntPtr Constructor (string ns);

		[Export ("addReadOnlyCachePath:")]
		void AddReadOnlyCachePath (string path);

        [Export ("storeImage:forKey:")]
        void StoreImage (UIImage image, string key);

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
		ulong GetSize ();

		[Export ("getDiskCount")]
		int GetDiskCount ();

		[Export ("calculateSizeWithCompletionBlock:")]
		void CalculateSize (CalculateSizeCompletionBlock completionBlock);

		[Export ("diskImageExistsWithKey:")]
		bool DiskImageExists (string key);

		[Export ("setValue:forKey:")]
		void SetValueForKey ([NullAllowed] NSObject value, NSString key);

		#region private methods

        [Export ("defaultCachePathForKey:"), Advice ("This is a private method so be careful!")]
        string GetDefaultCachePath (string key);

		#endregion
    }
}
