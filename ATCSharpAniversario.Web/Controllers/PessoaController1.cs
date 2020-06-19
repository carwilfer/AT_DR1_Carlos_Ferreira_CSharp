using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATCSharpAniversario.Dados;
using ATCSharpAniversario.Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATCSharpAniversario.Web.Controllers
{
    public class PessoaController1 : Controller
    {
        public PessoaController1()
        {
            Bd = new BancoDeDadosDeArquivos();
        }
        private readonly BancoDeDados Bd;
        // GET: PessoaController1
        public ActionResult Index()
        {
            var pessoas = Bd.BuscarTodosOsAniversariantes();
            return View(pessoas);
        }

        // GET: PessoaController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PessoaController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PessoaController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pessoa pessoa)
        {
            try
            {
                Bd.Salvar(pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoaController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PessoaController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoaController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PessoaController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
