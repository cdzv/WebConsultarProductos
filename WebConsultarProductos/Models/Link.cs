namespace WebConsultarProductos.Models
{
    public class Link
    {
        public Self self { get; set; }
        public Next next { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Next
    {
        public string href { get; set; }
    }
}
