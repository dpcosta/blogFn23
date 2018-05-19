using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caelum.Fn23.Curso.DAO
{
    public interface IDataAccessObject<T>
    {
        IList<T> Lista { get; }
        T BuscaPorId(int id);
        void Incluir(T obj);
        void Alterar(T obj);
        void Remover(T obj);
    }
}