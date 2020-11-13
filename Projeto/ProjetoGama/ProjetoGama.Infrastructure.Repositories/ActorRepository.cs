using Microsoft.Extensions.Configuration;
using ProjetoGama.Domain.Entities;
using ProjetoGama.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjetoGama.Infrastructure.Repositories
{
    public class ActorRepository : IActorRepository
    {

        private readonly IConfiguration _configuration;

        public ActorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IEnumerable<Actor> GetActor()
        {
            try
            {
                using (var con = new SqlConnection("ConnectionString"))
                {
                    var actorList = new List<Actor>();
                    var sqlCmd = @"SELECT U.[UserId]
                                 ,U.[Name]
                                 ,U.[Email]
                                 ,U.[BirthDate]
                                 ,U.[Sex]
                                 ,U.[Password]
                                 ,A.[ActorId]
	                             ,A.[Racking]
	                             ,A.[SalaryHour]
                                 FROM [dbo].[USER] U INNER JOIN [dbo].[ACTOR] A ON A.UserId = U.UserId";

                    using (var cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = cmd.ExecuteReader();
                        var genreList = new List<Genre>();

                        while (reader.Read())
                        {

                            var sqlGenres = @"SELECT
                                            G.[GenreId],    
	                                        G.[Description]
                                            FROM [dbo].[ACTOR_GENRE] AG INNER JOIN [dbo].[GENRE] G ON AG.GenreId = G.GenreId
                                            WHERE AG.ActorId = @Id";


                            using (var cmdGeneros = new SqlCommand(sqlGenres, con))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmdGeneros.Parameters.AddWithValue("@Id", reader["ActorId"].ToString());

                                var readerGeneros = cmd.ExecuteReader();

                                while (readerGeneros.Read())
                                {
                                    var genre = new Genre(int.Parse(readerGeneros["GenreId"].ToString()),
                                                          readerGeneros["Description"].ToString()
                                                          );
                                    genreList.Add(genre);
                                }

                            }


                            /*var actor = new Actor(int.Parse(reader["UserId"].ToString()),
                                                     reader["Name"].ToString(),
                                                     DateTime.Parse(reader["BirthDate"].ToString()),
                                                     reader["Email"].ToString(),
                                                     reader["Password"].ToString(),
                                                     genreList,
                                                     (Sex) Enum.Parse(typeof(Sex), reader["Sex"].ToString()),
                                                     Double.Parse(reader["SalaryHour"].ToString()),
                                                     int.Parse(reader["Racking"].ToString())
                                                     ); */

                            actorList.Add(null);
                        }

                        return actorList;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Actor> GetActorByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var heroList = new List<Actor>();
                    var sqlCmd = "dbo.SELECIONAR_HEROIS_POR_ID";

                    using (var cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        con.Open();

                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            return default;
                        }

                        return default;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> InsertActorAsync(Actor actor)
        {
            /* try
             {
                 using (var con = new SqlConnection(_configuration["ConnectionString"]))
                 {
                     var sqlCmd = @"INSERT INTO 
                                     HERO (Name, 
                                         IdEditor, 
                                         Age, 
                                         Created) 
                                VALUES (@name, 
                                         @editor,
                                         @age, 
                                         @created); SELECT scope_identity();";

                     using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                     {
                         cmd.CommandType = CommandType.Text;

                         cmd.Parameters.AddWithValue("name", actor.);


                         con.Open();
                         var id = await cmd
                                         .ExecuteScalarAsync()
                                         .ConfigureAwait(false);

                         return int.Parse(id.ToString());
                     }
                 }
             }
             catch (SqlException ex)
             {
                 throw new Exception(ex.Message);
             } */

            return 0;
        }

   
    }
}
