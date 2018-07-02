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
    public class ProjectHelper : HelperBase
    {
        private string baseUrl;

        public ProjectHelper(ApplicationManager manager, String baseUrl)
            : base(manager)
        {
            this.baseUrl = baseUrl;
        }



        private void FindByCss(string css)
        {
            driver.FindElement(By.CssSelector(css)).Click();
        }


        public void GotoProjectPage()
        {
            AccountData admin = new AccountData()
            {
                Name = "administrator",
                Pass = "root"
            };

            manager.Logon.Login(admin);
            FindByCss("div[id='sidebar'] > ul[class='nav nav-list'] > li:nth-child(7) > a");
            FindByCss("div[class='main-content'] > div[class='page-content'] > div[class='row'] > ul[class='nav nav-tabs padding-18'] > li:nth-child(3) > a");
        }

        public void SelectProgect(ProjectData proj)
        {
            GotoProjectPage();
            driver.FindElement(By.XPath(@"//tr/td/a[contains(text(), '" + proj.Name + "')]")).Click();
            
        }

        public void CreateProject(ProjectData project)
        {
            FindByCss("fieldset > button[type='submit']");
            Type(By.Name("name"), project.Name);
            FindByCss("input[value='Добавить проект']");
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
                ICollection<IWebElement> projs = driver.FindElements(By.CssSelector("div[class='col-md-12 col-xs-12'] > div:nth-child(2) > div:nth-child(2) > div:nth-child(1) > div:nth-child(2) > table > tbody > tr"));
                foreach (IWebElement proj in projs)
                {
                    IWebElement a = proj.FindElement(By.TagName("a"));
                    string name = a.Text;
                    string href = a.GetAttribute("href");
                    Match m = Regex.Match(href, @"\d+$");
                    string id = m.Value;

                    projectCache.Add(new ProjectData()
                    {
                        Name = name,
                        Id = id
                    });
                }
                
            }
                return new List<ProjectData>(projectCache);
        }
    }
}
