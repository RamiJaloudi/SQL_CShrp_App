//using GenFu.ValueGenerators.Music;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseSQLMusicApp
{
    internal class AlbumsDAO
    {
        string connectionString = "datasource=localhost; port=3306;username=root;password=root;database=music2";

        public List<Album> getAllAlbums()
        {
            //start with an empty list
            List<Album> returnThese = new List<Album>();

            //connect to the SQL Server

            MySqlConnection connection = new MySqlConnection(connectionString);


            connection.Open();

            // define the sql statement to fetch all albums

            MySqlCommand command = new MySqlCommand("SELECT * FROM ALBUMS", connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    Album a = new Album
                    {


                        ID = reader.GetInt32(0),
                        AlbumName= reader.GetString(1),
                        ArtistName= reader.GetString(2),
                        Year= reader.GetInt32(3),
                        ImageURL= reader.GetString(4),
                        Description= reader.GetString(5),

                    };

                    returnThese.Add(a);
                }

            }

            connection.Close();



            return returnThese;

        }


        public List<Album> searchTitles(String searchTerm)
        {
            //start with an empty list
            List<Album> returnThese = new List<Album>();

            //connect to the SQL Server

            MySqlConnection connection = new MySqlConnection(connectionString);


            connection.Open();

            String searchWildPhrase = "%" + searchTerm + "%";

            MySqlCommand command = new MySqlCommand();

            command.CommandText = "SELECT ID, ALBUM_TITLE, ARTIST, YEAR, IMAGE_NAME, " +
                "DESCRIPTION FROM ALBUMS WHERE ALBUM_TITLE LIKE @search";

            command.Parameters.AddWithValue("@search", searchWildPhrase);

            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    Album a = new Album
                    {


                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5),

                    };

                    returnThese.Add(a);
                }

            }

            connection.Close();



            return returnThese;

        }

        internal int addOneAlbum(Album album)
        {
            //start with an empty list
            List<Album> returnThese = new List<Album>();

            //connect to the SQL Server

            MySqlConnection connection = new MySqlConnection(connectionString);


            connection.Open();

            // define the sql statement to fetch all albums

            MySqlCommand command = new MySqlCommand("INSERT INTO `albums`(`ALBUM_TITLE`, " +
                "`ARTIST`, `YEAR`, `IMAGE_NAME`, `DESCRIPTION`) VALUES (@albumtitle,@artist, @year, @imageURL, @description )", connection);

            
            command.Parameters.AddWithValue("@albumtitle", album.AlbumName);
            command.Parameters.AddWithValue("@artist", album.ArtistName);
            command.Parameters.AddWithValue("@year", album.Year);
            command.Parameters.AddWithValue("@imageURL", album.ImageURL);
            command.Parameters.AddWithValue("@description", album.Description);

            int newRows = command.ExecuteNonQuery(); 

            connection.Close();
            return newRows;


        }
    }


}

