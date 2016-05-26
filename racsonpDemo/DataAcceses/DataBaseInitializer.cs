using System.Collections.Generic;
using System.Data.Entity;
using racsonpDemo.Models.Entities;

namespace racsonpDemo.DataAcceses
{
   // public class DataBaseInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
    public class DataBaseInitializer : CreateDatabaseIfNotExists<DataBaseContext>
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


               var p1 = new Producto
               {
                   Nombre = "Cat Chow 1.5",
                   Precio = 6.25m,
                   Tienda = "WALLMART"


               };
               context.Productos.Add(p1);


               

            context.SaveChanges();
        }
    
    }
}