using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace BugInMvvmForWPF.Lib.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<ViewModel_Main>();
        }

        public ViewModel_Main Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel_Main>();
            }
        }
    }
}
