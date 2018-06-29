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
            AccountData admin = new AccountData()
            {
                Name = "administrator",
                Pass = "root"
            };

            ProjectData prog = new ProjectData()
            {
                Name = "Test Project",
            };

            app.Logon.Login(admin);
            app.Project.GotoProjectPage();
            if (!app.Project.FindProject())
            {
                app.Project.CreateProject(prog);
            }

            List<ProjectData> oldList = app.Project.GetProjectList();
            ProjectData removePr = oldList[0];

            int oldPr = app.Project.GetProjectCount();

            app.Project.SelectProgect();
            app.Project.DeleteProject();
            app.Project.SubmitDeleteProject();

            int newPr = app.Project.GetProjectCount();
            Assert.AreEqual(oldPr - 1, newPr);

            List<ProjectData> newList = app.Project.GetProjectList();
            Assert.AreEqual(oldList, newList);

        }
    }
}
