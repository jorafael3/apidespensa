using Microsoft.EntityFrameworkCore;
using DespensaAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DespensaAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Productos> Producto { get; set; }

        // Método para obtener todos los productos usando un procedimiento almacenado
        public async Task<List<Productos>> GetProductos()
        {
            return await Producto.FromSqlRaw("EXEC GetProductos").ToListAsync();
        }

        // Método para obtener un producto por ID usando un procedimiento almacenado
        public async Task<Productos> GetProductoById(int id)
        {
            var idParam = new SqlParameter("@Id", id);
            var productos = await Producto.FromSqlRaw("EXEC GetProductoById @Id", idParam).ToListAsync();

            return productos.FirstOrDefault(); // Devuelve el primer producto encontrado
        }

        // Método para crear un nuevo producto usando un procedimiento almacenado
        public async Task<int> AddProducto(Productos producto)
        {
            var nombreParam = new SqlParameter("@Nombre", producto.Nombre);
            var precioParam = new SqlParameter("@Precio", producto.Precio);
            var stockParam = new SqlParameter("@Stock", producto.Stock);

            return await Database.ExecuteSqlRawAsync("EXEC AddProducto @Nombre, @Precio, @Stock", nombreParam, precioParam, stockParam);
        }

        // Método para actualizar un producto usando un procedimiento almacenado
        public async Task<int> UpdateProducto(Productos producto)
        {
            var idParam = new SqlParameter("@Id", producto.Id);
            var nombreParam = new SqlParameter("@Nombre", producto.Nombre);
            var precioParam = new SqlParameter("@Precio", producto.Precio);
            var stockParam = new SqlParameter("@Stock", producto.Stock);

            return await Database.ExecuteSqlRawAsync("EXEC UpdateProducto @Id, @Nombre, @Precio, @Stock", idParam, nombreParam, precioParam, stockParam);
        }

        // Método para eliminar un producto usando un procedimiento almacenado
        public async Task<int> DeleteProducto(int id)
        {
            var idParam = new SqlParameter("@Id", id);
            return await Database.ExecuteSqlRawAsync("EXEC DeleteProducto @Id", idParam);
        }

        public DbSet<User> Users { get; set; }

    }
}
