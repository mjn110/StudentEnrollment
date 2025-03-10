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

            // Step 2: Mock GET request (fetch existing student data)
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri.ToString().Contains($"https://localhost:7117/api/Student/GetStudentForUpdate/?id={studentId}")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(updatedStudent)
                });

            // Step 3: Mock PUT request (update student data)
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Put &&
                        req.RequestUri.ToString().Contains($"https://localhost:7117/api/Student/{studentId}")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NoContent
                });

            var httpClient = new HttpClient(mockHandler.Object)
            {
                BaseAddress = new Uri("http://localhost:7117/")
            };

            Services.AddSingleton(httpClient);

            // Render the UpdateStudent component
            var cut = RenderComponent<UpdateStudent>(parameters => parameters
                .Add(p => p.StudentVM, updatedStudent) // Pass the student object to the component
                .Add(p => p.studentid, studentId) // Pass the studentId to the component
            );

            //await Task.Delay(100);

            ////Fill out the form with updated values
            //cut.Find("input[aria-label='Name']").Change(updatedStudent.StudentName);
            //cut.Find("select[aria-label='Welsh']").Change(updatedStudent.WelshLanguageProficiency);

            // Step 5: Simulate form submission (update student)
            await cut.InvokeAsync(() => cut.Find("form").Submit());

            // Wait for async API call to complete

            // Debugging: Log requests
            //var requests = mockHandler.Invocations;
            //Console.WriteLine($"Total requests made: {requests.Count}");
            //foreach ( var request in requests)
            //{
            //    Console.WriteLine(request);
            //}

            // Assert: Ensure API call happened
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Put &&
                    req.RequestUri.ToString().Contains($"api/Student/{studentId}")),
                ItExpr.IsAny<CancellationToken>()
            );

            //// Ensure success message is displayed
            //cut.WaitForState(() => cut.Markup.Contains("Student updated successfully!"));
            //Assert.Contains("Student updated successfully!", cut.Markup);
        }
    }
}
