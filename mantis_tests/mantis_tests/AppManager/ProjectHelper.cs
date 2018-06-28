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

        public void GotoProjectPage()
        {
            driver.FindElement(By.CssSelector("div[id='sidebar'] > ul[class='nav nav-list'] > li:nth-child(7) > a")).Click();
            driver.FindElement(By.CssSelector("div[class='main-content'] > div[class='page-content'] > div[class='row'] > ul[class='nav nav-tabs padding-18'] > li:nth-child(3) > a")).Click();
        }

        internal void SelectProgect()
        {
            driver.FindElement(By.CssSelector("div[class='table-responsive'] > table > tbody > tr > td > a")).Click();
        }

        public void CreateProject(ProjectData project)
        {
            driver.FindElement(By.CssSelector("fieldset > button[type='submit']")).Click();
            Type(By.Name("name"), project.Name);
            driver.FindElement(By.CssSelector("input[value='Добавить проект']")).Click();
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
            driver.FindElement(By.CssSelector("input[value='Удалить проект']")).Click();
        }

        public void SubmitDeleteProject()
        {
            driver.FindElement(By.CssSelector("input[value='Удалить проект']")).Click();
        }

        public int GetProjectCount()
        {
            return driver.FindElements(By.CssSelector("div[class='table-responsive'] > table > tbody > tr")).Count();
        }
    }
}
