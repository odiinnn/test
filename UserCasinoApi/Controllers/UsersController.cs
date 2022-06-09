using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserCasinoApi.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace UserCasinoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetTodoItems()
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            var user = await _context.TodoItems.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.TodoItems == null)
          {
              return Problem("Entity set 'UserContext.TodoItems'  is null.");
          }
            user.Token = GenRandomString("QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm12345678910_;&?/,", 32);
            _context.TodoItems.Add(user);
            await _context.SaveChangesAsync();
            try
            {
                Insert(user);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Fail", ex);
            }
            

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            var user = await _context.TodoItems.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public string GenRandomString(string Alphabet, int Length)
        {

            Random rnd = new Random();

            StringBuilder sb = new StringBuilder(Length - 1);

            int Position = 0;

            for (int i = 0; i < Length; i++)
            {

                Position = rnd.Next(0, Alphabet.Length - 1);
                sb.Append(Alphabet[Position]);
            }

            return sb.ToString();

        }

        public void Insert(User user)
        {
            //string path = "D:/Projects/c#/UserCasinoApi/UserCasinoApi/Models/users.db";
            var cn = new SqliteConnection("Data Source=users.db");
            cn.Open();
            //SqliteCommand insertSQL = new SqliteCommand("INSERT INTO user (id, username, password, balance, token) VALUES (1,`dsa`,`dasdsa`, 10, `dsaadasd`)", cn);
            //insertSQL.Parameters.Add(user.Id);
            //insertSQL.Parameters.Add(user.Username);
            //insertSQL.Parameters.Add(user.Password);
            //insertSQL.Parameters.Add(user.Balance);
            //insertSQL.Parameters.Add(user.Token);
            //string commandText = "INSERT INTO user (id, username, password, balance, token) VALUES (@Id, @Username, @Password, @Balance, @Token)";
            string commandText = "INSERT INTO user (id, username, password, balance, token) VALUES (2, 2, 2, 2, 2)";
            SqliteCommand insertSQL = new SqliteCommand(commandText, cn);
            //SqliteParameter id = new SqliteParameter("@id", user.Id);
            //insertSQL.Parameters.Add(id);
            //SqliteParameter username = new SqliteParameter("@username", user.Username);
            //insertSQL.Parameters.Add(username);
            //SqliteParameter password = new SqliteParameter("@password", user.Password);
            //insertSQL.Parameters.Add(password);
            //SqliteParameter balance = new SqliteParameter("@balance", user.Balance);
            //insertSQL.Parameters.Add(balance);
            //SqliteParameter token = new SqliteParameter("@token", user.Token);
            //insertSQL.Parameters.Add(token, id);
            insertSQL.ExecuteNonQuery();
            cn.Close();
        }
} 
}


