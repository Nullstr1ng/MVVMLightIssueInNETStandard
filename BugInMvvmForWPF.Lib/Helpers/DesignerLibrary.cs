namespace GalaSoft.MvvmLight.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class DesignerLibrary
    {
        internal enum DesignerPlatformLibrary
        {
            Unknown,
            Net,
            WinRt,
            Silverlight
        }

        private static DesignerPlatformLibrary? _detectedDesignerPlatformLibrary;

        private static bool? _isInDesignMode;

        internal static DesignerPlatformLibrary DetectedDesignerLibrary
        {
            get
            {
                if (!_detectedDesignerPlatformLibrary.HasValue)
                {
                    _detectedDesignerPlatformLibrary = GetCurrentPlatform();
                }
                return _detectedDesignerPlatformLibrary.Value;
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
                    _isInDesignMode = IsInDesignModePortable();
                }
                return _isInDesignMode.Value;
            }
        }

        private static DesignerPlatformLibrary GetCurrentPlatform()
        {
            DesignerPlatformLibrary ret = DesignerPlatformLibrary.Unknown;

            if (!(Type.GetType("System.ComponentModel.DesignerProperties, PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35") is null))
            {
                ret = DesignerPlatformLibrary.Net;
            }
            else if (!(Type.GetType("Windows.ApplicationModel.DesignMode, Windows, ContentType=WindowsRuntime") is null))
            {
                ret = DesignerPlatformLibrary.WinRt;
            }

            // for WOF
            // this prevents the designer to ignore design time datas
            try
            {
                if (!(Type.GetType("System.ComponentModel.DesignerProperties, System.Windows, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e") is null))
                {
                    ret = DesignerPlatformLibrary.Silverlight;
                }
            }
            catch { }

            return ret;
        }

        private static bool IsInDesignModePortable()
        {
            switch (DetectedDesignerLibrary)
            {
                case DesignerPlatformLibrary.WinRt:
                    return IsInDesignModeMetro();
                case DesignerPlatformLibrary.Silverlight:
                    {
                        bool desMode = IsInDesignModeSilverlight();
                        if (!desMode)
                        {
                            desMode = IsInDesignModeNet();
                        }
                        return desMode;
                    }
                case DesignerPlatformLibrary.Net:
                    return IsInDesignModeNet();
                default:
                    return false;
            }
        }

        private static bool IsInDesignModeSilverlight()
        {
            try
            {
                Type dm = Type.GetType("System.ComponentModel.DesignerProperties, System.Windows, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e");
                if ((object)dm == null)
                {
                    return false;
                }
                PropertyInfo dme = dm.GetTypeInfo().GetDeclaredProperty("IsInDesignTool");
                if ((object)dme == null)
                {
                    return false;
                }
                return (bool)dme.GetValue(null, null);
            }
            catch
            {
                return false;
            }
        }

        private static bool IsInDesignModeMetro()
        {
            try
            {
                return (bool)Type.GetType("Windows.ApplicationModel.DesignMode, Windows, ContentType=WindowsRuntime").GetTypeInfo().GetDeclaredProperty("DesignModeEnabled")
                    .GetValue(null, null);
            }
            catch
            {
                return false;
            }
        }

        private static bool IsInDesignModeNet()
        {
            try
            {
                Type dm = Type.GetType("System.ComponentModel.DesignerProperties, PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");

                if ((object)dm == null)
                {
                    return false;
                }

                object dmp = dm.GetTypeInfo().GetDeclaredField("IsInDesignModeProperty").GetValue(null);

                Type dpd = Type.GetType("System.ComponentModel.DependencyPropertyDescriptor, WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
                Type typeFe = Type.GetType("System.Windows.FrameworkElement, PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");

                if ((object)dpd == null || (object)typeFe == null)
                {
                    return false;
                }

                List<MethodInfo> fromPropertys = dpd.GetTypeInfo().GetDeclaredMethods("FromProperty").ToList();

                if (fromPropertys == null || fromPropertys.Count == 0)
                {
                    return false;
                }

                MethodInfo fromProperty = fromPropertys.FirstOrDefault(delegate (MethodInfo mi)
                {
                    if (mi.IsPublic && mi.IsStatic)
                    {
                        return mi.GetParameters().Length == 2;
                    }
                    return false;
                });

                if ((object)fromProperty == null)
                {
                    return false;
                }

                object descriptor = fromProperty.Invoke(null, new object[2]
                {
                    dmp,
                    typeFe
                });

                if (descriptor == null)
                {
                    return false;
                }

                PropertyInfo metaProp = dpd.GetTypeInfo().GetDeclaredProperty("Metadata");
                if ((object)metaProp == null)
                {
                    return false;
                }

                object metadata = metaProp.GetValue(descriptor, null);
                Type tPropMeta = Type.GetType("System.Windows.PropertyMetadata, WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");

                if (metadata == null || (object)tPropMeta == null)
                {
                    return false;
                }

                PropertyInfo dvProp = tPropMeta.GetTypeInfo().GetDeclaredProperty("DefaultValue");

                if ((object)dvProp == null)
                {
                    return false;
                }

                return (bool)dvProp.GetValue(metadata, null);
            }
            catch
            {
                return false;
            }
        }
    }
}