using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SkillBox_WIN10
{
    public class VM:INotifyPropertyChanged
    {
        private ModelClass Model;
        private RelayCommand commandReverse;
        public RelayCommand CommandReverse
        {
            get
            {
                return commandReverse ?? (commandReverse = new RelayCommand(obj =>
                {

                    MessageBox.Show("it is Test commnad Reverse message");
                }));

            }
            set { commandReverse = value; }
        }
       
        public RelayCommand commandSeparate;
        public RelayCommand CommandSeparate { get {
                return commandSeparate ?? (commandSeparate = new RelayCommand(obj =>
                                                                                {
                                                                                    
                                                                                    MessageBox.Show("it is Test commnad Split message");
                                                                                }));

            }
            set { commandSeparate = value; }
        }
        private string inputText="Enter text";
        /// <summary>
        /// text that Entered in MainTextBOx
        /// </summary>
        public string InputText { get { return inputText; } set { inputText = value; OnPropertyChanged(); } }
        private string labelText;
        /// <summary>
        /// text binded with Lablel.
        /// </summary>
        public string LabelText { get { return (labelText); } set { labelText = value; OnPropertyChanged(); } }

        public ObservableCollection<string> Collection { get; set; }

        public VM()
        {
            Model = new ModelClass();
            CommandSeparate = new RelayCommand(Separate);
            CommandReverse = new RelayCommand(Reverse);
            Collection = new ObservableCollection<string>();
            
            LabelText = "text any reserse   dd";
        }
        
        public VM(ModelClass Model)
        {
            this.Model = Model;
            CommandSeparate = new RelayCommand(Separate);
            CommandReverse = new RelayCommand(Reverse);
            LabelText = "text resersed";

        }
        
        public void Separate(object text)
        {
            Collection.Clear();
            if (InputText == null)
                return;
            string sTmp = InputText.Trim();
            if (sTmp == null)
                return;
            ObservableCollection<string> obscolTmp=new ObservableCollection<string>(sTmp.Split(' ').ToList<string>());
            foreach (var i in obscolTmp)
                Collection.Add(i);        
        }/// <summary>
         /// Make
         /// </summary>
         /// <param name="text"></param>
        private void Reverse(object text)
        {
            
            if (InputText == null)
                return;
            string sTmp = InputText.Trim();
            if (sTmp == null)
                return;
            string result = string.Join(" ", sTmp.Split(' ').Reverse().Select(x => new string(x.ToArray())));
            LabelText=result;           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }
}
