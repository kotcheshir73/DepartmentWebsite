using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDesktop.Models
{
    //public class RuleModel : InterfaceModel<Role>
    //{
    //    private DepartmentDbContext _db;

    //    private string _error;

    //    public string Error { get { return _error; } }

    //    public RuleModel()
    //    {
    //        _db = new DepartmentDbContext();
    //    }

    //    public List<Rule> GetList()
    //    {
    //        try
    //        {
    //            return _db.Rules.ToList();
    //        }
    //        catch (Exception ex)
    //        {
    //            _error = ex.Message;
    //            return null;
    //        }
    //    }

    //    public List<Rule> GetSearchList(string key, string value)
    //    {
    //        switch (key)
    //        {
    //            case "RuleName":
    //                try
    //                {
    //                    return _db.Rules.Where(r => r.RuleName.Contains(value)).ToList();
    //                }
    //                catch (Exception ex)
    //                {
    //                    _error = ex.Message;
    //                    return null;
    //                }
    //        }
    //        _error = "Нет ключа для поиска";
    //        return null;
    //    }

    //    public Rule GetElem(int id)
    //    {
    //        try
    //        {
    //            return _db.Rules.Find(id);
    //        }
    //        catch (Exception ex)
    //        {
    //            _error = ex.Message;
    //            return null;
    //        }
    //    }

    //    public bool CheckElem(Rule elem)
    //    {
    //        _error = "";
    //        if (elem.RuleName == "")
    //        {
    //            _error = "Поле \"Название\" не заполнено!";
    //            return false;
    //        }
    //        return true;
    //    }

    //    public bool AddElem(Rule elem)
    //    {
    //        if (!CheckElem(elem))
    //        {
    //            return false;
    //        }
    //        try
    //        {
    //            if (_db.Rules.SingleOrDefault(r => r.RuleName == elem.RuleName) != null)
    //            {
    //                _error = "Имеется пользователь с таким логином и паролем!";
    //                return false;
    //            }
    //            _db.Rules.Add(elem);
    //            _db.SaveChanges();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            _error = ex.Message;
    //            return false;
    //        }
    //    }

    //    public bool UpdElem(Rule elem)
    //    {
    //        if (!CheckElem(elem))
    //        {
    //            return false;
    //        }
    //        try
    //        {
    //            if (_db.Rules.SingleOrDefault(r => r.RuleName == elem.RuleName &&
    //                r.Id != elem.Id) != null)
    //            {
    //                _error = "Имеется роль с таким названием!";
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

    //    public bool DelElem(Rule elem)
    //    {
    //        try
    //        {
    //            _db.Rules.Remove(_db.Rules.Find(elem.Id));
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
