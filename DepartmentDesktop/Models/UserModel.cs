using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DepartmentDesktop.Models
{
    //public class UserModel : InterfaceModel<User>
    //{
    //    private DepartmentDbContext _db;

    //    private string _error;

    //    public string Error { get { return _error; } }

    //    public UserModel()
    //    {
    //        _db = new DepartmentDbContext();
    //    }

    //    public List<User> GetList()
    //    {
    //        try
    //        {
    //            return _db.Users.ToList();
    //        }
    //        catch (Exception ex)
    //        {
    //            _error = ex.Message;
    //            return null;
    //        }
    //    }

    //    public List<User> GetSearchList(string key, string value)
    //    {
    //        //var list = _db.Users.ToList();
    //        //foreach(var elem in keys)
    //        //{
    //        //    switch(elem.Key)
    //        //    {
    //        //        case "Login":
    //        //            list = list.Where(r => r.Login.Contains(elem.Value));
    //        //            break;
    //        //    }
    //        //}
    //        return null;
    //    }

    //    public User GetElem(int id)
    //    {
    //        try
    //        {
    //            return _db.Users.Find(id);
    //        }
    //        catch (Exception ex)
    //        {
    //            _error = ex.Message;
    //            return null;
    //        }
    //    }

    //    public bool CheckElem(User elem)
    //    {
    //        _error = "";
    //        if (elem.Login == "")
    //        {
    //            _error = "Поле \"логин\" не заполнено!";
    //            return false;
    //        }
    //        if (elem.Password == "")
    //        {
    //            _error = "Поле \"пароль\" не заполнено!";
    //            return false;
    //        }
    //        if (elem.Rule == null)
    //        {
    //            _error = "Пользователь не принадлежит ни к одной группе!";
    //            return false;
    //        }
    //        return true;
    //    }

    //    public bool AddElem(User elem)
    //    {
    //        if (!CheckElem(elem))
    //        {
    //            return false;
    //        }
    //        elem.DateCreate = DateTime.Now;
    //        elem.DateLastVisit = DateTime.Now;
    //        try
    //        {
    //            if (_db.Users.SingleOrDefault(r => r.Login == elem.Login && r.Password == elem.Password) != null)
    //            {
    //                _error = "Имеется пользователь с таким логином и паролем!";
    //                return false;
    //            }
    //            _db.Users.Add(elem);
    //            _db.SaveChanges();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            _error = ex.Message;
    //            return false;
    //        }
    //    }

    //    public bool UpdElem(User elem)
    //    {
    //        if (!CheckElem(elem))
    //        {
    //            return false;
    //        }
    //        try
    //        {
    //            if (_db.Users.SingleOrDefault(r => r.Login == elem.Login && r.Password == elem.Password &&
    //                r.Id != elem.Id) != null)
    //            {
    //                _error = "Имеется пользователь с таким логином и паролем!";
    //                return false;
    //            }
    //            _db.Entry(elem).State = EntityState.Modified;
    //            _db.SaveChanges();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            _error = ex.Message;
    //            return false;
    //        }
    //    }

    //    public bool DelElem(User elem)
    //    {
    //        try
    //        {
    //            _db.Users.Remove(_db.Users.Find(elem.Id));
    //            _db.SaveChanges();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            _error = ex.Message;
    //            return false;
    //        }
    //    }
    //}
}
