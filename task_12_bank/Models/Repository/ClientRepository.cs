using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types;
using System.IO;
using System.Windows;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace task_12_bank.Models.Repository
{
    internal class ClientRepository:IClientRepository
    {
        private string _path=$"Clients.xml";
        private List<Client> _clients;
        public ClientRepository()
        {
            _clients = (List<Client>)GetFromFile(_path);
            if (_clients==null)
                 GenerateClients();
        }
        public IEnumerable<Client> GetAll()
        {
            GetFromFile();
            return _clients;
        }
        public Client GetClient(string Surname)
        {
            if (string.IsNullOrEmpty(Surname))
                return (null);
           return( _clients.FirstOrDefault(x => x.Surname == Surname));
        }
        public bool IsClient(string Surname)
        {
            if (string.IsNullOrEmpty(Surname))
                return (false);
            return (_clients.FirstOrDefault(x => x.Surname == Surname)!=null?true:false);
        }
        private IEnumerable<Client> GetFromFile()
        {
            return GetFromFile(_path);
        }
        private IEnumerable<Client> GetFromFile(string Path) 
        {
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<Client>));           
            IEnumerable<Client>? clients;
            try
            {
                if (!string.IsNullOrEmpty(Path))
                    if (File.Exists(Path))
                    {
                        using (FileStream reader = new FileStream(Path,FileMode.Open))
                        {
                             clients = xmlFormatter.Deserialize(reader) as List<Client>;

                            reader.Close();
                        }
                        return (clients);
                    }
             }
            catch(Exception e) { MessageBox.Show($"File is not found or Empty: {Path}"); }
            return (null);
                
        }
        public void SaveToRepository(IEnumerable<Client> rep)
        {
            _clients = rep.ToList<Client>();
            XmlSerializer formatter = new XmlSerializer(typeof(List<Client>));
            try
            {
                if (!string.IsNullOrEmpty(_path))
                {
                    if (!File.Exists(_path))
                    {
                        if (MessageBox.Show($"The file {_path} is not exists.\n\r Should it be created" +
                            $"\n\r if not all infarmation will be lost", "File not found",
                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                            return;
                    }
                    using (FileStream writer = new FileStream(_path, FileMode.Create))
                    {
                        formatter.Serialize(writer, _clients);
                        writer.Close();
                    }
                }
                    
            }
            catch (Exception e) {MessageBox.Show($"Can't send to the file path is {_path}\n\r"+e.Message); }
        }
        private void GenerateClients()
        {
            List<Client> _listClients = new List<Client>();
            _listClients.Add(new Client(
                name: "Denis",
                surname:"Smolov",
                thirdName:"Petrovich",
                phone:"89161192123",
                sID:"4500",
                nID:"324567"
                ));
            _listClients.Add(new Client(
                name: "Vasya",
                surname: "Petrov",
                thirdName: "Nickolaevich",
                phone: "89161135421",
                sID: "4501",
                nID: "765432"
                ));
              _listClients.Add(new Client(
                name: "Oleg",
                surname: "Pukin",
                thirdName: "Olegovich",
                phone: "89431138765",
                sID: "4701",
                nID: "763412"
                ));
            _clients=_listClients;
        }
    }
}
