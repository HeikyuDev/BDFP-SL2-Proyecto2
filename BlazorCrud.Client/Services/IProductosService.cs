using Crud.Shared;
namespace BlazorCrud.Client.Services
{
	public interface IProductosService
	{
		Task<List<ProductoBIB>> Lista();
		Task<ProductoBIB> Buscar(int id);
		Task<int> Guardar(ProductoBIB producto);
		Task<int> Editar(ProductoBIB producto);
		Task<bool> Eliminar(int id);

	}
}
