using System;

namespace SDWebImage
{
	public enum SDWebImageOptions
	{
		SDWebImageRetryFailed = 1 << 0,
		SDWebImageLowPriority = 1 << 1,
		SDWebImageCacheMemoryOnly = 1 << 2,
		SDWebImageProgressiveDownload = 1 << 3,
		SDWebImageRefreshCached = 1 << 4,
        None = 0
	}

    public enum SDImageCacheType
    {
        SDImageCacheTypeNone = 0,
        SDImageCacheTypeDisk,
        SDImageCacheTypeMemory
    }

	public enum SDWebImageDownloaderExecutionOrder
	{
		SDWebImageDownloaderFIFOExecutionOrder,
		SDWebImageDownloaderLIFOExecutionOrder
	}

	public enum SDWebImageDownloaderOptions
	{
		SDWebImageDownloaderLowPriority = 1 << 0,
		SDWebImageDownloaderProgressiveDownload = 1 << 1,
		SDWebImageDownloaderUseNSURLCache = 1 << 2,
		SDWebImageDownloaderIgnoreCachedResponse = 1 << 3
	}
}

