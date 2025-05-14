namespace GestVeicular.Models
{
    public class Response<T>
    {
        public bool Status { get; set; }
        public string Mensagem { get; set; }
        public T? Dados { get; set; }
    }
    
}
