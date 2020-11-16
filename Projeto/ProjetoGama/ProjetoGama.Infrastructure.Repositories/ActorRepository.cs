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

                Actor actor = default;

                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    con.Open();
                    var actorList = new List<Actor>();
                    var sqlCmd = @"SELECT 
                                  A.[ActorId]
                                 ,A.[UserId]   
	                             ,A.[Racking]
                                 ,A.[Sex]   
	                             ,A.[SalaryHour]
                                 FROM [dbo].[ACTOR] A";

                    using (var cmd = new SqlCommand(sqlCmd, con))
                    { 

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {

                            var genres = new List<Genre>();

                            var sqlGenres = @"SELECT
                                            G.[Description],
                                            G.[GenreId]
                                            FROM [dbo].[ACTOR_GENRE] AG INNER JOIN [dbo].[GENRE] G ON AG.GenreId = G.GenreId
                                            WHERE AG.ActorId = @Id";

                            using (var con2 = new SqlConnection(_configuration["ConnectionString"]))
                            {
                                con2.Open();

                                using (var cmdGeneros = new SqlCommand(sqlGenres, con2))
                                { 

                                    cmdGeneros.Parameters.AddWithValue("@Id", int.Parse(reader["ActorId"].ToString()));

                                    var readerGeneros = cmdGeneros.ExecuteReader();

                                    while (readerGeneros.Read())
                                    {
                                        genres.Add(new Genre(int.Parse(readerGeneros["GenreId"].ToString()), 
                                                                readerGeneros["Description"].ToString()));
                                    }

                                }
                            }

                            actor = new Actor(int.Parse(reader["ActorId"].ToString()),
                                                  genres,
                                                  Char.Parse(reader["Sex"].ToString()),
                                                  double.Parse(reader["SalaryHour"].ToString()),
                                                  int.Parse(reader["UserId"].ToString()),
                                                  int.Parse(reader["Racking"].ToString()));

                            actorList.Add(actor);
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
                    con.Open();
                    var sqlCmd = @"SELECT 
                                  A.[ActorId]
                                 ,A.[UserId]
	                             ,A.[Racking]
                                 ,A.[Sex]
	                             ,A.[SalaryHour]
                                 FROM[dbo].[ACTOR] A
                                WHERE A.[ActorId] = @Id";

                    using (var cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Id", id);

                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var genres = new List<Genre>();

                            var sqlGenres = @"SELECT
                                            G.[GenreId],
                                            G.[Description]
                                            FROM [dbo].[ACTOR_GENRE] AG INNER JOIN [dbo].[GENRE] G ON AG.GenreId = G.GenreId
                                            WHERE AG.ActorId = @Id";

                            using (var con2 = new SqlConnection(_configuration["ConnectionString"]))
                            {
                                con2.Open();

                                using (var cmdGeneros = new SqlCommand(sqlGenres, con2))
                                {

                                    cmdGeneros.Parameters.AddWithValue("@Id", int.Parse(reader["ActorId"].ToString()));

                                    var readerGeneros = cmdGeneros.ExecuteReader();

                                    while (readerGeneros.Read())
                                    {
                                        genres.Add(new Genre(int.Parse(readerGeneros["GenreId"].ToString()),
                                                                readerGeneros["Description"].ToString()));
                                    }

                                }
                            }                                                       
                                
                            return  new Actor(int.Parse(reader["ActorId"].ToString()),
                                                  genres,
                                                  Char.Parse(reader["Sex"].ToString()),
                                                  double.Parse(reader["SalaryHour"].ToString()),
                                                  int.Parse(reader["UserId"].ToString()),
                                                  int.Parse(reader["Racking"].ToString()));
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

        public async Task<int> GetActorByUserIdAsync(int UserId)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    con.Open();
                    var sqlCmd = @"SELECT 
                                  A.[ActorId]
                                 ,A.[UserId]
	                             ,A.[Racking]
                                 ,A.[Sex]
	                             ,A.[SalaryHour]
                                 FROM[dbo].[ACTOR] A
                                WHERE A.[UserId] = @UserId";

                    using (var cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", UserId);

                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);

                        while (reader.Read())
                        { 
                            return int.Parse(reader["ActorId"].ToString());
                        }

                        return 0;
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
             try
             {
                 using (var con = new SqlConnection(_configuration["ConnectionString"]))
                 {
                     var sqlCmd = @"INSERT INTO 
                                     [ACTOR](UserId, 
                                         Racking, 
                                         Sex, 
                                         SalaryHour) 
                                VALUES (@UserId, 
                                         @Racking,
                                         @Sex, 
                                         @SalaryHour); SELECT scope_identity();";

                     using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                     {
                        con.Open();
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("UserId", actor.UserId);
                        cmd.Parameters.AddWithValue("Racking", actor.Ranking);
                        cmd.Parameters.AddWithValue("Sex", actor.Sex);
                        cmd.Parameters.AddWithValue("SalaryHour", actor.Salary);
                        
                        var id = await cmd
                                         .ExecuteScalarAsync()
                                         .ConfigureAwait(false);

                        foreach (Genre element in actor.GenresId)
                        {
                            using (var con2 = new SqlConnection(_configuration["ConnectionString"]))
                            {
                                var sql = @"INSERT INTO 
                                     [ACTOR_GENRE](ActorId, 
                                         GenreId) 
                                VALUES (@ActorId, 
                                         @GenreId);";

                                using (SqlCommand cmd2 = new SqlCommand(sql, con2))
                                {
                                    con2.Open();
                                    cmd2.CommandType = CommandType.Text;

                                    cmd2.Parameters.AddWithValue("ActorId", id);
                                    cmd2.Parameters.AddWithValue("GenreId", element.Id);

                                    cmd2.ExecuteScalar();

                                }
                            }
                        }

                        return int.Parse(id.ToString());
                     }
                 }
             }
             catch (SqlException ex)
             {
                 throw new Exception(ex.Message);
             } 
        }

   
    }
}
