using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database_connection.Model
{
    internal interface IRepository<T> :IDisposable
    {
        /// <summary>
        /// get all objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetItemList();
        /// <summary>
        /// get one object using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetItem(int id);
        /// <summary>
        /// creating an object
        /// </summary>
        /// <param name="item"></param>
        void Create(T item);
        /// <summary>
        /// update the object
        /// </summary>
        /// <param name="item"></param>
        void Update(T item);
        /// <summary>
        /// delete the object using id
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        /// <summary>
        /// save the changes
        /// </summary>
        void Save();
    }
}
