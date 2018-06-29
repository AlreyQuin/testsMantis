using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager)
            : base(manager) { }

        private void FindByCss(string css)
        {
            driver.FindElement(By.CssSelector(css)).Click();
        }


        public void GotoProjectPage()
        {
            FindByCss("div[id='sidebar'] > ul[class='nav nav-list'] > li:nth-child(7) > a");
            FindByCss("div[class='main-content'] > div[class='page-content'] > div[class='row'] > ul[class='nav nav-tabs padding-18'] > li:nth-child(3) > a");
        }

        public void SelectProgect()
        {
            FindByCss("div[class='table-responsive'] > table > tbody > tr > td > a");
        }

        public void CreateProject(ProjectData project)
        {
            FindByCss("fieldset > button[type='submit']");
            Type(By.Name("name"), project.Name);
            FindByCss("input[value='Добавить проект']");
        }

        public bool FindProject()
        {
            return IsElementPresent(By.CssSelector("div[class='table-responsive'] > table > tbody > tr"));
        }

        public bool FindProjects()
        {
            GotoProjectPage();
            return FindProject();
        }

        public void DeleteProject()
        {
            FindByCss("input[value='Удалить проект']");
        }

        public void SubmitDeleteProject()
        {
            FindByCss("input[value='Удалить проект']");
        }

        public int GetProjectCount()
        {
            return driver.FindElements(By.CssSelector("div[class='table-responsive'] > table > tbody > tr")).Count();
        }

        private List<ProjectData> projectCache = null;

        public List<ProjectData> GetProjectList()
        {
            if (projectCache == null)
            {
                projectCache = new List<ProjectData>();
                GotoProjectPage();
                ICollection<IWebElement> projs = driver.FindElements(By.CssSelector("div[class='col-md-12 col-xs-12'] > div:nth-child(2) > div:nth-child(2) > div:nth-child(1) > div:nth-child(2) > table > tbody"));
                foreach (IWebElement proj in projs)
                {
                    var name = proj.FindElements(By.TagName("tr")).ToArray();
                    projectCache.Add(new ProjectData()
                    {
                        Name = name[1].Text,
                    });
                }
                
            }
                return new List<ProjectData>(projectCache);
        }
    }
}
