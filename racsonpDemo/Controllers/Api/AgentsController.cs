using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using racsonpDemo.DataServices;
using racsonpDemo.Models.Entities;
using racsonpDemo.ViewModels;


namespace racsonpDemo.Controllers.Api
{
    public class AgentsController : ApiController
    {


        private AgentService dataService;
        // private AdventureWorks2012Entities db;

        public AgentsController()
        {
            dataService = new AgentService();
            // db = new AdventureWorks2012Entities();
            AutoMapper.Mapper.CreateMap<Agent, AgentViewModel>();
            AutoMapper.Mapper.CreateMap<AgentViewModel, AgentViewModel>();
        }



        // GET: api/Authors
        public ResultList<AgentViewModel> Get([FromUri]QueryOptions queryOptions)
        {
            var entities = dataService.Get(queryOptions);

            return new ResultList<AgentViewModel>(
                AutoMapper.Mapper.Map<List<Agent>, List<AgentViewModel>>(entities), queryOptions);
        }

        // GET: api/Authors/5
        [ResponseType(typeof(AgentViewModel))]
        public IHttpActionResult Get(int id)
        {
            var entity = dataService.GetById(id);

            return Ok(AutoMapper.Mapper.Map<Agent, AgentViewModel>(entity));
        }

        // PUT: api/Authors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(AgentViewModel entity)
        {
            var model = AutoMapper.Mapper.Map<AgentViewModel, Agent>(entity);

            dataService.Update(model);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Authors
        [ResponseType(typeof(AgentViewModel))]
        public IHttpActionResult Post(AgentViewModel entity)
        {
            var model = AutoMapper.Mapper.Map<AgentViewModel, Agent>(entity);

            dataService.Insert(model);

            return CreatedAtRoute("DefaultApi", new { id = entity.Id }, entity);
        }





        // DELETE: api/Authors/5
        [ResponseType(typeof(Agent))]
        public IHttpActionResult DeleteAuthor(int id)
        {
            var entity = dataService.GetById(id);

            dataService.Delete(entity);

            return Ok(entity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dataService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}