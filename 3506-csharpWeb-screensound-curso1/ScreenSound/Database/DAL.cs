using Microsoft.Identity.Client;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Database
{
    internal  class DAL <T> where T : class
    {
        protected readonly ScreenSoundContext context;

        public DAL(ScreenSoundContext context)
        {
            this.context = context;
        }

        public  IEnumerable<T> Listar()
        {
            return context.Set<T>().ToList();
        }
        public void Adicionar(T objeto)
        {
            context.Set<T>().Add(objeto);
        }
        
        public  void Atualizar(T objeto)
        {
            context.Set<T>().Update(objeto);
        }

        public  void Deletar(T objeto)
        {
            context.Set<T>().Remove(objeto);
        }

        public  T? ObterPor(Func<T,Boolean> condicao)
        {
            return context.Set<T>().FirstOrDefault(condicao);
        }
    }
}
