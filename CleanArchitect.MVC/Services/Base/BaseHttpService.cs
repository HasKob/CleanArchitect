using System.Net.Http.Headers;
using CleanArchitect.MVC.Contracts;

namespace CleanArchitect.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected readonly ILocalStorageService _localStorageService;
        protected IClient _client;
        public BaseHttpService(ILocalStorageService localStorageService, IClient client)
        {
            _localStorageService = localStorageService;
            _client = client;
        }
        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == StatusCodes.Status400BadRequest)
                return new Response<Guid>() { Message = "Validation errors have occured.", ValidationErrors = ex.Response, Success = false };
            else if (ex.StatusCode == StatusCodes.Status404NotFound)
                return new Response<Guid>() { Message = "The requested item couldn't be found.", Success = false };
            else
                return new Response<Guid>() { Message = "Something went wrong, please try again.", Success = false };
        }
        protected void AddBearerToken()
        {
            if (_localStorageService.Exists("token"))
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _localStorageService.GetStorageValue<string>("token"));
        }
    }
}
