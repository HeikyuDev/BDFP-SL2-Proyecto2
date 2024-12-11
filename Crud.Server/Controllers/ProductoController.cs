using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Server.Models;
using Crud.Shared;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Crud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly DbcrudSeminarioContext _dbContext;

        public ProductoController(DbcrudSeminarioContext dbContext)
        {
                _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<ProductoBIB>>();
            var listaProducto = new List<ProductoBIB>();

            try
            {


                foreach (var item in await _dbContext.Productos.ToListAsync())
                {
                    listaProducto.Add(new ProductoBIB
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Precio = item.Precio,
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaProducto;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<ProductoBIB>();
            var Producto = new ProductoBIB();

            try
            {
                var dbProducto = await _dbContext.Productos.FirstOrDefaultAsync(x => x.Id == id);

                if (dbProducto != null)
                {
                    Producto.Id = dbProducto.Id;
                    Producto.Nombre = dbProducto.Nombre;
                    Producto.Precio = dbProducto.Precio;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = Producto;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                } 
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        ///////////////////////////////////////////////////

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(ProductoBIB productoBIB)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                // Mapear ProductoBIB a Producto (el modelo que se usa en la base de datos)
                var dbProducto = new Producto
                {
                    Nombre = productoBIB.Nombre,
                    Precio = productoBIB.Precio// Asigna un valor predeterminado si Precio es null
                };

                // Agregar el producto a la base de datos
                _dbContext.Add(dbProducto);
                await _dbContext.SaveChangesAsync();

                // Verificar si se guardó correctamente
                if (dbProducto.Id != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProducto.Id;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No guardado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        ///////////////////////////////////////////////////

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(ProductoBIB producto, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProducto = await _dbContext.Productos.FirstOrDefaultAsync(e => e.Id == id); 
               

               

                if (dbProducto != null)
                {
                    dbProducto.Nombre = producto.Nombre;
                    dbProducto.Precio = producto.Precio;

                    _dbContext.Update(dbProducto);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProducto.Id;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Producto no encontrado";
                }


            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        //////////////////////////////////////////////////////////


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProducto = await _dbContext.Productos.FirstOrDefaultAsync(e => e.Id == id);




                if (dbProducto != null)
                {
                   
                    _dbContext.Remove(dbProducto);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Producto no encontrado";
                }


            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


    }
}
