using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace classFeb12.Models
{
    public class PeopleManager
    {
        private string _connectionString { get; set; }
        public PeopleManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetPeople()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Person";
            connection.Open();
            List<Person> people = new List<Person>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                people.Add(new Person {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return people;
        }

        public void AddPerson(Person person)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Person
                                    VALUES (@firstName, @lastName, @age)";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void DeletePerson(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Person WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public Person GetPerson(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM Person WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Person person = new Person()
            {
               Id =  (int)reader["Id"],
               FirstName = (string)reader["FirstName"],
               LastName = (string)reader["LastName"],
               Age = (int)reader["Age"]
            };
            return person;
        }

        public void UpdatePerson(Person person)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"UPDATE Person SET FirstName = @firstName, LastName = @lastName, Age = @age WHERE Id = @id";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@LastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            command.Parameters.AddWithValue("@id", person.Id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}