using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using AssessmentJonhTRMFS.Data;
using AssessmentJonhTRMFS.Models;

namespace AssessmentJonhTRMFS.Models
{
    public class PessoaRepository
    {

        public AssessmentJonhTRMFSContext db = new AssessmentJonhTRMFSContext();

        public void Create(Pessoa pessoa)
        {
            db.Pessoas.Add(pessoa);
            db.SaveChanges();
        }
        public void Edit(Pessoa pessoa)
        {
            db.Entry(pessoa).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
        public Pessoa Buscar(int? id)
        {
            Pessoa pessoa = db.Pessoas.Find(id);
            return pessoa;
        }
        public void Delete(Pessoa pessoa)
        {
            db.Pessoas.Remove(pessoa);
            db.SaveChanges();
        }

    }
}