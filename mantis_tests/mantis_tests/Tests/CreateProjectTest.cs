using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    [TestFixture]
    public class CreateProjectTest : BaseClassTest
    {
        public static IEnumerable<ProjectData> RandomProgpDataProvider()
        {
            List<ProjectData> proj = new List<ProjectData>();
            for (int i = 0; i < 1; i++)
            {
                proj.Add(new ProjectData()
                {
                    Name = GenerateRandomString(10)
                });
            }
            return proj;
        }

        [Test, TestCaseSource("RandomProgpDataProvider")]
        public void TestCreateProjectWithRandomName(ProjectData proj)
        {
            app.Project.GotoProjectPage();

            int oldPr = app.Project.GetProjectCount();

            app.Project.CreateProject(proj);

            int newPr = app.Project.GetProjectCount();
            Assert.AreEqual(oldPr + 1, newPr);
        }

        [Test]
        public void TestCreateProject()
        {
            ProjectData project = new ProjectData()
            {
                Name = "Test Project",
            };
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Pass = "root"
            };

            app.Project.GotoProjectPage();

            // List<ProjectData> oldList = app.Project.GetProjectList();
            List<ProjectData> oldList = app.Api.GetAllProjects(account);
            ProjectData existPr = oldList.Find(x => x.Name == project.Name);

            if (existPr != null)
            {
                app.Project.SelectProgect(existPr);
                app.Project.DeleteProject();
                app.Project.SubmitDeleteProject();
                oldList.Remove(existPr);
            }
            int oldPr = oldList.Count;
            //app.Project.CreateProject(project);
            app.Api.AddProjects(account, project);
            oldList.Add(project);
            var oldNames = oldList.Select(x => x.Name).ToList();
            oldNames.Sort();
            //List<ProjectData> newList = app.Project.GetProjectList();
            List<ProjectData> newList = app.Api.GetAllProjects(account);
            var newNames = newList.Select(x => x.Name).ToList();
            newNames.Sort();

            int newPr = newList.Count;
            Assert.AreEqual(oldPr + 1, newPr);
            Assert.AreEqual(oldNames, newNames);
        }
    }
}

