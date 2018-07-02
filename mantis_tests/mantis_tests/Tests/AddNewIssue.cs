using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{ 

    [TestFixture]
    public class AddNewIssue : BaseClassTest
    {
        [Test]
        public void AddNewIssueTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Pass = "root"
            };

            List<ProjectData> projList = app.Project.GetProjectList();
            ProjectData proj = projList[0];

            IssueData issue = new IssueData()
            {
                Summary = "some_short_text",
                Description = "some_long_text",
                Category = "General"
            };

            app.Api.CreateNewIssue(account, proj, issue);
        }
    }
}
