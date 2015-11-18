using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using Coderful.EntityFramework.Testing.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using ViewModels.CRUDs;

namespace TestProject.ViewModels.CRUDs
{
   
    [TestClass]
    public class CourseCrudViewModelTests
    {
        CourseCRUDViewModel vm;
        DataContext datacontext;
        Course testCourse;

        [TestInitialize]
        public void TestInit()
        {
            datacontext = new DataContext();
            vm = new CourseCRUDViewModel(datacontext);//todo use that epicly fancy Moq stuff to do this...
            testCourse = new Course()
            {
                Name = "TestCourse",
                Duration = 5
            };
        }

        [TestMethod]
        public void Test()
        {
            Assert.IsNotNull(vm.Courses);
        }

        [TestMethod]
        public void AssertThatItIsPossibleToCreateACourse()
        {
            //setup
            if (datacontext.Courses.Count(course => course.Name == testCourse.Name) != 0)
            {
                datacontext.Courses.RemoveRange(datacontext.Courses.Where(course => course.Name == testCourse.Name));
                datacontext.SaveChanges();
            }

            //process
            vm.SelectedCourse = testCourse;
            vm.SaveCommand.Execute(null);
            var coursesInDb = datacontext.Courses.Where(course => course.Name == testCourse.Name);
            var coursesInVm = vm.Courses.Where(course => course.Name == testCourse.Name);


            //test
            Assert.IsNotNull(coursesInDb);
            Assert.AreEqual(1,coursesInDb.Count());
            Assert.AreEqual(1,coursesInVm.Count());

            var courseInDb = coursesInDb.First();

            Assert.AreEqual(testCourse.Name,courseInDb.Name);
            Assert.AreEqual(testCourse.Duration,courseInDb.Duration);
        }

        [TestMethod]
        public void AssertThatItIsPossibleToDeleteACourse()
        {
            //setup
            if (datacontext.Courses.Count(course => course.Name == testCourse.Name) == 0)
            {
                datacontext.Courses.Add(testCourse);
                datacontext.SaveChanges();
            }
            //process
            vm.SelectedCourse = datacontext.Courses.First(course => course.Name == testCourse.Name);
            vm.DeleteCommand.Execute(null);
            var coursesInDb = datacontext.Courses.Where(course => course.Name == testCourse.Name);
            var coursesInVm = vm.Courses.Where(course => course.Name == testCourse.Name);
            
            //test
            Assert.AreEqual(0, coursesInDb.Count());
            Assert.AreEqual(0, coursesInVm.Count());
        }
    }
}
