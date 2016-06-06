using System.Collections.Generic;
using System.Data;
using NUnit.Framework;



namespace SqlBoxCore.Test
{
    public class SqlExecuterTest
    {
       // [Test]
        //public void TomaRespuestasQueEstanEnPlatilla()
        //{
        //    //Crea un examen contestado
        //    var examenContestado = new ExamenBuilder().GetCompletedQuiz();
        //    //Numero de preguntas que se traeran de ese examen
        //    var indicePreguntas = new List<int> { 9, 27 };


        //    var cal = new ProcesarPuntos();
        //    //Metodo que trae Numero de respeustas del examen
        //    var respuestasLengh = cal.GetRespuestasByPlantillaCorrecion(indicePreguntas, examenContestado);

        //    Assert.AreEqual(2, respuestasLengh.Count);


        //} 
        [Test]
        public void NotificaQueNoEncontroRegistros()
        {
           
            var resultado = new SqlExecuter().RunSql();




            StringAssert.AreEqualIgnoringCase("No rows  found", resultado.Message);
        }
    }

  

  
}