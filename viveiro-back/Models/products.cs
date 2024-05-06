namespace viveiro_back.Models
{
    public class products
    {
        public int id {  get; set; }
        public string nombre { get; set; }
        public int numero { get; set; }
        public int serie { get; set; }
        public int fraccion { get; set; }
        public float precio { get; set; }
        public Boolean disponibilidad { get; set; }   
    }
}
