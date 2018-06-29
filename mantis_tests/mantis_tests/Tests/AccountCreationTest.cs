using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTest : BaseClassTest
    {
        [TestFixtureSetUp]
        public void setUpConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
                
        }

        [Test]
        public void TestAccountRegistration()
        {
            List<AccountData> accounts = app.Admin.GetAllAccounts();

            AccountData account = new AccountData()
            {
                Name = "testuser",
                Pass = "password",
                Email = "testuser@localhost.localdomain"
            };

            app.Admin.DeleteAccount(account);

            app.James.Delete(account);
            app.James.Add(account);

            app.RegistHelper.RegistHelper(account);
        }

        [TestFixtureTearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }
}
