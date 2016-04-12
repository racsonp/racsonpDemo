using System.Collections.Generic;
using System.Data.Entity;
using racsonpDemo.Models.Entities;

namespace racsonpDemo.DataAcceses
{
    public class DataBaseInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
    {
           protected override void Seed(DataBaseContext context)
        {
            var entity = new Agent
            {
                Biography = "...",
                FirstName = "Oscar",
                LastName = "Rivera"
            };
               context.Agents.Add(entity);

               var entity2 = new Agent
               {
                   Biography = "...",
                   FirstName = "Laura",
                   LastName = "Delgado"
               };
               context.Agents.Add(entity2);

            context.SaveChanges();
        }
    
    }
}