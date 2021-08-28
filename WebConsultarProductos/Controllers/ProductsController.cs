using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebConsultarProductos.Models;
using WebConsultarProductos.Services;

namespace WebConsultarProductos.Controllers
{

    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ApiService _apiService;

        public ProductsController()
        {
            _apiService = new ApiService();
        }


        [HttpGet]
        [Route("/productos")]
        public async Task<string> ProductsAsync()
        {
            var tokenRequest = new TokenRequest()
            {
                access_key = "...",
                username = "correo del usuario"
            };
            //var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkFFNjY0RkJCMkY4OUMwQjhEOTgxMTYwQTVBMEVEMjhCNjk3MDkzQUNSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6InJtWlB1eS1Kd0xqWmdSWUtXZzdTaTJsd2s2dyJ9.eyJuYmYiOjE2Mjk4MjQwOTUsImV4cCI6MTYyOTkxMDQ5NSwiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwIiwiYXVkIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwL3Jlc291cmNlcyIsImNsaWVudF9pZCI6IlNpaWdvQVBJIiwic3ViIjoiMTA1NTI0OSIsImF1dGhfdGltZSI6MTYyOTgyNDA5NSwiaWRwIjoibG9jYWwiLCJuYW1lIjoib2ZhcnRlZXVAZ21haWwuY29tIiwibWFpbF9zaWlnbyI6Im9mYXJ0ZWV1QGdtYWlsLmNvbSIsImNsb3VkX3RlbmFudF9jb21wYW55X2tleSI6Ik9GQVJURVNBUzAwMSIsInVzZXJzX2lkIjoiNjg1IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDQwMzYwMiIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiNDI2IiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiMmE0ZWE4NjJlNDMzNDBiNGIyNzk3NzRmZmRkZDAzYjUiLCJhY2NvdW50YW50IjoiZmFsc2UiLCJqdGkiOiJGOENDMjBFMDQ1OEZBRENDM0YyNjhGQ0VCOUIzQ0UwNSIsImlhdCI6MTYyOTgyNDA5NSwic2NvcGUiOlsiU2lpZ29BUEkiXSwiYW1yIjpbImN1c3RvbSJdfQ.ms7CqK5VRbyMbmBCM73TQ1F8m7IixNBj5vWHMnRhDRAdfZfbRwYCuU68cfgrpp1kodpcRq3OaOBwFq_BsxuBpzkYxaQ-qiVMZTnQXSTnOKzMYcx8YEDY5kmEqHKHjtstOuES-0Nzk5EYeYuDB58sm2g2sAQoTwKYyI-o_dSDYdSxGUA6p0Bzv-1I96U081liD68Gep0ezH7rvYhVGzpbAb4R5tMcDwtQ6JnruBAt5zcCLFoDvgYQHE9bJmU_EvLCTpNI4jX6OwAIKi7bpW7MRybibj-rcsM_XmxRTN7TaDoVHOW01hWi5ug2QPxQlIpez78RwagJ7kwneNBbldzLag";
            var token = await _apiService.GetTokenAsync("https://api.siigo.com", "auth", tokenRequest);

            if (token.IsSuccess)
            {
                var count = 1;
                var productList = new List<ProductResponse>();

                ApiResponse data;
                do
                {
                    var tokenResponse = (TokenResponse)token.Result;
                    var complement = $"products?page={count}&page_size=100";
                    var response = await _apiService.GetListAsync<ApiResponse>("https://api.siigo.com", "v1/", complement, tokenResponse.Type, tokenResponse.Token);
                        data = JsonConvert.DeserializeObject<ApiResponse>(response.Result.ToString());
                
                    foreach (var p in data.results)
                    {
                        productList.Add(new ProductResponse() { 
                            id = p.id,
                            code = p.code,
                            name = p.name,
                            account_group = new AccountGroup()
                            {
                                id = p.account_group.id,
                                name = p.account_group.name
                            },
                            type = p.type,
                            stock_control = p.stock_control,
                            active = p.active,
                            tax_classification = p.tax_classification,
                            tax_included = p.tax_included,
                            tax_consumption_value = p.tax_consumption_value,
                            taxes = p.taxes?.Select(pt => new Tax
                            {
                                id = pt.id,
                                name = pt.name
                            }).ToList(),
                            prices = p.prices?.Select(pr => new Price
                            {
                                currency_code = pr.currency_code,
                                price_list = pr.price_list?.Select(pl => new PriceList
                                {
                                    name = pl.name,
                                    position = pl.position,
                                    value = pl.value
                                }).ToList(),
                            }).ToList(),
                            reference = p.reference,
                            description = p.description,
                            warehouses = p.warehouses?.Select(w => new Warehouse
                            {
                                id = w.id,
                                name = w.name,
                                quantity = w.quantity
                            }).ToList()
                        });
                    }
                    count++;
                } while (data._links.next != null);

                var respuesta = JsonConvert.SerializeObject(productList);

                foreach (var item in productList)
                {
                    foreach (var bodega in item.warehouses)
                    {
                        var cantidadbodega = bodega.quantity;
                    }
                }

                return respuesta;

            } else
            {
                return token.Message;
            }
        }

    }
}
