# MVVMLightIssueInNETStandard
Some issue I found when working with shared code in .NET Standard with MVVM for WPF and UWP

So after much tinkering of the codes. I found the the ViewModelBase.IsInDesignMode is not handling the WPF propertly if it used in .NET Standard class library
