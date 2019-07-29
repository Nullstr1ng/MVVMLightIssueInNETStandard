using GalaSoft.MvvmLight;

namespace BugInMvvmForWPF.Lib.Models
{
    public class Model_Person : ViewModelBase
    {
        private string _FirstName = string.Empty;
        public string FirstName
        {
            get { return _FirstName; }
            set { Set(nameof(FirstName), ref _FirstName, value); }
        }

        private string _LastName = string.Empty;
        public string LastName
        {
            get { return _LastName; }
            set { Set(nameof(LastName), ref _LastName, value); }
        }
    }
}
