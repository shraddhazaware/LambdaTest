using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambdaTestProj.BaseSetup;
using LambdaTestProj;

namespace Certificate_project
{
    [TestFixture("chrome")]
    [TestFixture("edge")]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    internal class Tests : DriverSetup
    {
        public Tests(string browser) : base(browser) { }

        [Test]
        public void SimpleFormDemo()
        {
            Page pg = new Page(driver);
            pg.SimpleDemoForm();
            pg.EnterForm();
            Console.WriteLine("Welcome To LambdaTest");

        }

        [Test]
        public void DragAndDrop()
        {
            Page pg = new Page(driver);
            pg.dragdrop();
        }

        [Test]
        public void InputFormSubmit()
        {
            Page pg = new Page(driver);
            pg.inputform();
        }

    }
}