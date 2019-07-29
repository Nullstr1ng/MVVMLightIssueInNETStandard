using BugInMvvmForWPF.Lib.Models;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BugInMvvmForWPF.Lib.ViewModels
{
    public class ViewModel_Main : INotifyPropertyChanged
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

            // used only in UWP & WPF
            // or anything that supports design time updates
            //if (base.IsInDesignMode)
            //{
            //    DesignData();
            //}
            //else
            //{
            //    RuntimeData();
            //} 

            // temporarily use DESIGN DATA for Designer and Runtime
            DesignData();
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

            this.PersonCollection.Add(new Model_Person()
            {
                FirstName = "DESIGN_Emelda",
                LastName = "Crosma"
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

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
