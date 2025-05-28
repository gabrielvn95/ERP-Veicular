using GestVeicular.Enums;
using GestVeicular.Models;
using GestVeicular.Repositorio;
using GestVeicular.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;

public class UsuarioController : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ISessaoInterface _sessaoService;

    public UsuarioController(IUsuarioRepositorio usuarioRepositorio, ISessaoInterface sessaoService)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _sessaoService = sessaoService;
    }

    private Usuario ObterUsuarioLogado()
    {
        return _sessaoService.BuscarSessao();
    }

    public IActionResult PainelAdmin()
    {
        var usuarioLogado = ObterUsuarioLogado();

        if (usuarioLogado == null || usuarioLogado.TipoUsuario != TipoUsuario.Admin)
        {
            TempData["MensagemErro"] = "Acesso negado: você não tem permissão para acessar essa página.";
            return RedirectToAction("Index", "Home");
        }

        var usuarios = _usuarioRepositorio.BuscarTodosUsuarios();
        return View(usuarios);
    }

    public IActionResult Editar(int id)
    {
        var usuario = _usuarioRepositorio.BuscarPorId(id);
        if (usuario == null) return NotFound();
        return View(usuario);
    }

    [HttpPost]
    public IActionResult Editar(Usuario usuario)
    {
        try
        {
            _usuarioRepositorio.Atualizar(usuario);
            TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
            return RedirectToAction("PainelAdmin");
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Erro ao atualizar usuário: {ex.Message}";
            return View(usuario);
        }
    }

    public IActionResult Deletar(int id)
    {
        try
        {
            _usuarioRepositorio.Apagar(id);
            TempData["MensagemSucesso"] = "Usuário removido com sucesso!";
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Erro ao remover usuário: {ex.Message}";
        }

        return RedirectToAction("PainelAdmin");
    }
}
