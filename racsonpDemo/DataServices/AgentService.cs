using System;
using System.Collections.Generic;
using System.Data.Entity;
using racsonpDemo.DataAcceses;
using racsonpDemo.Models.Entities;
using racsonpDemo.ViewModels;

using System.Linq;
using System.Linq.Dynamic;


namespace racsonpDemo.DataServices
{
    public class AgentService : IDisposable
    {
        private DataBaseContext db = new DataBaseContext();

        public List<Agent> Get(QueryOptions queryOptions)
        {
            // queryOptions.SortField = "Id";
            var start = QueryOptionsCalculator.CalculateStart(queryOptions);

            var authors = db.Agents.
                OrderBy(queryOptions.Sort).
                Skip(start).
                Take(queryOptions.PageSize);

            queryOptions.TotalPages = QueryOptionsCalculator.CaclulateTotalPages(
                db.Agents.Count(), queryOptions.PageSize);

            return authors.ToList();
        }

        public Agent GetById(long id)
        {
            Agent author = db.Agents.Find(id);
            if (author == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException
                    (string.Format("Unable to find entity with id {0}", id));
            }

            return author;
        }

        public Agent GetByName(string name)
        {
            Agent author = db.Agents
                .Where(a => a.FirstName + ' ' + a.LastName == name)
                .SingleOrDefault();
            if (author == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException
                    (string.Format("Unable to find entity  with name {0}", name));
            }

            return author;
        }

        public void Insert(Agent agent)
        {
            db.Agents.Add(agent);

            db.SaveChanges();
        }

        public void Update(Agent author)
        {
            db.Entry(author).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void Delete(Agent author)
        {
            db.Agents.Remove(author);

            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}