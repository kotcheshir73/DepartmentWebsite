using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDesktop.Models
{
    interface InterfaceModel<T> 
    {
        List<T> GetList();

        List<T> GetSearchList(string key, string value);

        T GetElem(int id);

        bool CheckElem(T elem);

        bool AddElem(T elem);

        bool UpdElem(T elem);

        bool DelElem(T elem);
    }
}
