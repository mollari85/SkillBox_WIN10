using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace task_12_bankAccount.Model.Types
{
    abstract class Person
    {
        string _surname;
        public string Surname { get { return _surname; } set { if (_surname != value) { _surname = value; OnPropertyChanged(); } } }
        string _name;
        /// <summary>
        /// Person's name
        /// </summary>
        public string Name { get { return _name; } set { if (_name != value) { _name = value; OnPropertyChanged(); } } }
        string? _thirdName;
        public string? ThirdName { get { return _thirdName; } set { if (_thirdName != value) { _thirdName = value; OnPropertyChanged(); } } }

        public Person(string surname, string name, string? thirdName)
        {
            Surname = surname;
            Name = name;
            ThirdName = thirdName;

        }
        public Person() : this("Surname", "Name", "THirdName")
        { }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
