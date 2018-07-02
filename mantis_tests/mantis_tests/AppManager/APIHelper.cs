using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;


namespace mantis_tests
{
    public class APIHelper : HelperBase
    {

        public APIHelper(ApplicationManager manager)
            : base(manager)
        { }

        public void CreateNewIssue(AccountData acc, ProjectData proj, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = proj.Id;
            client.mc_issue_add(acc.Name, acc.Pass, issue);
        }

        public void AddProjects(AccountData acc, ProjectData proj)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = proj.Name;
            client.mc_project_add(acc.Name, acc.Pass, project);
        }

        public List<ProjectData> GetAllProjects(AccountData acc)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            List<ProjectData> projs = new List<ProjectData>();
            var elements = client.mc_projects_get_user_accessible(acc.Name, acc.Pass);
            foreach (var el in elements)
            {
                string name = el.name;
                string id = el.id;

                projs.Add(new ProjectData()
                {
                    Name = name,
                    Id = id
                });
            }
            return projs;
        }
    }
}
