using BugInMvvmForWPF.Lib.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BugInMvvmForWPF.Lib.ViewModels
{
    public class ViewModel_Main : ViewModelBase
    {
        #region events

        #endregion

        #region vars

        #endregion

        #region properties
        public ObservableCollection<Model_Person> PersonCollection { get; set; } = new ObservableCollection<Model_Person>();
        #endregion

        #region commands
        public ICommand Command_Add { get; set; }
        // move this command methods
        #endregion

        #region ctors
        public ViewModel_Main()
        {
            InitCommands();

#if WPF
            Debug.WriteLine("WPF!");
#endif

            // used only in UWP & WPF
            // or anything that supports design time updates
            if (DesignerLibrary.IsInDesignMode)
            {
                DesignData();
            }
            else
            {
                RuntimeData();
            }
        }
#endregion

#region command methods
        void Command_Add_Click()
        {
            this.PersonCollection.Add(new Model_Person()
            {
                FirstName = "Person",
                LastName = "Person's lastname"
            });
        }
#endregion

#region methods
        void InitCommands()
        {
            if (Command_Add == null) Command_Add = new RelayCommand(Command_Add_Click);
        }

        void DesignData()
        {
            this.PersonCollection.Add(new Model_Person()
            {
                FirstName = "DESIGN_Rodolfo",
                LastName = "Presbitero"
            });

            this.PersonCollection.Add(new Model_Person()
            {
                FirstName = "DESIGN_Ralph",
                LastName = "Guiterrez"
            });

            this.PersonCollection.Add(new Model_Person()
            {
                FirstName = "DESIGN_Cy",
                LastName = "Loleng"
            });
        }

        void RuntimeData()
        {
            this.PersonCollection.Add(new Model_Person()
            {
                FirstName = "Rodolfo",
                LastName = "Presbitero"
            });

            this.PersonCollection.Add(new Model_Person()
            {
                FirstName = "Ralph",
                LastName = "Guiterrez"
            });

            this.PersonCollection.Add(new Model_Person()
            {
                FirstName = "Cy",
                LastName = "Loleng"
            });
        }

        public async Task RefreshData()
        {

        }

        #endregion
    }
}
