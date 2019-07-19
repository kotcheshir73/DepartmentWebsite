using DepartmentUniversalTabletTest;
using DepartmentWebCore.Services;
using NUnit.Framework;
using System;
using System.Linq;
using Unity;
using WebImplementations.Implementations;
using WebInterfaces.Interfaces;

namespace Tests
{
    public class WebTests
    {
        private INewsService _serviceE;
        private ICommentService _serviceC;
        private IWebProcess _serviceWP;

        [SetUp]
        public void Setup()
        {
            _serviceE = UnityConfig.Container.Resolve<NewsService>();
            _serviceC = UnityConfig.Container.Resolve<CommentService>();
            _serviceWP = UnityConfig.Container.Resolve<WebProcess>();
        }

        [Test]
        public void TestGetMenu()
        {
          //  Assert.IsTrue(MenuService.getMenuElementList() != null);
        }

        [Test]
        public void TestGetEvents()
        {
                //Assert.IsTrue(_serviceE.GetEvents(new WebInterfaces.BindingModels.EventGetBindingModel{}).Succeeded);
        }

        [Test]
        public void TestGetComments()
        {
            Assert.IsTrue(_serviceC.GetComments(new WebInterfaces.BindingModels.CommentGetBindingModel { }).Succeeded);
        }


        [Test]
        public void TestGetDisciplinePositiv()
        {
            //var dis = DisciplineService.GetDiscipline(new BaseInterfaces.BindingModels.DisciplineGetBindingModel() { DisciplineName = "Технологии программирования" });

            //if (dis.Result.Count != 0)
            //{
            //    var tmp = _serviceWP.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
            //    { DisciplineName = dis.Result.FirstOrDefault().DisciplineName });

            //    if (tmp.StatusCode == Enums.ResultServiceStatusCode.Error)
            //    {
            //        _serviceWP.CreateFolderDis(dis.Result);
            //        tmp = _serviceWP.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
            //        { DisciplineName = dis.Result.FirstOrDefault().DisciplineName });
            //    }

            //    foreach (var item in dis.Result.Select(x => new { LecturerName = x.LecturerName }).GroupBy(x => x.LecturerName))
            //    {
            //        tmp.Result.LecturerName += item.Key + " ";
            //    }

            //    Assert.NotNull(tmp.Result);
            //}
            //else
            //{
            //    Assert.Fail();
            //}
        }

        [Test]
        public void TestGetDisciplineNegativ()
        {
            //var dis = DisciplineService.GetDiscipline(new BaseInterfaces.BindingModels.DisciplineGetBindingModel() { DisciplineName = "Технологии програмирования" });

            //if (dis.Result.Count != 0)
            //{
            //    var tmp = _serviceWP.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
            //    { DisciplineName = dis.Result.FirstOrDefault().DisciplineName });

            //    if (tmp.StatusCode == Enums.ResultServiceStatusCode.Error)
            //    {
            //        _serviceWP.CreateFolderDis(dis.Result);
            //        tmp = _serviceWP.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
            //        { DisciplineName = dis.Result.FirstOrDefault().DisciplineName });
            //    }

            //    foreach (var item in dis.Result.Select(x => new { LecturerName = x.LecturerName }).GroupBy(x => x.LecturerName))
            //    {
            //        tmp.Result.LecturerName += item.Key + " ";
            //    }

            //    Assert.IsNull(tmp.Result);
            //}
            //else
            //{
            //    Assert.Pass();
            //}
        }

        Guid curentEventId;

        [Test]
        public void TestEvent1Create()
        {
            //curentEventId = (Guid)_serviceE.CreateEvent(new WebInterfaces.BindingModels.EventSetBindingModel
            //{
            //    Content = "Все очень хорошо(Тест)",
            //    DepartmentUser = "Эгов Е.Н.",
            //    Title = "Хорошая новость"
            //}).Result;
            //Assert.NotNull(curentEventId);
        }

        [Test]
        public void TestEvent2Update()
        {
            //_serviceE.UpdateEvent(new WebInterfaces.BindingModels.EventUpdateBindingModel
            //{
            //    Content = "Все очень ПЛОХОООООО)Тест(",
            //    Title = "Плохая новость",
            //    Id = curentEventId
            //});
            //Assert.AreEqual(_serviceE.GetEvent(new WebInterfaces.BindingModels.EventGetBindingModel {Id = curentEventId }).Result.Title, "Плохая новость");
        }

        [Test]
        public void TestEvent3Delete()
        {
            //_serviceE.DeleteEvent(new WebInterfaces.BindingModels.EventGetBindingModel
            //{
            //    Id = curentEventId
            //});
            //Assert.IsFalse(_serviceE.GetEvent(new WebInterfaces.BindingModels.EventGetBindingModel { Id = curentEventId }).Succeeded);
        }
    }
}