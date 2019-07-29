# MVVMLight Issue In .NET Standard
Some issue I found when working with shared code in .NET Standard with MVVM for WPF and UWP

So after much tinkering of the codes. I found the the *ViewModelBase.IsInDesignMode* is not handling the WPF properly if it used in .NET Standard class library

# SOLUTION
I took the DesignerLibrary class form MVVMLight dll and added error trapping in GetCurrentPlatform() method. Something's crashing in Silverlight condition. Checkout hacked_designerlibrary_class branch for the solution
