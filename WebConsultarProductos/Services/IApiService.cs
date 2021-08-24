using System.Threading.Tasks;
using WebConsultarProductos.Models;

namespace WebConsultarProductos.Services
{
    public interface IApiService
    {
        Task<Response> GetTokenAsync(
            string urlBase,
            string controller,
            TokenRequest request);

        Task<Response> GetListAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken);
    }
}
