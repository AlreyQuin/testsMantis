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
        [Test]
        public void TestCreateProject()
        {
            AccountData admin = new AccountData()
            {
                Name = "administrator",
                Pass = "root"
            };

            app.Logon.Login(admin);
            

        }
    }
}
