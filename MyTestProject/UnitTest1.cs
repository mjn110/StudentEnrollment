using AngleSharp.Dom;
using Bunit;
using StudentEnrolment.Client.Pages;
using StudentEnrolment.Shared;
using StudentEnrolment.Shared.ViewModels;

namespace MyTestProject
{
    public class UnitTest1 : TestContext
    {
        [Fact]
        public void Test1()
        {
            AddStudentViewModel SVM = new AddStudentViewModel();
            var cut = RenderComponent<StudentEnrolment.Client.Pages.AddStudent>();

            cut.Find("input[aria-label=Name]").Change("ab");
            cut.Find("input[aria-label=Birth]").Change(new DateTime(1990-02-02));
            cut.Find("input[aria-label=Course]").Change("Law");
            cut.Find("input[aria-label=Start]").Change(new DateTime(2020-02-02));
            cut.Find("input[aria-label=End]").Change(new DateTime(2024-02-02));
            cut.Find("select[aria-label=Welsh]").Change("none");

            cut.Find("form").Submit();
        }
    }
}