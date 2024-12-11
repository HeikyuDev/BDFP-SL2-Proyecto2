using Crud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
	public class ProductosSerivice : IProductosService
	{
		private readonly HttpClient _http;

		public ProductosSerivice(HttpClient http)
		{
			_http = http;
		}
		public async Task<List<ProductoBIB>> Lista()
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<List<ProductoBIB>>>("api/Producto/Lista");

			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje);
		}
		public async Task<ProductoBIB> Buscar(int id)
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<ProductoBIB>>($"api/Producto/Buscar/{id}");

			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje);
		}
		public async Task<int> Guardar(ProductoBIB producto)
		{
			var result = await _http.PostAsJsonAsync($"api/Producto/Guardar", producto);
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.Valor!;
			else
				throw new Exception(response.Mensaje);
		}
		public async Task<int> Editar(ProductoBIB producto)
		{
			var result = await _http.PutAsJsonAsync($"api/Producto/Editar/{producto.Id}", producto);
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.Valor!;
			else
				throw new Exception(response.Mensaje);
		}

		public async Task<bool> Eliminar(int id)
		{
			var result = await _http.DeleteAsync($"api/Producto/Eliminar/{id}");
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.EsCorrecto!;
			else
				throw new Exception(response.Mensaje);
		}
	}
}
