using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    public class DeleteProjectTest : BaseClassTest
    {
        [Test]
        public void TestDeleteProject()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Pass = "root"
            };
            ProjectData proj = new ProjectData()
            {
                Name = "Test Project",
            };

            app.Project.GotoProjectPage();

            //List<ProjectData> oldList = app.Project.GetProjectList();
            List<ProjectData> oldList = app.Api.GetAllProjects(account);
            ProjectData existPr = oldList.Find(x => x.Name == proj.Name);
            if (existPr == null)
            {
                app.Api.AddProjects(account, proj);
                oldList.Add(proj);
            }

            int oldPr = oldList.Count;

            app.Project.SelectProgect(proj);
            app.Project.DeleteProject();
            app.Project.SubmitDeleteProject();

            var removeItem = oldList.SingleOrDefault(x => x.Name == proj.Name);
            oldList.Remove(removeItem);
            var oldNames = oldList.Select(x => x.Name).ToList();
            oldNames.Sort();
            //List<ProjectData> newList = app.Project.GetProjectList();
            List<ProjectData> newList = app.Api.GetAllProjects(account);
            var newNames = newList.Select(x => x.Name).ToList();
            newNames.Sort();
            int newPr = newList.Count;
            Assert.AreEqual(oldPr - 1, newPr);
            Assert.AreEqual(oldNames, newNames);

        }
    }
}
