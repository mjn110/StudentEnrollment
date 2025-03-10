using Bunit;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using StudentEnrolment.Client.Pages;
using StudentEnrolment.Shared;
using StudentEnrolment.Shared.ViewModels;

namespace MyTestProject
{
    public class AddStudentTest : TestContext
    {
        [Fact]
        public void AddStudent()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            var expectedStudent = new Student
            {
                StudentName = "Joe Yuen",
                DateOfBirth = new DateTime(2000, 5, 10),
                CourseName = "Artificial Intelligence",
                StartDate = new DateTime(2024, 9, 1),
                EndDate = new DateTime(2028, 6, 30),
                WelshLanguageProficiency = "Fluent"
            };

            // Mock HTTP response (Simulating API call success)
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Post &&
                        req.RequestUri.ToString() == "https://localhost:7117/api/Student"),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                });

            var httpClient = new HttpClient(mockHandler.Object)
            {
                BaseAddress = new Uri("http://localhost:7117/")
            };

            Services.AddSingleton(httpClient);

            // Render the component
            var cut = RenderComponent<AddStudent>();


            //Act
            AddStudentViewModel SVM = new AddStudentViewModel();
            cut.Find("input[aria-label='Name']").Change(expectedStudent.StudentName);
            cut.Find("input[aria-label='Birth']").Change(expectedStudent.DateOfBirth.ToString("yyyy-MM-dd"));
            cut.Find("input[aria-label='Course']").Change(expectedStudent.CourseName);
            cut.Find("input[aria-label='Start']").Change(expectedStudent.StartDate.ToString("yyyy-MM-dd"));
            cut.Find("input[aria-label='End']").Change(expectedStudent.EndDate.ToString("yyyy-MM-dd"));
            cut.Find("select[aria-label='Welsh']").Change(expectedStudent.WelshLanguageProficiency);

            cut.Find("form").Submit();

            //Assert
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post &&
                    req.RequestUri.ToString() == "https://localhost:7117/api/Student"),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}