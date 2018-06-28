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
        public void TestCreateProject(ProjectData proj)
        {
            AccountData admin = new AccountData()
            {
                Name = "administrator",
                Pass = "root"
            };

            app.Logon.Login(admin);
            app.Project.GotoProjectPage();

            int oldPr = app.Project.GetProjectCount();

            app.Project.CreateProject(proj);

            int newPr = app.Project.GetProjectCount();
            Assert.AreEqual(oldPr + 1, newPr);
        }
    }
}
