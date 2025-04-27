using Bunit;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using StudentEnrolment.Client.Pages;
using StudentEnrolment.Shared;
using StudentEnrolment.Shared.ViewModels;
using System.Linq;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace MyTestProject
{
    public class UpdateStudentTest : TestContext
    {
        [Fact]
        public async void UpdateStudent()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();

            var studentId = 1;
            var updatedStudent = new UpdateStudentViewModel
            {
                StudentName = "Harry Goldingay",
                WelshLanguageProficiency = "Fluent"
            };


            var mockHttpHandler = new Mock<HttpMessageHandler>();

            // Mock GET request
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri.ToString().Contains($"api/Student/GetStudentForUpdate?id={studentId}")
                    ),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(updatedStudent)
                });

            // Mock PUT request
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Put &&
                        req.RequestUri.ToString().Contains($"api/Student/{studentId}")
                    ),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent });

            var client = new HttpClient(mockHttpHandler.Object) { BaseAddress = new Uri("https://localhost:7117/") };

            Services.AddSingleton(client);
            var mockNav = new Mock<NavigationManager>();
            Services.AddSingleton(mockNav.Object);

            // Act
            var cut = RenderComponent<UpdateStudent>(parameters => parameters.Add(p => p.studentid, studentId));

            // Verify student data loads
            Assert.Equal("Harry Goldingay", cut.Find("input[aria-label='Name']").GetAttribute("value"));

            // Simulate form update
            cut.Find("input[aria-label='Name']").Change("Updated Name");
            cut.Find("select[aria-label='Welsh']").Change("none");

            // Submit the form
            cut.Find("button").Click();

            // Verify PUT request was sent
            mockHttpHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Put &&
                    req.RequestUri.ToString().Contains($"api/Student/{studentId}")
                ),
                ItExpr.IsAny<CancellationToken>()
            );

            // Verify navigation occurs after update
            //mockNav.Verify(nav => nav.NavigateTo("/", true), Times.Once);
        }
    }
}
