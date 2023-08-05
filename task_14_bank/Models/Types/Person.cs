using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace task_14_bank.Models.Types
{
    [Serializable]
    public abstract class Person : INotifyPropertyChanged
    {
        public Guid PersonID { get; }
        string _surname;
        /// <summary>
        /// Person's Surname
        /// </summary>
        public string Surname { get { return _surname; } set { if (_surname != value) { _surname = value; OnPropertyChanged(); } } }
        string _name;
        /// <summary>
        /// Person's name
        /// </summary>
        public string Name { get { return _name; } set { if (_name != value) { _name = value; OnPropertyChanged(); } } }
        string? _thirdName;
        public string? ThirdName { get { return _thirdName; } set { if (_thirdName != value) { _thirdName = value; OnPropertyChanged(); } } }

        /*string _fullName;
        public string FUllName { get { return _fullName; } set { if (_fullName != value) { _fullName = value; OnPropertyChanged(); } } }
        */

        public Person(string surname, string name, string? thirdName)
        {
            if (!string.IsNullOrEmpty(surname) & !string.IsNullOrEmpty(name))
            {
                Surname = surname;
                Name = name;
                ThirdName = thirdName;
                PersonID = new Guid();


            }
        }
       // public Person() : this("Empty", "Empty", "Empty")
         public Person() 
        {
            PersonID = new Guid();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
