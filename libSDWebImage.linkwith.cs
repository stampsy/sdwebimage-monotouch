using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libSDWebImage.a", LinkTarget.Simulator | LinkTarget.ArmV7, ForceLoad = true, Frameworks = "UIKit CoreGraphics Foundation ImageIO")]
