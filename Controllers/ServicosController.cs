using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestVeicular.Models;
using GestVeicular.Data;

namespace GestVeicular.Controllers
{
  

    public class ServicosController : Controller
    {   

        private readonly ApplicationDbContext _context;
        private readonly Servicos _servicos;
        public ServicosController(ApplicationDbContext context, Servicos servicos)
        {
            _context = context;
            _servicos = servicos;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<Response<Servicos>> Detalhes(int id)
        {
            Response<Servicos> response = new Response<Servicos>();
            try
            {
                var servico = await _context.Servicos.FindAsync(id);
                if (servico == null)
                {
                    response.Status = false;
                    response.Mensagem = "Serviço não encontrado.";
                    return response;
                }
                response.Status = true;
                response.Dados = servico;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Response<Servicos>> CriarServico(Servicos servicos)
        {
            Response<Servicos> response = new Response<Servicos>();
            try
            {
                
                    _context.Servicos.Add(servicos);
                    await _context.SaveChangesAsync();
                    response.Mensagem = "Serviço cadastrado com sucesso!";

                    return response;
                    
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Response<Servicos>> EditarServico(int id)
        {
            Response<Servicos> response = new Response<Servicos>();
            try
            {
                var servico = await _context.Servicos.FindAsync(id);
                if (servico == null)
                {
                    response.Status = false;
                    response.Mensagem = "Serviço não encontrado.";
                    return response;
                }
                servico.DataUltimaAtualizacao = DateTime.Now;
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Serviço atualizado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }

            [HttpGet]
        public ActionResult ExcluirServico(int id)
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <Response<Servicos>> ExcluirServico(Servicos servicos)
        {
            Response<Servicos> response = new Response<Servicos>();
            try
            { 
                _context.Servicos.Remove(servicos);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Serviço excluído com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }
    }
}
