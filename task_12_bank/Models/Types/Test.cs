using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace task_12_bank.Models.Types
{
    internal class Test:INotifyPropertyChanged,IDataErrorInfo
    {
        private int _age;
        public int Age { get { return _age; } set { _age = value; OnPropertyChanged(); } }
        private string _name;
        public string Name { get{return _name; } set { _name = value; OnPropertyChanged(); } }

        
        public Test()
        {
            Age = 19;
            Name = "Denis";        
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Error => throw new NotImplementedException();

        public string this[string MyName]
        {
            get {
                string error = String.Empty;
                switch (MyName)
                {
                    case "Name":
                        if (Name.Contains("Den"))
                            error = "it is a good name Den";
                        break;
                    case "Age":
                        if ((Age < 18) | (Age > 100))
                            error = $"it can't be true the Age={Age}";
                        break;
                        default:error = "Unknown property";
                        break;
                }

            return (error);
            }
        }

    }
}
