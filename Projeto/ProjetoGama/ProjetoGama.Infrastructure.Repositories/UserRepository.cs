using Microsoft.Extensions.Configuration;
using ProjetoGama.Domain.Entities;
using ProjetoGama.Domain.Interfaces.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjetoGama.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<User> GetByLoginAsync(string email)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @$"SELECT U.UserId, 
                                           U.Name,
                                           U.Email,
                                           U.Password,
                                           P.ProfileId,
                                           P.Description 
                                        FROM [USER] U
                                    JOIN [PROFILE] P ON U.ProfileId = P.ProfileId
                                    WHERE U.Email='{email}'";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var user = new User(int.Parse(reader["UserId"].ToString()),
                                                reader["Name"].ToString(),
                                                new Profile(int.Parse(reader["ProfileId"].ToString()),
                                                            reader["Description"].ToString()));
                        
                            user.InformationLoginUser(reader["Email"].ToString(), reader["Password"].ToString());
                            return user;
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

        public async Task<int> InsertAsync(User user)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"INSERT INTO 
                                     [USER](Name, 
                                         Email, 
                                         Password,
                                         ProfileID,
                                         Created) 
                                VALUES (@Name, 
                                         @Email,
                                         @Password,
                                         @ProfileID, 
                                         @Created); SELECT scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("Name", user.Name);
                        cmd.Parameters.AddWithValue("Email", user.Email);
                        cmd.Parameters.AddWithValue("Password", user.Password);
                        cmd.Parameters.AddWithValue("ProfileID", user.Profile.Id);
                        cmd.Parameters.AddWithValue("Created", user.Created);

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
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"UPDATE [USER] 
                                    SET (ProfileID,
                                            Name, 
                                            Email, 
                                            Password) 
                                   VALUES (@ProfileID, 
                                            @Name,
                                            @Email, 
                                            @Password)";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("ProfileID", user.Profile.Id);
                        cmd.Parameters.AddWithValue("Name", user.Name);
                        cmd.Parameters.AddWithValue("Email", user.Email);
                        cmd.Parameters.AddWithValue("Password", user.Password);

                        con.Open();
                        await cmd
                                .ExecuteScalarAsync()
                                .ConfigureAwait(false);

                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @$"SELECT U.UserId, 
                                           U.Name,
                                           U.Email,
                                           U.Password,
                                           P.ProfileId,
                                           P.Description 
                                        FROM [USER] U
                                    JOIN [PROFILE] P ON U.ProfileId = P.ProfileId
                                    WHERE U.UserId='{id}'";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var user = new User(int.Parse(reader["UserId"].ToString()),
                                                reader["Name"].ToString(),
                                                new Profile(int.Parse(reader["ProfileId"].ToString()),
                                                            reader["Description"].ToString()));

                            user.InformationLoginUser(reader["Name"].ToString(), reader["Name"].ToString());
                            return user;
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
    }
}
