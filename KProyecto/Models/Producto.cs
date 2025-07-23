namespace KProyecto.Models
{
    public class Producto
    {
        public long IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public string Imagen { get; set; } = string.Empty;
    }
}